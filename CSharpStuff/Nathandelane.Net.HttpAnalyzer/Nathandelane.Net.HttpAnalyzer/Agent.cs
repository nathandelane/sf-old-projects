using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using ICSharpCode.SharpZipLib.GZip;

namespace Nathandelane.Net.HttpAnalyzer
{
	public class Agent : IDisposable
	{
		#region Fields

		private bool _isDisposed;
		private Uri _destination;
		private HtmlDocument _document;
		private CookieCollection _cookies;
		private WebHeaderCollection _headers;
		public bool _ignoreBadCerts;

		#endregion

		#region Properties

		/// <summary>
		/// Gets whether the object was disposed.
		/// </summary>
		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		/// <summary>
		/// Gets or sets the Accept header for the request.
		/// </summary>
		public string AcceptHeader { get; set; }

		/// <summary>
		/// Gets or sets the Accept-Language header for the request.
		/// </summary>
		public string AcceptLanguageHeader { get; set; }

		/// <summary>
		/// Gets or sets the Accept-Encoding header for the request.
		/// </summary>
		public string AcceptEncodingHeader { get; set; }

		/// <summary>
		/// Gets or sets the Accept-Charset header for the request.
		/// </summary>
		public string AcceptCharsetHeader { get; set; }

		/// <summary>
		/// Gets or sets the User-Agent header for the request.
		/// </summary>
		public string UserAgentHeader { get; set; }

		/// <summary>
		/// Gets or sets the Timeout header for the request.
		/// </summary>
		public int? Timeout { get; set; }

		/// <summary>
		/// Gets or sets the proxy to use for the request if required.
		/// </summary>
		public WebProxy Proxy { get; set; }

		/// <summary>
		/// Gets or sets the form post data.
		/// </summary>
		public string PostData { get; set; }

		/// <summary>
		/// Gets or sets the cookie collection to use in the request. Also gets the response cookies after run.
		/// </summary>
		public CookieCollection Cookies
		{
			get { return _cookies; }
			set { _cookies = value; }
		}

		public WebHeaderCollection Headers
		{
			get { return _headers; }
			set { _headers = value; }
		}

		public bool IgnoreBadCerts
		{
			get { return _ignoreBadCerts; }
			set { _ignoreBadCerts = value; }
		}

		/// <summary>
		/// Gets HtmlDocument from response after run.
		/// </summary>
		public HtmlDocument Document
		{
			get { return _document; }
		}

		#endregion

		#region Constructors

		public Agent(Uri uri)
		{
			_destination = uri;
		}

		public Agent(Uri uri, bool ignoreBadCerts)
		{
			_destination = uri;
			_ignoreBadCerts = ignoreBadCerts;
		}

		#endregion

		#region Methods

		public void Run()
		{
			try
			{
				HttpWebRequest request = WebRequest.Create(_destination) as HttpWebRequest;
				request.Method = "get";
				request.Accept = AcceptHeader ?? AgentDefaults.AcceptHeader;
				request.KeepAlive = true;
				request.UserAgent = UserAgentHeader ?? AgentDefaults.UserAgentHeader;
				request.Timeout = (int)(Timeout ?? AgentDefaults.TimeoutHeader);

				if (Headers != null)
				{
					request.Headers = Headers;
				}

				request.Headers.Add("Accept-Language", AcceptLanguageHeader ?? AgentDefaults.AcceptLanguageHeader);
				request.Headers.Add("Accept-Encoding", AcceptEncodingHeader ?? AgentDefaults.AcceptEncodingHeader);
				request.Headers.Add("Accept-Charset", AcceptCharsetHeader ?? AgentDefaults.AcceptCharsetHeader);

				if (IgnoreBadCerts)
				{
					ServicePointManager.CertificatePolicy = new AcceptAllCertsPolicy();
				}

				if (Cookies != null)
				{
					request.CookieContainer = new CookieContainer();
					request.CookieContainer.Add(Cookies);
				}

				if (Proxy != null)
				{
					request.Proxy = Proxy;
				}

				if (!String.IsNullOrEmpty(PostData))
				{
					request.Method = "post";

					using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
					{
						char[] encodedPostData = PostData.ToCharArray();//HttpUtility.UrlEncode(PostData).ToCharArray();

						writer.Write(encodedPostData);
						writer.Flush();
					}
				}

				try
				{
					HttpWebResponse response = request.GetResponse() as HttpWebResponse;

					_document = new HtmlDocument();

					if (response.ContentEncoding.ToLower().Equals("gzip"))
					{
						using (StreamReader reader = new StreamReader(new GZipInputStream(response.GetResponseStream())))
						{
							string data = reader.ReadToEnd();

							_document.LoadHtml(data);
						}
					}
					else
					{
						using (StreamReader reader = new StreamReader(response.GetResponseStream()))
						{
							string data = reader.ReadToEnd();

							_document.LoadHtml(data);
						}
					}

					Cookies = response.Cookies;
					Headers = response.Headers;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception caught! {0}", ex.Message);
				}
			}
			catch (Exception outerEx)
			{
				Console.WriteLine("Outer exception caught! {0}", outerEx.Message);
			}
		}

		public static void DisplayHelp()
		{
			Console.WriteLine("HttpAnalyzer [--help | --uri=<absolulte-url> [--suppress] [--find=<xpath-expression>] [--no-attributes] [--no-innerhtml] [--data] [--attributes=attr1,attrN] [--headers=hnm0=hval0&hnmN=hvalN] [--cookies=hcok0=hval0&hcokN=hvalN] [--proxy=host:port] [--ignore-bad-certs]]");
		}

		public override string ToString()
		{
			string toString = String.Empty;

			if (Headers != null)
			{
				toString = "--headers=";

				foreach (string key in Headers.AllKeys)
				{
					toString = String.Concat(toString, key, "=", Headers[key], "&");
				}

				toString = toString.Substring(0, toString.Length - 1);
			}
			else
			{
				toString = "Headers is null";
			}

			return toString;
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (!_isDisposed)
			{
				_isDisposed = true;
			}
		}

		#endregion
	}
}

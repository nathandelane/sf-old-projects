using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using System.IO;

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

		#endregion

		#region Methods

		public void Run()
		{
			HttpWebRequest request = WebRequest.Create(_destination) as HttpWebRequest;
			request.Accept = AcceptHeader ?? AgentDefaults.AcceptHeader;
			request.KeepAlive = true;
			request.UserAgent = UserAgentHeader ?? AgentDefaults.UserAgentHeader;
			request.Timeout = (int)(Timeout ?? AgentDefaults.TimeoutHeader);

			if(Headers != null)
			{
				request.Headers = Headers;
			}

			request.Headers.Add("Accept-Language", AcceptLanguageHeader ?? AgentDefaults.AcceptLanguageHeader);
			request.Headers.Add("Accept-Encoding", AcceptEncodingHeader ?? AgentDefaults.AcceptEncodingHeader);
			request.Headers.Add("Accept-Charset", AcceptCharsetHeader ?? AgentDefaults.AcceptCharsetHeader);

			if (Cookies != null)
			{
				request.CookieContainer = new CookieContainer();
				request.CookieContainer.Add(Cookies);
			}

			HttpWebResponse response = request.GetResponse() as HttpWebResponse;

			_document = new HtmlDocument();

			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				string data = reader.ReadToEnd();

				_document.LoadHtml(data);
			}

			Cookies = response.Cookies;
			Headers = response.Headers;
		}

		public static void DisplayHelp()
		{

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

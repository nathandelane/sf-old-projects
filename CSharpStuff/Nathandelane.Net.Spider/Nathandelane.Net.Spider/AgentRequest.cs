/*
 * Created by SharpDevelop.
 * User: nalane
 * Date: 1/28/2011
 * Time: 12:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using HtmlAgilityPack;

namespace Nathandelane.Net.Spider
{
	/// <summary>
	/// Description of AgentRequest.
	/// </summary>
	public class AgentRequest
	{
		#region Fields
		
		private HttpWebRequest _request;
		private IList<string> _urls;
		private IList<SpiderUrl> _spiderUrls;
		private TimeSpan _elapsedTime;
		private string _documentTitle;
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets the cookies associated with this AgentRequest and its response.
		/// </summary>
		public CookieCollection Cookies
		{
			get { return _request.CookieContainer.GetCookies(_request.RequestUri); }
		}
		
		/// <summary>
		/// Gets the document title for this request's response.
		/// </summary>
		public string DocumentTitle
		{
			get { return _documentTitle; }
		}
		
		/// <summary>
		/// Gets the hash for this agent.
		/// </summary>
		public string Hash
		{
			get
			{
				string hash = String.Empty;

				byte[] buffer = ASCIIEncoding.ASCII.GetBytes(_request.RequestUri.OriginalString);
				SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();

				hash = BitConverter.ToString(provider.ComputeHash(buffer)).Replace("-", String.Empty);

				return hash;
			}
		}

		/// <summary>
		/// Gets the user-agent string for this agent.
		/// </summary>
		public string UserAgent
		{
			get
			{
				string retVal = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.8) Gecko/2009032609 Firefox/3.0.8 (.NET CLR 3.5.30729) Vehix Spider";

				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["overrideUserAgent"]))
				{
					retVal = ConfigurationManager.AppSettings["overrideUserAgent"];
				}

				return retVal;
			}
		}
		
		/// <summary>
		/// Gets the target for this AgentRequest.
		/// </summary>
		public string Target
		{
			get { return _request.RequestUri.AbsolutePath; }
		}
		
		/// <summary>
		/// Gets the referrer for this AgentRequest.
		/// </summary>
		public string Referrer
		{
			get { return _request.Referer; }
		}
		
		/// <summary>
		/// Gets the elapsed time for this request.
		/// </summary>
		public TimeSpan ElapsedTime
		{
			get { return _elapsedTime; }
		}

		/// <summary>
		/// Gets the number of ticks.
		/// </summary>
		private long Ticks
		{
			get { return DateTime.Now.Ticks; }
		}

		#endregion
		
		#region Constructor
		
		/// <summary>
		/// Creates an instance of AgentRequest.
		/// </summary>
		public AgentRequest(SpiderUrl url, CookieCollection cookies)
		{
			int timeout = 30000;

			Int32.TryParse(ConfigurationManager.AppSettings["timeout"], out timeout);

			_request = WebRequest.Create(url.Target) as HttpWebRequest;
			_request.CookieContainer = new CookieContainer();
			_request.CookieContainer.Add(cookies);
			_request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			_request.AllowWriteStreamBuffering = true;
			_request.AuthenticationLevel = AuthenticationLevel.None;
			_request.AutomaticDecompression = DecompressionMethods.GZip;
			_request.ImpersonationLevel = TokenImpersonationLevel.Impersonation;
			_request.KeepAlive = true;
			_request.Method = "GET";
			_request.Referer = url.Referrer;
			_request.Timeout = timeout;
			_request.UseDefaultCredentials = true;
			_request.UserAgent = UserAgent;
//			_request.Headers = new WebHeaderCollection();
//			_request.Headers.Add("host", url.Target.OriginalString);
//			_request.Headers.Add("Accept-Language", "en-us,en;q=0.5");
//			_request.Headers.Add("Accept-Encoding", "gzip,deflate");
//			_request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8,q=0.7,*;q=0.7");
//			_request.Headers.Add("Cache-Control", "max-age=0");

			if (bool.Parse(ConfigurationManager.AppSettings["ignoreBadCertificates"]))
			{
				ServicePointManager.ServerCertificateValidationCallback +=
						delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
						{
							return true;
						};
			}
			
			_urls = new List<string>();
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Executes this request.
		/// </summary>
		/// <returns></returns>
		public HttpResponseInfo Execute()
		{
			HttpResponseInfo responseInfo;
			long startingTicks = Ticks;

			try
			{
				HttpWebResponse response = _request.GetResponse() as HttpWebResponse;

				long mark = Ticks;

				_elapsedTime = new TimeSpan(mark - startingTicks);

				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					HtmlDocument document = new HtmlDocument();
					document.LoadHtml(reader.ReadToEnd());

					_documentTitle = document.DocumentNode.SelectSingleNode("//title").InnerText.Trim();

					GatherUrls(document);
				}

				_request.CookieContainer = new CookieContainer();
				_request.CookieContainer.Add(response.Cookies);
				
				responseInfo = new HttpResponseInfo("200 OK");
			}
			catch (Exception ex)
			{
				responseInfo = new HttpResponseInfo(ex.Message);
			}
			
			return responseInfo;
		}
		
		/// <summary>
		/// Gets a list of SpiderUrls.
		/// </summary>
		/// <returns></returns>
		public IList<SpiderUrl> GetUrls()
		{
			if (_spiderUrls == null || _spiderUrls.Count == 0)
			{	
				_spiderUrls = new List<SpiderUrl>();
								
				foreach (string nextUrl in _urls)
				{
					Uri nextUri;
					
					if (Uri.TryCreate(nextUrl, UriKind.Absolute, out nextUri))
					{
						SpiderUrl nextSpiderUrl = new SpiderUrl(nextUri, _request.RequestUri.OriginalString);
						_spiderUrls.Add(nextSpiderUrl);
					}
					else if (Uri.TryCreate(String.Format("{0}{1}", _request.RequestUri.AbsolutePath, nextUrl), UriKind.Absolute, out nextUri))
					{
						SpiderUrl nextSpiderUrl = new SpiderUrl(nextUri, _request.RequestUri.OriginalString);
						_spiderUrls.Add(nextSpiderUrl);						
					}
				}
			}
			
			return _spiderUrls;
		}
		
		/// <summary>
		/// Gets all of the URLs on the page relevant to the current configuration.
		/// </summary>
		/// <param name="document"></param>
		private void GatherUrls(HtmlDocument document)
		{
			HtmlNodeCollection linksNodes = document.DocumentNode.SelectNodes("//a[@href]");
			foreach (HtmlNode linkNode in linksNodes)
			{
				_urls.Add(linkNode.Attributes["href"].Value.Trim());
			}

			if (bool.Parse(ConfigurationManager.AppSettings["checkImages"]))
			{
				HtmlNodeCollection imageNodes = document.DocumentNode.SelectNodes("//img[@src]");
				foreach (HtmlNode imageNode in imageNodes)
				{
					string srcAttr = imageNode.Attributes["src"].Value.Trim();

					if (!String.IsNullOrEmpty(srcAttr))
					{
						_urls.Add(srcAttr);
					}
				}
			}
		}
		
		#endregion
	}
}
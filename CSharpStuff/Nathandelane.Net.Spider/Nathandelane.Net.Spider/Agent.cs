using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using HtmlAgilityPack;

namespace Nathandelane.Net.Spider
{
	public class Agent
	{
		#region Fields

		private static long __id = 0L;

		private HttpWebRequest _webRequest;
		private CookieCollection _cookies;
		private TimeSpan _elapsedTime;
		private string _documentTitle;
		private List<string> _urls;
		private SpiderUrl _url;
		private string _message;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the cookies set by the agent.
		/// </summary>
		public CookieCollection Cookies
		{
			get { return _cookies; }
		}

		/// <summary>
		/// Gets the URLs collected by the agent.
		/// </summary>
		public List<string> Urls
		{
			get { return _urls; }
		}

		/// <summary>
		/// Gets the referrer.
		/// </summary>
		public Uri Referrer
		{
			get { return _url.Target; }
		}

		/// <summary>
		/// Gets the hash for this agent.
		/// </summary>
		public string Hash
		{
			get
			{
				string hash = String.Empty;

				byte[] buffer = Encoding.ASCII.GetBytes(_url.Target.ToString());
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

				if (ConfigurationManager.AppSettings["overrideUserAgent"] != null)
				{
					retVal = ConfigurationManager.AppSettings["overrideUserAgent"];
				}

				return retVal;
			}
		}

		/// <summary>
		/// Gets the number of ticks.
		/// </summary>
		private long Ticks
		{
			get { return DateTime.Now.Ticks; }
		}

		#endregion

		#region Constructors

		public Agent(SpiderUrl target, CookieCollection cookies)
		{
			_elapsedTime = new TimeSpan();
			_documentTitle = String.Empty;
			_urls = new List<string>();
			_url = target;

			_webRequest = WebRequest.Create(_url.Target) as HttpWebRequest;
			_webRequest.CookieContainer = new CookieContainer();
			_webRequest.CookieContainer.Add(cookies);

			SetupWebRequest();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Runs the agent.
		/// </summary>
		public void Run()
		{
			Agent.__id++;

			long startingTicks = Ticks;

			try
			{
				HttpWebResponse response = _webRequest.GetResponse() as HttpWebResponse;

				long mark = Ticks;

				_elapsedTime = new TimeSpan(mark - startingTicks);

				if (!_url.IsImage)
				{
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						HtmlDocument document = new HtmlDocument();
						document.LoadHtml(reader.ReadToEnd());

						if (!_url.IsImage && !_url.IsDocument)
						{
							_documentTitle = document.DocumentNode.SelectSingleNode("//title").InnerText.Trim();

							GatherUrls(document);
						}
					}
				}

				_cookies = response.Cookies;
				_message = "HTTP 200 OK";
			}
			catch (Exception ex)
			{
				_message = ex.Message;
			}
		}

		/// <summary>
		/// Sets up the web request for this agent.
		/// </summary>
		private void SetupWebRequest()
		{
			_webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			_webRequest.AllowWriteStreamBuffering = true;
			_webRequest.AuthenticationLevel = AuthenticationLevel.None;
			_webRequest.AutomaticDecompression = DecompressionMethods.GZip;
			_webRequest.CookieContainer = new CookieContainer();
			_webRequest.ImpersonationLevel = TokenImpersonationLevel.Impersonation;
			_webRequest.KeepAlive = true;
			_webRequest.Method = "GET";
			_webRequest.Referer = _url.Referrer;
			_webRequest.Timeout = 30000;
			_webRequest.UseDefaultCredentials = true;
			_webRequest.UserAgent = UserAgent;

			if (bool.Parse(ConfigurationManager.AppSettings["ignoreBadCertificates"]))
			{
				ServicePointManager.ServerCertificateValidationCallback +=
						delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
						{
							return true;
						};
			}
		}

		/// <summary>
		/// Gets all of the URLs on the page relevant to the current configuration.
		/// </summary>
		/// <param name="document"></param>
		private void GatherUrls(HtmlDocument document)
		{
			HtmlNodeCollection linksNodes = document.DocumentNode.SelectNodes("//a[@href]");
			foreach(HtmlNode linkNode in linksNodes)
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

		/// <summary>
		/// Provides a string representation for this agent.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("{0},{1},\"{2}\",\"{3}\",\"{4}\",\"{5}\",{6}", __id, DateTime.Now.ToString("hh:mm:ss.fff"), _message, _url.Target, _url.Referrer, _documentTitle, _elapsedTime);
		}

		#endregion
	}
}

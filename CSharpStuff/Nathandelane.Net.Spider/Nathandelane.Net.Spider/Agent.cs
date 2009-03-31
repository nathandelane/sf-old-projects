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

		public CookieCollection Cookies
		{
			get { return _cookies; }
		}

		public List<string> Urls
		{
			get { return _urls; }
		}

		public Uri Referrer
		{
			get { return new Uri(_url.Target); }
		}

		public string Hash
		{
			get
			{
				string hash = String.Empty;

				byte[] buffer = Encoding.ASCII.GetBytes(_url.Target);
				SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();

				hash = BitConverter.ToString(provider.ComputeHash(buffer)).Replace("-", String.Empty);

				return hash;
			}
		}

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

		#region Public Methods

		public void Run()
		{
			__id++;

			long startingTicks = Ticks;

			try
			{
				HttpWebResponse response = _webRequest.GetResponse() as HttpWebResponse;

				long mark = Ticks;

				_elapsedTime = new TimeSpan(mark - startingTicks);

				HtmlDocument document = new HtmlDocument();
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					document.LoadHtml(reader.ReadToEnd());

					if (!_url.IsImage && !_url.IsDocument)
					{
						_documentTitle = document.DocumentNode.SelectSingleNode("//title").InnerText.Trim();

						GatherUrls(document);
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

		#endregion

		#region Private Methods

		private void SetupWebRequest()
		{
			_webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			_webRequest.AllowAutoRedirect = bool.Parse(ConfigurationManager.AppSettings["allowAutoRedirects"]);
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

		private void GatherUrls(HtmlDocument document)
		{
			HtmlNodeCollection linksNodes = document.DocumentNode.SelectNodes("//a[@href]");
			foreach(HtmlNode linkNode in linksNodes)
			{
				_urls.Add(linkNode.Attributes["href"].Value.Trim());
			}

			if (bool.Parse(ConfigurationManager.AppSettings["checkImages"]))
			{
				HtmlNodeCollection imageNodes = document.DocumentNode.SelectNodes("//a[@href]");
				foreach (HtmlNode imageNode in imageNodes)
				{
					_urls.Add(imageNode.Attributes["src"].Value.Trim());
				}
			}
		}

		#endregion

		#region Override Methods

		public override string ToString()
		{
			// Id, Message, Target, Referrer, Title, Time
			return String.Format("{0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",{5}", __id, _message, _url.Target, _url.Referrer, _documentTitle, _elapsedTime);
		}

		#endregion
	}
}

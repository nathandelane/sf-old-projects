using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Nathandelane.Net.Spider
{
	public class Agent
	{
		#region Fields

		private HttpWebRequest _webRequest;
		private TimeSpan _elapsedTime;
		private string _documentTitle;
		private List<string> _urls;
		private Uri _referrer;

		#endregion

		#region Properties

		public List<string> Urls
		{
			get { return _urls; }
		}

		public Uri Referrer
		{
			get { return _referrer; }
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

		public Agent(SpiderUrl address)
		{
			_webRequest = null;
			_elapsedTime = new TimeSpan();
			_documentTitle = String.Empty;
			_urls = new List<string>();
			_referrer = new Uri(address.Target, UriKind.Absolute);

			SetupWebRequest(address);
		}

		#endregion

		#region Public Methods

		public void Run()
		{
			long startingTicks = Ticks;

			HttpWebResponse response = _webRequest.GetResponse() as HttpWebResponse;

			long mark = Ticks;

			_elapsedTime = new TimeSpan(mark - startingTicks);

			XDocument xDocument = new XDocument();
			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
				document.LoadHtml(reader.ReadToEnd());
				document.OptionOutputAsXml = true;

				using (StringWriter sw = new StringWriter())
				{
					document.Save(sw);

					xDocument = XDocument.Parse(sw.GetStringBuilder().ToString());
				}
			}

			_documentTitle = (from el in xDocument.Root.Descendants()
							 where el.Name.LocalName.Equals("title")
							 select el).FirstOrDefault<XElement>().Value;

			GatherUrls(xDocument);
		}

		#endregion

		#region Private Methods

		private void SetupWebRequest(SpiderUrl uri)
		{
			_webRequest = WebRequest.Create(uri.Target) as HttpWebRequest;
			_webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			_webRequest.AllowAutoRedirect = bool.Parse(ConfigurationManager.AppSettings["allowAutoRedirects"]);
			_webRequest.AllowWriteStreamBuffering = true;
			_webRequest.AuthenticationLevel = AuthenticationLevel.None;
			_webRequest.AutomaticDecompression = DecompressionMethods.GZip;
			_webRequest.Connection = "keep-alive";
			_webRequest.CookieContainer = new CookieContainer();
			_webRequest.ImpersonationLevel = TokenImpersonationLevel.Impersonation;
			_webRequest.KeepAlive = true;
			_webRequest.Method = "GET";
			_webRequest.Referer = uri.Referrer;
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

		private void GatherUrls(XDocument document)
		{
			var links = from el in document.Root.Descendants()
						where el.Name.LocalName.Equals("a")
						&& el.Attribute(XName.Get("href")) != null
						select el.Attribute(XName.Get("href")).Value;

			_urls.AddRange(links);

			if (bool.Parse(ConfigurationManager.AppSettings["checkImages"]))
			{
				var images = from el in document.Root.Descendants()
							 where el.Name.LocalName.Equals("img")
							 && el.Attribute(XName.Get("src")) != null
							 select el.Attribute(XName.Get("src")).Value;

				_urls.AddRange(images);
			}
		}

		#endregion
	}
}

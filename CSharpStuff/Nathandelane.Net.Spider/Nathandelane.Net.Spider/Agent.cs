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
		private string _message;
		private string _contentType;
		private List<string> _urls;
		private SpiderUrl _url;
		private long _responseSize;
		private IList<MimeType> _mimeTypesToIgnore;
		private IList<string> _linkHrefPatternsToIgnore;
		private IDictionary<string, bool> _contentTypesToInclude;
		private MimeType _mimeType;
		private bool _checked;

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

				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["overrideUserAgent"]))
				{
					retVal = ConfigurationManager.AppSettings["overrideUserAgent"];
				}

				return retVal;
			}
		}

		/// <summary>
		/// Gets the content type value of the response.
		/// </summary>
		public string ContentType
		{
			get { return _contentType; }
		}

		/// <summary>
		/// Gets the Agent's request object.
		/// </summary>
		public HttpWebRequest Request
		{
			get { return _webRequest; }
		}

		/// <summary>
		/// Gets the MIME Type of the response.
		/// </summary>
		public MimeType MimeType
		{
			get { return _mimeType; }
		}

		/// <summary>
		/// Gets whether this request was checked.
		/// </summary>
		public bool Checked
		{
			get { return _checked; }
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

		public Agent(SpiderUrl target, CookieCollection cookies, IList<string> linkHrefPatternsToIgnore, IList<MimeType> mimeTypesToIgnore, IDictionary<string, bool> contentTypesToInclude)
		{
			_linkHrefPatternsToIgnore = linkHrefPatternsToIgnore;
			_mimeTypesToIgnore = mimeTypesToIgnore;
			_contentTypesToInclude = contentTypesToInclude;

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
			_checked = false;

			Agent.__id++;

			long startingTicks = Ticks;

			try
			{
				HttpWebResponse response = null;
				HttpWebRequest headRequest = _webRequest.Clone();

				headRequest.Method = "HEAD";

				response = headRequest.GetResponse() as HttpWebResponse;

				_contentType = (response.ContentType.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)[0]).Trim();
				_mimeType = ContentTypes.GetMimeType(_contentType);
				_responseSize = response.ContentLength;

				response.Close();
				response = null;

				if (!_mimeTypesToIgnore.Contains(_mimeType) && _contentTypesToInclude.ContainsKey(_contentType) && _contentTypesToInclude[_contentType])
				{
					_checked = true;
					_webRequest.Method = "GET";

					response = _webRequest.GetResponse() as HttpWebResponse;

					long mark = Ticks;

					_elapsedTime = new TimeSpan(mark - startingTicks);

					if (MimeType == Spider.MimeType.Text && ContentType.Equals("text/html", StringComparison.InvariantCultureIgnoreCase))
					{
						using (StreamReader reader = new StreamReader(response.GetResponseStream()))
						{
							HtmlDocument document = new HtmlDocument();
							string responseString = reader.ReadToEnd();

							_responseSize = responseString.Length;

							document.LoadHtml(responseString);

							_documentTitle = document.DocumentNode.SelectSingleNode("//title").InnerText.Trim();

							GatherUrls(document);
						}
					}

					_cookies = response.Cookies;
					_message = "HTTP 200 OK";
				}
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
			int timeout = 30000;

			Int32.TryParse(ConfigurationManager.AppSettings["timeout"], out timeout);

			_webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			_webRequest.AllowWriteStreamBuffering = true;
			_webRequest.AuthenticationLevel = AuthenticationLevel.None;
			_webRequest.AutomaticDecompression = DecompressionMethods.GZip;
			_webRequest.CookieContainer = new CookieContainer();
			_webRequest.ImpersonationLevel = TokenImpersonationLevel.Impersonation;
			_webRequest.KeepAlive = true;
			_webRequest.Method = "GET";
			_webRequest.Referer = _url.Referrer;
			_webRequest.Timeout = timeout;
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

			foreach (HtmlNode linkNode in linksNodes)
			{
				if (_linkHrefPatternsToIgnore.Count > 0)
				{
					bool patternFound = false;

					foreach (string nextHrefPattern in _linkHrefPatternsToIgnore)
					{
						if (linkNode.Attributes["href"].Value.IndexOf(nextHrefPattern, StringComparison.InvariantCultureIgnoreCase) > -1)
						{
							patternFound = true;
							break;
						}
					}

					if (!patternFound)
					{
						_urls.Add(linkNode.Attributes["href"].Value.Trim());
					}
				}
				else
				{
					_urls.Add(linkNode.Attributes["href"].Value.Trim());
				}
			}
		}

		/// <summary>
		/// Provides a string representation for this agent.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("{0},\"{10}\",{1},\"{2}\",\"{3}\",\"{4}\",\"{5}\",{6},{7},\"{8}\",\"{9}\"", __id, DateTime.Now.ToString("hh:mm:ss.fff"), _message, _url.Target, _url.Referrer, _documentTitle, _elapsedTime, _responseSize, _contentType, _mimeType, _checked);
		}

		#endregion
	}
}

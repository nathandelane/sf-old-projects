using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace Nathandelane.Net.Spider
{
	public class Agent
	{
		#region Fields

		private string _data;
		private string[] _urls;
		private string _root;
		private string _referringUrl;
		private string _pageTitle;
		private int _timeOut;
		private long _id;
		private HttpWebRequest _request;
		private HttpWebResponse _response;
		private Settings _settings;
		private Exception _ex;
		private CookieCollection _cookies;
		private WebHeaderCollection _headers;

		#endregion

		#region Properties

		public string[] Urls
		{
			get { return _urls; }
		}

		public string Root
		{
			get { return _root; }
			set { _root = value; }
		}

		public string ReferringUrl
		{
			get { return _referringUrl; }
			set { _referringUrl = value; }
		}

		public int TimeOut
		{
			get { return _timeOut; }
			set { _timeOut = value; }
		}

		public long Id
		{
			get { return _id; }
		}

		public HttpWebRequest Request
		{
			get { return _request; }
			set { _request = value; }
		}

		public HttpWebResponse Response
		{
			get { return _response; }
		}

		public CookieCollection Cookies
		{
			get { return _cookies; }
		}

		public WebHeaderCollection Headers
		{
			get { return _headers; }
		}

		#endregion

		#region Constructors

		public Agent(SpiderUrl url, long id, Settings settings, CookieCollection cookies, WebHeaderCollection headers)
		{
			_pageTitle = String.Empty;
			_urls = new string[0];
			_referringUrl = url.ReferringUrl;
			_root = url.Url.Replace("&amp;", "&");
			_id = id;
			_settings = settings;
			_cookies = cookies;
			_headers = headers;
		}

		#endregion

		#region Public Methods

		public void Run()
		{
			PerformRequest();
		}

		public override string ToString()
		{
			return String.Format("{0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\"", _id, ((_response == null) ? _ex.Message : GetStatusMessageFor(_response.StatusCode)), _root, (String.IsNullOrEmpty(_pageTitle) ? "null" : _pageTitle), _referringUrl);
		}

		public string Hash()
		{
			string resultingHash = String.Empty;

			SHA1Managed sha1 = new SHA1Managed();
			byte[] bytes = sha1.ComputeHash(ASCIIEncoding.ASCII.GetBytes(String.Format("{0}", _root).ToCharArray()));
			resultingHash = ASCIIEncoding.ASCII.GetString(bytes);

			return resultingHash;
		}

		#endregion

		#region Private Methods

		private void PerformRequest()
		{
			_request = WebRequest.CreateDefault(new Uri(_root)) as HttpWebRequest;
			_request.Accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
			_request.ImpersonationLevel = TokenImpersonationLevel.Impersonation;
			_request.KeepAlive = true;
			_request.Referer = _referringUrl;
			_request.Timeout = int.Parse(_settings["timeOut"]);
			_request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; Media Center PC 5.0; .NET CLR 1.1.4322; Nathandelane.Net.Spider)";

			CookieContainer cookies = new CookieContainer(100);
			cookies.Add(_cookies);
			_request.CookieContainer = cookies;

			try
			{
				_response = _request.GetResponse() as HttpWebResponse;
				_cookies = _response.Cookies;

				using (StreamReader reader = new StreamReader(_response.GetResponseStream()))
				{
					_data = reader.ReadToEnd();
				}

				SetPageTitleAndLinkList();
			}
			catch (Exception ex)
			{
				_ex = ex;
			}
		}

		private XDocument HtmlToXDocument(HtmlAgilityPack.HtmlDocument document)
		{
			XDocument xmlDocument = null;

			using (StringWriter sw = new StringWriter())
			{
				document.OptionOutputAsXml = true;
				document.Save(sw);
				xmlDocument = XDocument.Parse(sw.GetStringBuilder().ToString());
			}

			return xmlDocument;
		}

		private void SetPageTitleAndLinkList()
		{
			HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
			document.LoadHtml(_data);

			XDocument xmlDoc = HtmlToXDocument(document);

			var titleNode = from node in xmlDoc.Descendants()
							where node.Name.LocalName.Equals("title")
							select node as XElement;

			_pageTitle = titleNode.FirstOrDefault<XElement>().Value.Replace("\n", String.Empty).Replace("\r", String.Empty).Replace("\t", String.Empty);

			var links = from node in xmlDoc.Descendants()
						where node.Name.LocalName.Equals("a")
						&& !String.IsNullOrEmpty((node.Attribute(XName.Get("href"))).Value)
						select node as XElement;

			List<string> hrefs = new List<string>();

			foreach (XElement nextLink in links)
			{
				string normalizedLink = NormalizeLinkHref(nextLink.Attribute("href").Value);

				bool add = true;
				if (bool.Parse(_settings["stayWithinWebSite"]))
				{
					if (normalizedLink.Contains(_settings["webSite"]))
					{
						add = true;
					}
					else
					{
						add = false;
					}
				}

				if (!String.IsNullOrEmpty(normalizedLink) && !(normalizedLink.ToLower()).Contains("javascript") && !normalizedLink.Contains("mailto:") && add)
				{
					hrefs.Add(normalizedLink);
				}

			}

			_urls = hrefs.ToArray();
		}

		private string NormalizeLinkHref(string linkHref)
		{
			string result = linkHref;

			linkHref = linkHref.Replace("../", String.Empty);
			linkHref = linkHref.Replace("&amp;", "&");

			if (!linkHref.StartsWith("http://") && !linkHref.StartsWith("https://"))
			{
				if (!linkHref.StartsWith("/"))
				{
					if (!HasZone(linkHref))
					{
						string zone = GetZoneFor(linkHref.Split(new string[] { ".aspx" }, StringSplitOptions.RemoveEmptyEntries)[0]);
						result = String.Format("{0}/{1}/{2}", _settings["startingUrl"], zone, linkHref);
					}
					else
					{
						result = String.Format("{0}/{1}", _settings["startingUrl"], linkHref);
					}
				}
				else
				{
					if (!HasZone(linkHref.Substring(1)))
					{
						string zone = GetZoneFor(linkHref.Split(new string[] { ".aspx" }, StringSplitOptions.RemoveEmptyEntries)[0]);
						result = String.Format("{0}/{1}{2}", _settings["startingUrl"], zone, linkHref);
					}
					else
					{
						result = String.Format("{0}{1}", _settings["startingUrl"], linkHref);
					}
				}
			}

			return result;
		}

		private bool HasZone(string href)
		{
			bool hasZone = false;

			if(href.StartsWith("automotive"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("Community"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("corporate"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("dealer"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("error"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("expresslane"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("finance"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("forums"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("green"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("honda"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("inventory"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("myVehix"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("research"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("rss"))
			{
				hasZone = true;
			}
			else if(href.StartsWith("sell-your-car"))
			{
				hasZone = true;
			}
			else if (href.StartsWith("video"))
			{
				hasZone = true;
			}

			return hasZone;
		}

		private string GetZoneFor(string aspxFileName)
		{
			string zone = String.Empty;

			if (aspxFileName.StartsWith("/"))
			{
				aspxFileName = aspxFileName.Substring(1);
			}

			if (true/*aspxFileName.Contains(".aspx")*/)
			{
				string pageName = (aspxFileName.Split(new string[] { ".aspx" }, StringSplitOptions.RemoveEmptyEntries)[0]).ToLower();

				if (!String.IsNullOrEmpty(_settings.GetZoneFor(pageName)))
				{
					zone = _settings.GetZoneFor(pageName);
				}
			}

			return zone;
		}

		private string GetStatusMessageFor(HttpStatusCode statusCode)
		{
			string message = String.Empty;

			switch (statusCode)
			{
				case HttpStatusCode.Continue:
					message = "Continue HTTP 100";
					break;
				case HttpStatusCode.SwitchingProtocols:
					message = "Switching Protocols HTTP 101";
					break;
				case HttpStatusCode.OK:
					message = "OK HTTP 200";
					break;
				case HttpStatusCode.Created:
					message = "Creates HTTP 201";
					break;
				case HttpStatusCode.Accepted:
					message = "Accepted HTTP 202";
					break;
				case HttpStatusCode.NonAuthoritativeInformation:
					message = "Non-Authoritative Information HTTP 203";
					break;
				case HttpStatusCode.NoContent:
					message = "No Content HTTP 204";
					break;
				case HttpStatusCode.ResetContent:
					message = "Reset Content HTTP 205";
					break;
				case HttpStatusCode.PartialContent:
					message = "Partial Content HTTP 206";
					break;
				case HttpStatusCode.MultipleChoices:
					message = "Multiple Choices HTTP 300";
					break;
				case HttpStatusCode.MovedPermanently:
					message = "Moved Permanently HTTP 301";
					break;
				case HttpStatusCode.Redirect:
					message = "Redirect HTTP 302";
					break;
				default:
					message = "OK HTTP 200";
					break;
			}

			return message;
		}

		#endregion
	}
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Timers;
using System.Xml;
using HtmlAgilityPack;

namespace Nathandelane.Net.Spider
{
	internal class Agent
	{
		#region Fields

		private string _data;
		private string[] _urls;
		private string[] _images;
		private string _root;
		private string _referringUrl;
		private string _pageTitle;
		private string _userAgent;
		private bool _searchValueFound;
		private int _timeOut;
		private long _id;
		private long _elapsedMillis;
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

		public string[] Images
		{
			get { return _images; }
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

		public string UserAgent
		{
			get
			{
				if (String.IsNullOrEmpty(_userAgent))
				{
					_userAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; Media Center PC 5.0; .NET CLR 1.1.4322; Nathandelane.Net.Spider)";

					if (_settings.ContainsKey("overrideUserAgent"))
					{
						_userAgent = _settings["overrideUserAgent"];
					}
				}

				return _userAgent;
			}
		}

		public bool SearchValueFound
		{
			get { return _searchValueFound; }
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

		public double ElapsedTime
		{
			get { return (new TimeSpan(_elapsedMillis)).TotalSeconds; }
		}

		#endregion

		#region Constructors

		public Agent(SpiderUrl url, long id, Settings settings, CookieCollection cookies, WebHeaderCollection headers)
		{
			_pageTitle = String.Empty;
			_urls = new string[0];
			_images = new string[0];
			_referringUrl = url.ReferringUrl;
			_root = url.Url.Replace("&amp;", "&");
			_id = id;
			_searchValueFound = false;
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
			return String.Format("{0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5:0.00}\"", _id, ((_response == null) ? _ex.Message : GetStatusMessageFor(_response.StatusCode)), _root, (String.IsNullOrEmpty(_pageTitle) ? "null" : _pageTitle), _referringUrl, ElapsedTime);
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
			_request.Timeout = _settings.ContainsKey("timeOut") ? int.Parse(_settings["timeOut"]) : (30 * 1000);
			_request.UserAgent = UserAgent;

			if (bool.Parse(_settings["igoreCertificateErrors"]))
			{
				ServicePointManager.ServerCertificateValidationCallback +=
					delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
					{
						return true;
					};
			}

			CookieContainer cookies = new CookieContainer(100);
			cookies.Add(_cookies);
			_request.CookieContainer = cookies;

			try
			{
				long startTime = DateTime.Now.Ticks;
	
				_response = _request.GetResponse() as HttpWebResponse;
				_elapsedMillis = DateTime.Now.Ticks - startTime;
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

		private void SetPageTitleAndLinkList()
		{
			if (_settings.ContainsKey("searchForText"))
			{
				string[] values = _settings["searchForText"].Split(new char[] { '|' });
				int valuesIndex = 0;

				while (valuesIndex < values.Length && !_searchValueFound)
				{
					if (_data.Contains(values[valuesIndex]))
					{
						_searchValueFound = true;
					}

					valuesIndex++;
				}
			}

			HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
			document.LoadHtml(_data);
			_pageTitle = (((document.DocumentNode.SelectSingleNode("//title")).InnerText).Trim(new char[] { '\n', '\r' })).Trim();

			HtmlNodeCollection links = document.DocumentNode.SelectNodes("//a[@href]");
			List<string> hrefs = new List<string>();

			foreach (HtmlNode nextLink in links)
			{
				string normalizedLink = NormalizeLinkHref(nextLink.Attributes["href"].Value);

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

			if (bool.Parse(_settings["checkImages"]))
			{
				HtmlNodeCollection images = document.DocumentNode.SelectNodes("//img[@src]");
				List<string> srcs = new List<string>();

				foreach (HtmlNode nextImage in images)
				{
					string normalizedImage = NormalizeLinkSrc(nextImage.Attributes["src"].Value);

					bool add = true;
					if (bool.Parse(_settings["stayWithinWebSite"]))
					{
						if (normalizedImage.Contains(_settings["webSite"]))
						{
							add = true;
						}
						else
						{
							add = false;
						}
					}

					if (add)
					{
						srcs.Add(normalizedImage);
					}
				}

				_images = srcs.ToArray();
			}

			_urls = hrefs.ToArray();
		}

		private string NormalizeLinkSrc(string imageSrc)
		{
			string result = imageSrc;

			imageSrc = imageSrc.Replace("../", String.Empty);
			imageSrc = imageSrc.Replace("&amp;", "&");

			if (!imageSrc.StartsWith("http://") && !imageSrc.StartsWith("https://"))
			{
				if (!imageSrc.StartsWith("/"))
				{
					result = String.Format("{0}/{1}", _settings["startingUrl"], imageSrc);
				}
				else
				{
					result = String.Format("{0}{1}", _settings["startingUrl"], imageSrc);
				}
			}

			string[] parts = result.Split(new string[] { "://" }, StringSplitOptions.RemoveEmptyEntries);

			while (parts[1].Contains("//"))
			{
				parts[1] = parts[1].Replace("//", "/");
			}

			result = String.Concat(parts[0], "://", parts[1]);

			return result;
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

			string[] parts = result.Split(new string[] { "://" }, StringSplitOptions.RemoveEmptyEntries);

			while (parts[1].Contains("//"))
			{
				parts[1] = parts[1].Replace("//", "/");
			}

			result = String.Concat(parts[0], "://", parts[1]);

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
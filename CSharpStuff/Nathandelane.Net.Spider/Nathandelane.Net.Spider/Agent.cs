using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml;
using HtmlAgilityPack;
using System.IO;

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

		#endregion

		#region Constructors

		public Agent(SpiderUrl url, long id, Settings settings)
		{
			_pageTitle = String.Empty;
			_urls = new string[0];
			_referringUrl = url.ReferringUrl;
			_root = url.Url;
			_id = id;
			_settings = settings;
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
			byte[] bytes = sha1.ComputeHash(ASCIIEncoding.ASCII.GetBytes(String.Format("{0}:{1}", _referringUrl, _root).ToCharArray()));
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

			try
			{
				_response = _request.GetResponse() as HttpWebResponse;

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

			_urls = hrefs.ToArray();
		}

		private string NormalizeLinkHref(string linkHref)
		{
			string result = linkHref;

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

			if (linkHref.Contains("&amp;"))
			{
				linkHref = linkHref.Replace("&amp;", "&");
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

			if (aspxFileName.Contains(".aspx"))
			{
				string switchOn = aspxFileName.Split(new string[] { ".aspx" }, StringSplitOptions.RemoveEmptyEntries)[0];

				switch (switchOn)
				{
					case "Article":
						zone = "automotive";
						break;
					case "Blogs":
						zone = "Community";
						break;
					case "road-trips":
						zone = "Community";
						break;
					case "findadealer":
						zone = "dealer";
						break;
					case "AutoLoans":
						zone = "finance";
						break;
					case "calculators":
						zone = "finance";
						break;
					case "creditReport":
						zone = "finance";
						break;
					case "insurance":
						zone = "finance";
						break;
					case "IsLeasingBetter":
						zone = "finance";
						break;
					case "LeasingCenter":
						zone = "finance";
						break;
					case "purchaseOptions":
						zone = "finance";
						break;
					case "green-articles":
						zone = "green";
						break;
					case "green-vehicles":
						zone = "green";
						break;
					case "green-video":
						zone = "green";
						break;
					case "search-green-vehicles":
						zone = "green";
						break;
					case "Both":
						zone = "inventory";
						break;
					case "ConsumerReviewComments":
						zone = "inventory";
						break;
					case "Details":
						zone = "inventory";
						break;
					case "ExpertReviews":
						zone = "inventory";
						break;
					case "New":
						zone = "inventory";
						break;
					case "NotFound":
						zone = "inventory";
						break;
					case "Search":
						zone = "inventory";
						break;
					case "Used":
						zone = "inventory";
						break;
					case "VehicleDetails":
						zone = "inventory";
						break;
					case "ChangeEmail":
						zone = "myVehix";
						break;
					case "ChangePassword":
						zone = "myVehix";
						break;
					case "ForgotPassword":
						zone = "myVehix";
						break;
					case "Login":
						zone = "myVehix";
						break;
					case "loginPopup":
						zone = "myVehix";
						break;
					case "Logout":
						zone = "myVehix";
						break;
					case "MembershipStatus":
						zone = "myVehix";
						break;
					case "MyAds":
						zone = "myVehix";
						break;
					case "MyAlerts":
						zone = "myVehix";
						break;
					case "MyCars":
						zone = "myVehix";
						break;
					case "MyProfile":
						zone = "myVehix";
						break;
					case "MySummary":
						zone = "myVehix";
						break;
					case "Registration":
						zone = "myVehix";
						break;
					case "savePopup":
						zone = "myVehix";
						break;
					case "UpgradeAd":
						zone = "myVehix";
						break;
					case "available-trims":
						zone = "research";
						break;
					case "browseVehicles":
						zone = "research";
						break;
					case "build":
						zone = "research";
						break;
					case "build-details":
						zone = "research";
						break;
					case "buildYourCar":
						zone = "research";
						break;
					case "buyingGuides":
						zone = "research";
						break;
					case "carfax":
						zone = "research";
						break;
					case "category":
						zone = "research";
						break;
					case "categoryBodyStyle":
						zone = "research";
						break;
					case "categoryBodyStyleVehicleList":
						zone = "research";
						break;
					case "categoryBodyStyleVehicleListIframe":
						zone = "research";
						break;
					case "categoryMake":
						zone = "research";
						break;
					case "category-results":
						zone = "research";
						break;
					case "certifiedPre-owned":
						zone = "research";
						break;
					case "compare-details":
						zone = "research";
						break;
					case "comparisonPrint":
						zone = "research";
						break;
					case "comparisonSelectVehicle":
						zone = "research";
						break;
					case "comparisonTool":
						zone = "research";
						break;
					case "consumer-review-comments":
						zone = "research";
						break;
					case "consumer-review-details":
						zone = "research";
						break;
					case "expert-review-details":
						zone = "research";
						break;
					case "free-price-quote":
						zone = "research";
						break;
					case "free-price-quote-thank-you":
						zone = "research";
						break;
					case "gallery-details":
						zone = "research";
						break;
					case "make":
						zone = "research";
						break;
					case "net-cars":
						zone = "research";
						break;
					case "previewDetails":
						zone = "research";
						break;
					case "previews":
						zone = "research";
						break;
					case "remoteBuildYourCar":
						zone = "research";
						break;
					case "remotComparisonTool":
						zone = "research";
						break;
					case "reviewDetails":
						zone = "research";
						break;
					case "review-details":
						zone = "research";
						break;
					case "reviewDetailsConsumer":
						zone = "research";
						break;
					case "reviews":
						zone = "research";
						break;
					case "safety":
						zone = "research";
						break;
					case "safetyDetails":
						zone = "research";
						break;
					case "safetyRatings":
						zone = "research";
						break;
					case "showrooms":
						zone = "research";
						break;
					case "showroomsVehicleDetails":
						zone = "research";
						break;
					case "specification-details":
						zone = "research";
						break;
					case "stateDMVInfo":
						zone = "research";
						break;
					case "topTenDefault":
						zone = "research";
						break;
					case "topTenLists":
						zone = "research";
						break;
					case "usedVehicleValueDetails":
						zone = "research";
						break;
					case "usedVehicleValues":
						zone = "research";
						break;
					case "vehicleSpecifications":
						zone = "research";
						break;
					case "VehicleSpecificationsPopup":
						zone = "research";
						break;
					case "VehicleSpecificationsVehicleDetails":
						zone = "research";
						break;
					case "video-details":
						zone = "research";
						break;
					case "ICOTY":
						zone = "research";
						break;
					case "ICOTY/CarOfTheYear":
						zone = "research";
						break;
					case "ICOTY/Default":
						zone = "research";
						break;
					case "ICOTY/MostAthletic":
						zone = "research";
						break;
					case "ICOTY/MostCompatible":
						zone = "research";
						break;
					case "ICOTY/MostDependable":
						zone = "research";
						break;
					case "ICOTY/MostResourceful":
						zone = "research";
						break;
					case "ICOTY/MostRespectful":
						zone = "research";
						break;
					case "ICOTY/MostSexAppeal":
						zone = "research";
						break;
					case "ICOTY/MostSpirited":
						zone = "research";
						break;
					case "ICOTY/MostVersatile":
						zone = "research";
						break;
					case "create-free-listing":
						zone = "sell-your-car";
						break;
					case "create-listing":
						zone = "sell-your-car";
						break;
					case "create-listing-purchase":
						zone = "sell-your-car";
						break;
					case "create-listing-step-2":
						zone = "sell-your-car";
						break;
					case "create-listing-step-3":
						zone = "sell-your-car";
						break;
					case "create-listing-step-4":
						zone = "sell-your-car";
						break;
					case "create-listing-step-5":
						zone = "sell-your-car";
						break;
					case "create-listing-thankyou":
						zone = "sell-your-car";
						break;
					case "create-listing-upgrade":
						zone = "sell-your-car";
						break;
					case "create-listing-upgrade-photos":
						zone = "sell-your-car";
						break;
					case "editAd":
						zone = "sell-your-car";
						break;
					case "editAdPhotos":
						zone = "sell-your-car";
						break;
					case "syndicatedVideo":
						zone = "video";
						break;
					default:
						break;
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

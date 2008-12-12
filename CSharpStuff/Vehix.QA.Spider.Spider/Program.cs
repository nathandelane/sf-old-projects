using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Threading;

namespace Vehix.QA.Spider.Spider
{
	class Program
	{
		/// <summary>
		/// Inner class for tracking Urls that we've seen.
		/// </summary>
		class UrlTracking
		{
			public static int __counter = 0;
			private int _hits;

			public int Id { get; private set; }
			public string Url { get; private set; }
			public string ReferringUrl { get; private set; }
			public int Hits { get { return _hits; } }
			public void Hit() { Interlocked.Increment(ref this._hits); }
			public Exception Ex { get; private set; }

			public UrlTracking(SpiderUrl url)
			{				
				this.Url = url.Url;
				this.ReferringUrl = url.ReferringUrl;
				this._hits = 1;
				this.Id = Interlocked.Increment(ref __counter);
			}

			public void Background_StartSpider(object unused)
			{
				// This method will get called in a ThreadPool thread
				string url = this.Url.Replace("&amp;", "&");

				if (!url.StartsWith(__settings["startingUrl"]))
				{
					string regularExpression = "^(automotive|Community|corporate|dealer|error|expresslane|finance|forums|green|honda|inventory|myVehix|research|rss|sell-your-car|video|freePriceQuote){1}";
					Regex zoneRegex = new Regex(regularExpression);
					if (zoneRegex.IsMatch(url))
					{
						url = String.Format("{0}{1}{2}", __settings["startingUrl"], ((url.StartsWith("/")) ? String.Empty : "/"), url);
					}
					else // Need to append zone to beginning
					{
						string zone = GetZoneFor(url);
						url = String.Format("{0}/{1}/{2}{3}", __settings["startingUrl"], zone, ((url.StartsWith("/")) ? String.Empty : "/"), url);
					}

					url = url.Replace("////", "/");
					url = url.Replace("///", "/");
				}

				if (!String.IsNullOrEmpty(url) && !url.Contains("ad.doubleclick.net") && !(url.ToLower()).Contains("javascript") && !url.Contains("nada") && !url.Contains("mailto:"))
				{
					HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
					request.CookieContainer = new CookieContainer();
					request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
					request.Timeout = int.Parse(__settings["timeOut"]);
					request.AllowAutoRedirect = true;
					request.UserAgent = __userAgent;
					request.Referer = this.ReferringUrl;

					if (__cookies != null)
					{
						foreach (Cookie cookie in __cookies)
						{
							request.CookieContainer.Add(cookie);
						}
					}
					// Make an async call to get the response... frees up the current thread
					// to do other work.
					request.BeginGetResponse(new AsyncCallback(Background_HandleAsyncResult), request);
				}
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

			private void Background_HandleAsyncResult(IAsyncResult ar)
			{
				// This method will get called in a ThreadPool thread
				// when the server has responded to the webrequest.
				HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
				HttpWebResponse response = null;
				try
				{
					response = request.EndGetResponse(ar) as HttpWebResponse;

					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						__data = reader.ReadToEnd();
					}

					string[] links = GetAllAnchorHrefs();
					string pageTitle = GetPageTitle();
					var fetch = from u in links
											where !__regex.IsMatch(u)
											select u;
					var except = links.Except(fetch);
					if (except.Count() > 0)
					{
						lock (__writeLock)
						{
							WriteLogMessage(String.Concat(Environment.NewLine, "Excluded urls:"), false);
						}
						foreach (string u in except)
						{
							lock (__writeLock)
							{
								WriteLogMessage(String.Format("\t{0}", u), false);
							}
						}
					}
					UrlTracking t = null;
					foreach (string u in fetch)
					{
						lock (__visitedPages)
						{
							SpiderUrl spiderUrl = new SpiderUrl(u, request.RequestUri.AbsoluteUri);
							if (__visitedPages.TryGetValue(spiderUrl, out t))
							{
								t.Hit();
							}
							else
							{
								__visitedPages.Add(new SpiderUrl(u, request.RequestUri.AbsoluteUri), t = new UrlTracking(spiderUrl));
								// Start spidering in a background thread when it becomes 
								// available...
								ThreadPool.QueueUserWorkItem(new WaitCallback(t.Background_StartSpider));
							}
						}
					}
					WriteLogMessage(String.Format("{2} Request for {0} passed: {1} (title: {3}, actual url: {4}, referrer: {5})", this.Url, response.StatusCode.ToString(), this.Id, ((String.IsNullOrEmpty(pageTitle)) ? "null" : pageTitle), request.RequestUri.AbsoluteUri, this.ReferringUrl), true);
					Interlocked.Increment(ref __goodUrls);
				}
				catch (Exception ex)
				{
					this.Ex = ex;
					WriteLogMessage(String.Format("{2} Request for {0} passed: {1} (actual url: {3}, referrer: {4})", this.Url, ((response == null) ? "null" : response.StatusCode.ToString()), this.Id, request.RequestUri.AbsoluteUri, this.ReferringUrl), true);
					Interlocked.Increment(ref __badUrls);
				}
				finally
				{
					if (response != null) response.Close();
				}
				Interlocked.Increment(ref __fetchedUrls);
			}

			private string GetPageTitle()
			{
				string pageTitle = String.Empty;

				HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(__data);
				HtmlAgilityPack.HtmlNodeCollection titleNodes = doc.DocumentNode.SelectNodes("//title");

				if (titleNodes != null & titleNodes.Count > 0)
				{
					pageTitle = titleNodes[0].InnerText;
				}

				pageTitle = pageTitle.Trim(new char[] { '\n', '\r' });
				pageTitle = pageTitle.Trim();

				return pageTitle;
			}

			public string[] GetAllAnchorHrefs()
			{
				HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
				doc.LoadHtml(__data);
				HtmlAgilityPack.HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");

				List<string> hrefs = new List<string>();
				foreach (HtmlAgilityPack.HtmlNode node in links)
				{
					hrefs.Add(node.Attributes["href"].Value);
				}

				return hrefs.ToArray();
			}

			internal string GetStatusOutput()
			{
				if (this.Ex == null)
				{
					return String.Format("\t{0} => {1} hits", this.Url, this.Hits);
				}
				else
				{
					return String.Format("\t{0} => {1}:{2}", this.Url, this.Ex.GetType().ToString(), this.Ex.Message);
				}				
			}
		}

		private static string __data;

		/// <summary>
		/// Settings from the App.config appSettings section.
		/// </summary>
		private static SpiderSettings __settings;

		/// <summary>
		/// Urls that I have visited so far. This should keep me from duplicating effort where it isn't needed.
		/// </summary>
		private static Dictionary<SpiderUrl, UrlTracking> __visitedPages;

		/// <summary>
		/// Number of good urls, i.e. 200 HTTP response codes.
		/// </summary>
		private static int __goodUrls;

		/// <summary>
		/// Number of bad urls, from timeouts, 500 errors, etc.
		/// </summary>
		private static int __badUrls;

		/// <summary>
		/// Regular expression to filter out bad links.
		/// </summary>
		private static Regex __regex;

		/// <summary>
		/// Log file.
		/// </summary>
		private static StreamWriter __logFileWriter;
		private static Object __writeLock = new Object();

		private static CookieCollection __cookies;
		private static Object __signal = new Object();
		private static int __fetchedUrls = 0;
		private static string __userAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; Media Center PC 5.0; .NET CLR 1.1.4322; Vehix.QA.Spider.Spider)";

		/// <summary>
		/// Set up the private variables, add the starting url, and call the recursive spider method.
		/// </summary>
		public Program()
		{
			__settings = new SpiderSettings();
			__visitedPages = new Dictionary<SpiderUrl, UrlTracking>();
			__goodUrls = 0;
			__badUrls = 0;

			string startingUrl = String.Format("{0}{1}", __settings["startingUrl"], __settings["path"]);
			string[] urlParts = __settings["startingUrl"].Split(new string[] { "://" }, StringSplitOptions.RemoveEmptyEntries);
			string regularExpression = String.Format("^(http|https){{1}}(://){{1}}{0}/{{1}}((|automotive|Community|corporate|dealer|error|expresslane|finance|forums|green|honda|inventory|myVehix|research|rss|sell-your-car|video|freePriceQuote){{1}}/{{1}}((|contactUs|aboutUs|help|calculators|autoloans|insurance|leasingCenter|purchaseOptions|ICOTY|safety){{0,1}}/{{1}}){{0,1}}){{0,1}}(\\w+\\.{{1}}aspx{{1}}){{0,1}}", urlParts[1]);
			__regex = new Regex(regularExpression);

			if (__regex.IsMatch(startingUrl))
			{
				Console.WriteLine("Starting address of {0} was valid, continuing on...", startingUrl);
								
				HttpWebResponse headResponse = null;
				HttpWebRequest request = WebRequest.Create(startingUrl) as HttpWebRequest;
				request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
				request.Timeout = int.Parse(__settings["timeOut"]);

				if (__settings.ContainsKey("userAgent"))
				{
					request.UserAgent = __settings["userAgent"];
				}

				// Try and get a HEAD request so that we can set the cookie
				try
				{
					request.Method = "HEAD";
					headResponse = request.GetResponse() as HttpWebResponse;
					__cookies = headResponse.Cookies;
					headResponse.Close();
				}
				catch (Exception)
				{
					Console.Write("(HEAD failed)...");
				}

				UrlTracking t = new UrlTracking(new SpiderUrl(startingUrl, startingUrl));
				lock(__visitedPages) __visitedPages.Add(new SpiderUrl(startingUrl, startingUrl), t);
				// Start spidering in a background thread...
				ThreadPool.QueueUserWorkItem(new WaitCallback(t.Background_StartSpider));

				int n;
				do
				{
					Thread.Sleep(500);
					lock (__writeLock)
						n = __visitedPages.Count();
				} while (__fetchedUrls < n);
			}
			else
			{
				Console.WriteLine("Starting address of {0} was invalid, continuing on...", startingUrl);
			}
		}

		//public void SpiderPage(List<string> urls, CookieCollection cookies)
		//{
		//  List<string> nextUrls = new List<string>();

		//  foreach (string url in urls)
		//  {
		//    if (!__visitedPages.ContainsKey(url))
		//    {
		//      try
		//      {
		//        url.Replace("&amp;", "&");

		//        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
		//        request.CookieContainer = new CookieContainer();
		//        request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
		//        request.Timeout = int.Parse(__settings["timeOut"]);
		//        request.AllowAutoRedirect = true;

		//        foreach (Cookie cookie in cookies)
		//        {
		//          request.CookieContainer.Add(cookie);
		//        }

		//        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
		//        nextUrls = FilterUrls(GetAllAnchorHrefs(response));

		//        __visitedPages.Add(url, true);
		//        WriteLogMessage(String.Format("{2} Request for {0} passed: {1}", url, response.StatusCode.ToString(), __counter), true);
		//        __goodUrls++;
		//      }
		//      catch (Exception ex)
		//      {
		//        __visitedPages.Add(url, false);
		//        WriteLogMessage(String.Format("{2} Request for {0} failed: {1}", url, ex.Message, __counter), true);
		//        __badUrls++;
		//      }

		//      __counter++;
		//    }
		//  }

		//  SpiderPage(nextUrls, cookies);
		//}

		public List<string> FilterUrls(string[] urls)
		{
			List<string> urlList = new List<string>();
			string excludedUrls = "\n\nExcluded URLS:\n";
			int excludedCounter = 0;

			foreach (string url in urls)
			{
				if (__regex.IsMatch(url))
				{
					urlList.Add(url);
				}
				else
				{
					excludedUrls += String.Format("{1} {0}\n", url, excludedCounter);
					excludedCounter++;
				}
			}

			WriteLogMessage(excludedUrls, 1);
			return urlList;
		}

		#region WriteLogMessage
		private static void WriteLogMessage(string m)
		{
			WriteLogMessage(m, false, 0);
		}
		private static void WriteLogMessage(string m, bool echo2console)
		{
			WriteLogMessage(m, echo2console, 0);
		}
		private static void WriteLogMessage(string m, int newlines)
		{
			WriteLogMessage(m, false, newlines);
		}
		private static void WriteLogMessage(string m, bool echo2console, int newlines)
		{
			lock (__writeLock)
			{
				try
				{
					if (!String.IsNullOrEmpty(m))
					{
						if (echo2console)
						{
							if ((__settings["useColors"].ToLower()).Equals("true"))
							{
								if (m.Contains("passed"))
								{
									Nathandelane.Win32.ConsoleColors.SetConsoleColor((byte)ConsoleColor.DarkGreen);
								}
								else
								{
									Nathandelane.Win32.ConsoleColors.SetConsoleColor((byte)ConsoleColor.DarkRed);
								}
							}
							Console.WriteLine(m);
						}
						__logFileWriter.WriteLine(m);
					}
					if (newlines > 0)
					{
						for (int i = 0; i < newlines; i++) __logFileWriter.WriteLine();
					}
					__logFileWriter.Flush();
				}
				catch (Exception ex)
				{
					Console.WriteLine("An error occurred while trying to write to the log file: [{0}] {1}", ex.GetType().ToString(), ex.Message);
				}
			}
		}
		#endregion

		public string[] GetAllAnchorHrefs(HttpWebResponse response)
		{
			Stream stream = response.GetResponseStream();
			StreamReader reader = new StreamReader(stream);
			string data = reader.ReadToEnd();
			HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(data);
			HtmlAgilityPack.HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");

			List<string> hrefs = new List<string>();
			foreach (HtmlAgilityPack.HtmlNode node in links)
			{
				hrefs.Add(node.Attributes["href"].Value);
			}

			return hrefs.ToArray();
		}

		public string[] GetAllImageSrcs(HttpWebResponse response)
		{
			Stream stream = response.GetResponseStream();
			StreamReader reader = new StreamReader(stream);
			string data = reader.ReadToEnd();
			HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(data);
			HtmlAgilityPack.HtmlNodeCollection images = doc.DocumentNode.SelectNodes("//img[@src]");

			List<string> srcs = new List<string>();
			foreach (HtmlAgilityPack.HtmlNode node in images)
			{
				srcs.Add(node.Attributes["src"].Value);
			}

			return srcs.ToArray();
		}

		static void Main(string[] args)
		{
			string logfileName = String.Concat(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Log.txt");
			using (__logFileWriter = new StreamWriter(new FileStream(logfileName, FileMode.Create)))
			{

				Console.CancelKeyPress += delegate
				{
					WriteLogMessage(String.Concat(Environment.NewLine, "Number of links checked: ", __visitedPages.Count));
					WriteLogMessage(String.Concat("Number of good links: ", __goodUrls));
					WriteLogMessage(String.Concat("Number of bad links: ", __badUrls));
					lock (__writeLock)
					{
						Console.WriteLine("See Log.txt for information about bad links.");
					}
					WriteLogMessage("Link urls:");

					foreach (UrlTracking url in __visitedPages.Values)
					{
						WriteLogMessage(url.GetStatusOutput());
					}
				};

				try
				{
					new Program();
				}
				catch (Exception e)
				{
					WriteLogMessage(String.Format("Exception caught in outer program.\n\n{0}\n{1}\n{2}\n\n", e.Message, e.Source, e.StackTrace));
				}
				finally
				{
					WriteLogMessage(String.Concat("Number of links checked: {0}", __visitedPages.Count));
					WriteLogMessage(String.Concat("Number of good links: {0}", __goodUrls));
					WriteLogMessage(String.Concat("Number of bad links: {0}", __badUrls));
					WriteLogMessage(String.Concat("Link urls:\n"));

					foreach (UrlTracking url in __visitedPages.Values)
					{
						WriteLogMessage(url.GetStatusOutput());
					}
				}
			}
		}
	}
}

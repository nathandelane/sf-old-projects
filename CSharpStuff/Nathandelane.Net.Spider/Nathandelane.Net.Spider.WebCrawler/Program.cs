using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;

namespace Nathandelane.Net.Spider.WebCrawler
{
	class Program
	{
		#region Fields

		private UrlCollection _urls;
		private List<string> _visitedUrls;
		private CookieCollection _cookies;
		private DateTime _startTime;
		private bool _onlyFollowUniques;
		private bool _checkImages;
		private Regex _website;
		private IList<MimeType> _mimeTypesToIgnore;
		private IList<string> _linkHrefPatternsToIgnore;
		private IDictionary<string, bool> _contentTypesToInclude;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the default cookies from the App.config.
		/// </summary>
		private CookieCollection DefaultCookies
		{
			get
			{
				if (_cookies == null)
				{
					_cookies = new CookieCollection();

					if (ConfigurationManager.AppSettings["defaultCookies"] != null)
					{
						string[] cookiePairs = ConfigurationManager.AppSettings["defaultCookies"].Split(new char[] { '&' });

						foreach (string pair in cookiePairs)
						{
							string[] keyValue = pair.Split(new char[] { '=' });

							if (keyValue.Length == 2)
							{
								Cookie cookie = new Cookie(keyValue[0], keyValue[1]);
								cookie.Domain = ConfigurationManager.AppSettings["cookieDomain"];

								_cookies.Add(cookie);
							}
						}
					}
				}

				return _cookies;
			}
		}

		/// <summary>
		/// Gets the magic wait period.
		/// </summary>
		private int MagicWaitPeriod
		{
			get
			{
				int magicWaitPeriod = 150;

				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["magicWaitPeriod"]))
				{
					magicWaitPeriod = int.Parse(ConfigurationManager.AppSettings["magicWaitPeriod"]);
				}

				return magicWaitPeriod;
			}
		}

		/// <summary>
		/// Gets the allowed runtime in minutes.
		/// </summary>
		private long AllowedRuntimeInMinutes
		{
			get
			{
				long allowedRuntimeInMinutes = -1;

				if (ConfigurationManager.AppSettings["limitRunTime"] != null)
				{
					allowedRuntimeInMinutes = long.Parse(ConfigurationManager.AppSettings["limitRunTime"]);
				}

				return allowedRuntimeInMinutes;
			}
		}

		/// <summary>
		/// Gets the current runtime in minutes.
		/// </summary>
		private long RuntimeInMinutes
		{
			get
			{
				long runtimeInMinutes = -1;

				if (AllowedRuntimeInMinutes != -1)
				{
					DateTime now = DateTime.Now;
					TimeSpan difference = now.Subtract(_startTime);
					runtimeInMinutes = (difference.Days * 24 * 60) + (difference.Hours * 60) + difference.Minutes;
				}

				return runtimeInMinutes;
			}
		}

		/// <summary>
		/// Gets the allowed memory remaining in megabytes.
		/// </summary>
		private float AllowedMemoryRemainingInMegabytes
		{
			get
			{
				float allowedMemoryRemainingInMegabytes = -1;

				if (ConfigurationManager.AppSettings["limitMemoryUsage"] != null)
				{
					allowedMemoryRemainingInMegabytes = float.Parse(ConfigurationManager.AppSettings["limitMemoryUsage"]);
				}

				return allowedMemoryRemainingInMegabytes;
			}
		}

		#endregion

		#region Constructor

		private Program()
		{
			CheckConfiguration();
			InitializeMimeTypesToIgnore();
			InitializeContentTypesToInclude();
			IntializeLinkHrefPatternsToIgnore();

			_startTime = DateTime.Now;

			string startingUrl = String.Concat(ConfigurationManager.AppSettings["startingUrl"], ConfigurationManager.AppSettings["path"]);

			_urls = new UrlCollection();
			_urls.Enqueue(new SpiderUrl(startingUrl, startingUrl));

			_visitedUrls = new List<string>();
			_onlyFollowUniques = bool.Parse(ConfigurationManager.AppSettings["onlyFollowUniques"]);
			_checkImages = bool.Parse(ConfigurationManager.AppSettings["checkImages"]);
			_website = new Regex(String.Format("^(http|https){{1}}://({0}){{1}}", ConfigurationManager.AppSettings["website"]), RegexOptions.Compiled | RegexOptions.CultureInvariant);

			Logger.InitializeLogFile("\"Id\",\"Start Time\",\"Message\",\"Target\",\"Referrer\",\"Title\",\"Time\",\"Size\",\"Content Type\",\"MIME Type\"");
		}

		#endregion

		#region Methods

		/// <summary>
		/// Crawls the next link in the queue.
		/// </summary>
		private void Crawl()
		{
			while (_urls.Count > 0)
			{
				SpiderUrl nextUrl = _urls.Dequeue();

				if ((_onlyFollowUniques && _website.IsMatch(nextUrl.Target.ToString())) || !_onlyFollowUniques)
				{
					Agent nextAgent = new Agent(nextUrl, DefaultCookies, _linkHrefPatternsToIgnore, _mimeTypesToIgnore, _contentTypesToInclude);

					if (!_visitedUrls.Contains(nextAgent.Hash))
					{
						nextAgent.Run();

						if (!_mimeTypesToIgnore.Contains(nextAgent.MimeType) && _contentTypesToInclude.ContainsKey(nextAgent.ContentType) && _contentTypesToInclude[nextAgent.ContentType])
						{
							Logger.LogMessage(nextAgent.ToString(), LoggingType.Both);

							if (nextAgent.Urls.Count > 0)
							{
								AddUrls(nextAgent.Urls.ToArray(), nextAgent.Referrer.AbsoluteUri);

								_visitedUrls.Add(nextAgent.Hash);
							}
						}
						else
						{
							Logger.LogToScreen(String.Format("Not Logging: {0}", nextAgent));
						}

						_cookies = nextAgent.Cookies;
					}
					else
					{
						nextAgent = null;
					}
				}
				else
				{
					nextUrl = null;
				}

				if (RuntimeInMinutes != -1)
				{
					if (RuntimeInMinutes >= AllowedRuntimeInMinutes)
					{
						return;
					}
				}

				if (AllowedMemoryRemainingInMegabytes != -1)
				{
					if (RamCounter.MegabytesAvailable < AllowedMemoryRemainingInMegabytes)
					{
						return;
					}
				}
			}

			Thread.Sleep(MagicWaitPeriod);
		}

		/// <summary>
		/// Adds the URLs found on the page to the queue.
		/// </summary>
		/// <param name="urls"></param>
		/// <param name="referrer"></param>
		private void AddUrls(string[] urls, string referrer)
		{
			foreach (string nextTarget in urls)
			{
				SpiderUrl nextUrl = new SpiderUrl(nextTarget, referrer);

				_urls.Enqueue(nextUrl);
			}
		}

		/// <summary>
		/// Checks the configuration to make sure that it doesn't break any rules.
		/// </summary>
		/// <returns></returns>
		private void CheckConfiguration()
		{
			if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["contentTypesToIgnore"]) & !String.IsNullOrEmpty(ConfigurationManager.AppSettings["contentTypesToInclude"]))
			{
				throw new Exception("The two configuration directives, contentTypesToIgnore and contentTypesToInclude may not be used together.");
			}
		}

		/// <summary>
		/// Gets the MIME Types to ignore, if any.
		/// </summary>
		private void InitializeMimeTypesToIgnore()
		{
			_mimeTypesToIgnore = new List<MimeType>();

			if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["mimeTypesToIgnore"]))
			{
				string[] mimeTypes = ConfigurationManager.AppSettings["mimeTypesToIgnore"].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string nextMimeType in mimeTypes)
				{
					MimeType next = (MimeType)Enum.Parse(typeof(MimeType), nextMimeType);

					_mimeTypesToIgnore.Add(next);
				}
			}
		}

		/// <summary>
		/// Gets any content types to include or ignore.
		/// </summary>
		private void InitializeContentTypesToInclude()
		{
			_contentTypesToInclude = new Dictionary<string, bool>();

			if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["contentTypesToInclude"]))
			{
				string[] contentTypes = ConfigurationManager.AppSettings["contentTypesToInclude"].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string nextContentType in contentTypes)
				{
					_contentTypesToInclude.Add(nextContentType, true);
				}
			}
			else if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["contentTypesToIgnore"]))
			{
				string[] contentTypes = ConfigurationManager.AppSettings["contentTypesToIgnore"].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string nextContentType in contentTypes)
				{
					_contentTypesToInclude.Add(nextContentType, false);
				}
			}
		}

		/// <summary>
		/// Gets any HREF link patterns to ignore.
		/// </summary>
		private void IntializeLinkHrefPatternsToIgnore()
		{
			_linkHrefPatternsToIgnore = new List<string>();

			if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["linkHrefPatternsToIgnore"]))
			{
				string[] patterns = ConfigurationManager.AppSettings["linkHrefPatternsToIgnore"].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

				if (patterns.Length > 0)
				{
					_linkHrefPatternsToIgnore = new List<string>(patterns);
				}
			}
		}

		#endregion

		static void Main(string[] args)
		{
			Program program = new Program();
			program.Crawl();
		}
	}
}

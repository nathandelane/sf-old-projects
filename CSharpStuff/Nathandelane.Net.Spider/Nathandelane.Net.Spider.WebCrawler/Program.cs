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
			_startTime = DateTime.Now;

			string startingUrl = String.Concat(ConfigurationManager.AppSettings["startingUrl"], ConfigurationManager.AppSettings["path"]);
			Uri startingUri = null;

			if (Uri.TryCreate(startingUrl, UriKind.Absolute, out startingUri))
			{
				_urls = new UrlCollection();
				_urls.Enqueue(new SpiderUrl(startingUri, startingUrl));

				_visitedUrls = new List<string>();
				_onlyFollowUniques = bool.Parse(ConfigurationManager.AppSettings["onlyFollowUniques"]);
				_checkImages = bool.Parse(ConfigurationManager.AppSettings["checkImages"]);
				_website = new Regex(String.Format("^(http|https){{1}}://({0}){{1}}", ConfigurationManager.AppSettings["website"]), RegexOptions.Compiled | RegexOptions.CultureInvariant);

				Logger.InitializeLogFile("Id, Start Time, Message, Target, Referrer, Title, Time");
			}
			else
			{
				throw new Exception(String.Format("Malformed Uri: {0}", startingUrl));
			}
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
					Agent nextAgent = new Agent(nextUrl, DefaultCookies);

					if (!_visitedUrls.Contains(nextAgent.Request.Hash))
					{
						nextAgent.Run();

						AddUrls(nextAgent.Request.GetUrls().ToArray(), nextAgent.Request.Referrer);

						_visitedUrls.Add(nextAgent.Request.Hash);

						Logger.LogMessage(nextAgent.ToString(), LoggingType.Both);

						_cookies = nextAgent.Request.Cookies;
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
		private void AddUrls(IList<SpiderUrl> urls, string referrer)
		{
			foreach (SpiderUrl nextTarget in urls)
			{
				Add(nextTarget);
			}
		}

		/// <summary>
		/// Adds the Url to the queue if it meets the internal criteria.
		/// </summary>
		/// <param name="url"></param>
		private void Add(SpiderUrl url)
		{
			if (url.IsDesirable)
			{
				if (_checkImages)
				{
					_urls.Enqueue(url);
				}
				else if (!_checkImages && !url.IsImage)
				{
					_urls.Enqueue(url);
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

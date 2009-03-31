﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Net;

namespace Nathandelane.Net.Spider.WebCrawler
{
	class Program
	{
		#region Fields

		private UrlCollection _urls;
		private List<string> _visitedUrls;
		private CookieCollection _cookies;

		#endregion

		#region Properties

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

		#endregion

		#region Constructor

		private Program()
		{
			string startingUrl = String.Concat(ConfigurationManager.AppSettings["startingUrl"], ConfigurationManager.AppSettings["path"]);

			_urls = new UrlCollection();
			_urls.Enqueue(new SpiderUrl(startingUrl, startingUrl));

			_visitedUrls = new List<string>();

			Logger.InitializeLogFile("Id, Message, Target, Referrer, Title, Time");
		}

		private void Crawl()
		{
			while (_urls.Count > 0)
			{
				SpiderUrl nextUrl = _urls.Dequeue();

				if ((bool.Parse(ConfigurationManager.AppSettings["onlyFollowUniques"]) && nextUrl.Target.Contains(ConfigurationManager.AppSettings["website"])) || !bool.Parse(ConfigurationManager.AppSettings["onlyFollowUniques"]))
				{
					Agent nextAgent = new Agent(nextUrl, DefaultCookies);

					if (!_visitedUrls.Contains(nextAgent.Hash))
					{
						nextAgent.Run();
						/*
						ThreadStart threadStart = new ThreadStart(nextAgent.Run);
						Thread thread = new Thread(threadStart);
						thread.Start();*/

						AddUrls(nextAgent.Urls.ToArray(), nextAgent.Referrer.AbsoluteUri);

						_visitedUrls.Add(nextAgent.Hash);

						Logger.LogMessage(nextAgent.ToString(), LoggingType.Both);

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
			}

			Thread.Sleep(250);
		}

		private void AddUrls(string[] urls, string referrer)
		{
			foreach (string nextTarget in urls)
			{
				SpiderUrl spiderUrl = new SpiderUrl(nextTarget, referrer);

				if (!spiderUrl.IsJavascript && !spiderUrl.IsMailto && !spiderUrl.Target.Contains("#"))
				{
					_urls.Enqueue(spiderUrl);
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

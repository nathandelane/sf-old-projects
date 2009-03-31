using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;

namespace Nathandelane.Net.Spider.WebCrawler
{
	class Program
	{
		#region Fields

		private UrlCollection _urls;
		private List<string> _visitedUrls;

		#endregion

		#region Constructor

		private Program()
		{
			string startingUrl = String.Concat(ConfigurationManager.AppSettings["startingUrl"], ConfigurationManager.AppSettings["path"]);

			_urls = new UrlCollection();
			_urls.Enqueue(new SpiderUrl(startingUrl, startingUrl));

			_visitedUrls = new List<string>();
		}

		private void Crawl()
		{
			while (_urls.Count > 0)
			{
				SpiderUrl nextUrl = _urls.Dequeue();

				if ((bool.Parse(ConfigurationManager.AppSettings["onlyFollowUniques"]) && nextUrl.Target.Contains(ConfigurationManager.AppSettings["website"])) || !bool.Parse(ConfigurationManager.AppSettings["onlyFollowUniques"]))
				{
					Agent nextAgent = new Agent(nextUrl);

					if (!_visitedUrls.Contains(nextAgent.Hash))
					{
						nextAgent.Run();
						/*
						ThreadStart threadStart = new ThreadStart(nextAgent.Run);
						Thread thread = new Thread(threadStart);
						thread.Start();*/

						AddUrls(nextAgent.Urls.ToArray(), nextAgent.Referrer.AbsoluteUri);

						_visitedUrls.Add(nextAgent.Hash);
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
		}

		private void AddUrls(string[] urls, string referrer)
		{
			foreach (string nextAddress in urls)
			{
				SpiderUrl spiderUrl = SanitizeAddress(nextAddress, referrer);

				_urls.Enqueue(spiderUrl);
			}
		}

		private SpiderUrl SanitizeAddress(string address, string referrer)
		{
			SpiderUrl spiderUrl = null;

			if (address.StartsWith("http://") || address.StartsWith("https://"))
			{
				spiderUrl = new SpiderUrl(address, referrer);
			}
			else
			{
				if (address.StartsWith("/"))
				{
					address = String.Concat(ConfigurationManager.AppSettings["startingUrl"], address);
					spiderUrl = new SpiderUrl(address, referrer);
				}
				else if (address.StartsWith("../"))
				{
					string relativeLocation = referrer.Substring(0, (referrer.LastIndexOf('/')));
					string actualLocation = relativeLocation;

					while (address.StartsWith("../"))
					{
						actualLocation = actualLocation.Substring(0, (referrer.LastIndexOf('/')));
						address = address.Substring("../".Length);
					}

					address = String.Concat(actualLocation, "/", address);
					spiderUrl = new SpiderUrl(address, referrer);
				}
				else
				{
					string relativeLocation = referrer.Substring(0, (referrer.LastIndexOf('/') + 1));

					address = String.Concat(relativeLocation, address);
					spiderUrl = new SpiderUrl(address, referrer);
				}
			}

			return spiderUrl;
		}

		#endregion

		static void Main(string[] args)
		{
			Program program = new Program();
			program.Crawl();
		}
	}
}

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
			string startingUrl = ConfigurationManager.AppSettings["startingUrl"];

			_urls = new UrlCollection();
			_urls.Enqueue(new SpiderUrl(startingUrl, startingUrl));

			_visitedUrls = new List<string>();
		}

		private void Crawl()
		{
			while (_urls.Count > 0)
			{
				Agent nextAgent = new Agent(_urls.Dequeue());

				if (!_visitedUrls.Contains(nextAgent.Hash))
				{
					nextAgent.Run();
					/*
					ThreadStart threadStart = new ThreadStart(nextAgent.Run);
					Thread thread = new Thread(threadStart);
					thread.Start();*/

					foreach (string nextUrl in nextAgent.Urls)
					{
						SpiderUrl spiderUrl = new SpiderUrl(nextUrl, nextAgent.Referrer.AbsolutePath);

						_urls.Enqueue(spiderUrl);
					}
				}
				else
				{
					nextAgent = null;
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

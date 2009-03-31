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

		#endregion

		#region Constructor

		private Program()
		{
			string startingUrl = ConfigurationManager.AppSettings["startingUrl"];

			_urls = new UrlCollection();
			_urls.Enqueue(new SpiderUrl(startingUrl, startingUrl));
		}

		private void Crawl()
		{
			while (_urls.Count > 0)
			{
				Agent nextAgent = new Agent(_urls.Dequeue());
				ThreadStart threadStart = new ThreadStart(nextAgent.Run);
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

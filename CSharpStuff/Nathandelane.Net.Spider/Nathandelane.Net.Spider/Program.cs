using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Nathandelane.Win32;

namespace Nathandelane.Net.Spider
{
	class Program
	{
		private Settings _settings;
		private List<string> _visitedUrlHashes;
		private Queue<SpiderUrl> _queuedLinks;
		private long _id;

		private Program()
		{
			_settings = new Settings();
			_visitedUrlHashes = new List<string>();
			_queuedLinks = new Queue<SpiderUrl>();
			_id = 0L;

			string startingUrl = String.Format("{0}{1}", _settings["startingUrl"], _settings["path"]);
			_queuedLinks.Enqueue(new SpiderUrl(startingUrl, startingUrl));

			string referrer = _queuedLinks.Peek().ReferringUrl;
			string lastUrl = _queuedLinks.Peek().Url;

			GetFirstPage(_queuedLinks.Dequeue());

			while (_queuedLinks.Count > 0)
			{
				SpiderUrl url = _queuedLinks.Dequeue();
				Agent agent = new Agent(url, 0L, _settings);

				if (!_visitedUrlHashes.Contains(agent.Hash()))
				{
					agent.Run();

					if (agent.ToString().Contains("200"))
					{
						ConsoleColors.SetConsoleColor((byte)ConsoleColor.Green);
					}
					else
					{
						ConsoleColors.SetConsoleColor((byte)ConsoleColor.DarkRed);
					}

					Console.WriteLine("{0}", agent);

					_visitedUrlHashes.Add(agent.Hash());
					_id++;

					AddLinksFor(agent);
				}
			}
		}

		private void GetFirstPage(SpiderUrl url)
		{

			Agent agent = GetNextAgent(url);
			agent.Run();

			_visitedUrlHashes.Add(agent.Hash());

			AddLinksFor(agent);
		}

		private Agent GetNextAgent(SpiderUrl url)
		{
			Agent agent = new Agent(url, _id, _settings);

			return agent;
		}

		private void AddLinksFor(Agent agent)
		{
			foreach (string url in agent.Urls)
			{
				_queuedLinks.Enqueue(new SpiderUrl(url, agent.Root));
			}
		}

		static void Main()
		{
			new Program();
		}
	}
}

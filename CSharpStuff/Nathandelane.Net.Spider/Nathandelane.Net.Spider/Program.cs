using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

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

		static void Main()
		{
			new Program();
		}
	}
}

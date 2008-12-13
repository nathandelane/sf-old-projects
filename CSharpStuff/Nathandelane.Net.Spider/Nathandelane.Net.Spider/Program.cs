using System;
using System.Collections.Generic;
using System.IO;
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
		private CookieCollection _cookies;
		private WebHeaderCollection _headers;

		private Program()
		{
			_settings = new Settings();
			_visitedUrlHashes = new List<string>();
			_queuedLinks = new Queue<SpiderUrl>();
			_id = 0L;
			_cookies = new CookieCollection();
			_headers = new WebHeaderCollection();

			if (File.Exists("SpiderLog.csv"))
			{
				File.Create("SpiderLog.csv");
			}

			string startingUrl = String.Format("{0}{1}", _settings["startingUrl"], _settings["path"]);
			_queuedLinks.Enqueue(new SpiderUrl(startingUrl, startingUrl));

			string referrer = _queuedLinks.Peek().ReferringUrl;
			string lastUrl = _queuedLinks.Peek().Url;

			GetFirstPage(_queuedLinks.Dequeue());

			while (_queuedLinks.Count > 0)
			{
				SpiderUrl url = _queuedLinks.Dequeue();
				Agent agent = new Agent(url, _id, _settings, _cookies, _headers);

				if (!_visitedUrlHashes.Contains(agent.Hash()))
				{
					agent.Run();

					_cookies = agent.Cookies;
					_headers = agent.Headers;

					if (agent.Response != null && agent.Response.StatusCode == HttpStatusCode.OK)
					{
						ConsoleColors.SetConsoleColor((byte)ConsoleColor.Green);
					}
					else
					{
						ConsoleColors.SetConsoleColor((byte)ConsoleColor.Red);
					}

					LogMessage(String.Format("{0}", agent), true);

					_visitedUrlHashes.Add(agent.Hash());
					_id++;

					AddLinksFor(agent);
				}
			}
		}

		private void GetHeadRequest(SpiderUrl url)
		{
			HttpWebResponse headResponse = null;
			HttpWebRequest request = WebRequest.Create(url.Url) as HttpWebRequest;
			request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
			request.Timeout = int.Parse(_settings["timeOut"]);

			if (_settings.ContainsKey("userAgent"))
			{
				request.UserAgent = _settings["userAgent"];
			}

			// Try and get a HEAD request so that we can set the cookie
			try
			{
				request.Method = "HEAD";
				headResponse = request.GetResponse() as HttpWebResponse;

				if (headResponse.Cookies.Count > 0)
				{
					_cookies = headResponse.Cookies;
				}
				else
				{
					string cookieHeader = headResponse.Headers["set-cookie"];
					string[] cookies = cookieHeader.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string nextCookie in cookies)
					{
						string[] cookieParts = nextCookie.Split(new char[] { '=' });

						if (cookieParts.Length == 2)
						{
							Cookie newCookie = new Cookie(cookieParts[0], cookieParts[1]);
							newCookie.Domain = _settings["domain"];
							_cookies.Add(newCookie);
						}
					}
				}

				headResponse.Close();
			}
			catch (Exception)
			{
				Console.Write("(HEAD failed)...");
			}
		}

		private void GetFirstPage(SpiderUrl url)
		{
			//GetHeadRequest(url);

			Agent agent = GetNextAgent(url);
			agent.Run();

			_visitedUrlHashes.Add(agent.Hash());

			AddLinksFor(agent);
		}

		private Agent GetNextAgent(SpiderUrl url)
		{
			Agent agent = new Agent(url, _id, _settings, _cookies, _headers);

			return agent;
		}

		private void AddLinksFor(Agent agent)
		{
			foreach (string url in agent.Urls)
			{
				_queuedLinks.Enqueue(new SpiderUrl(url, agent.Root));
			}
		}

		private void LogMessage(string msg, bool writeToConsole)
		{
			using (StreamWriter writer = new StreamWriter(new FileStream("SpiderLog.csv", FileMode.Append)))
			{
				writer.WriteLine(String.Format("{0}", msg));
				writer.Flush();
			}

			if (writeToConsole)
			{
				Console.WriteLine(String.Format("{0}", msg));
			}
		}

		static void Main()
		{
			new Program();
		}
	}
}

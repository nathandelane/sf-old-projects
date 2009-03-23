using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Nathandelane.Win32;
using System.Threading;

namespace Nathandelane.Net.Spider
{
	class Program
	{
		#region Fields

		private static Queue<SpiderUrl> __queuedLinks;

		private Settings _settings;
		private List<string> _visitedUrlHashes;
		private long _id;
		private CookieCollection _cookies;
		private WebHeaderCollection _headers;
		private string _logFileName;

		#endregion

		#region Properties

		public static Queue<SpiderUrl> QueuedLinks
		{
			get
			{
				return __queuedLinks; 
			}
		}

		private string LogFileName
		{
			get
			{
				string logFileName = "SpiderLog.csv";

				if (_settings.ContainsKey("loggingType"))
				{
					LoggingType loggingType = (LoggingType)Enum.Parse(typeof(LoggingType), _settings["loggingType"]);

					switch (loggingType)
					{
						case LoggingType.Reuse:
							logFileName = "SpiderLog.csv";
							break;
						case LoggingType.Rotate:
							logFileName = String.Format("SpiderLog_{0}.csv", DateTime.Now.ToString("dd.MMM.HH.mm.ss"));
							break;
					}
				}

				return logFileName;
			}
		}

		#endregion

		#region Constructors

		private Program()
		{
			_settings = new Settings();
			_visitedUrlHashes = new List<string>();
			__queuedLinks = new Queue<SpiderUrl>();
			_id = 0L;
			_logFileName = LogFileName;

			InitializeCookie();
			InitializeHeaders();
			InitializeLogFile();

			if (_settings.ContainsKey("useStartupQueue"))
			{
				if (bool.Parse(_settings["useStartupQueue"]))
				{
					SetQueueUsingStartUpQueue();
				}
			}

			Run();
		}

		#endregion

		#region Run Method

		private void Run()
		{
			string startingUrl = String.Format("{0}{1}", _settings["startingUrl"], _settings["path"]);
			__queuedLinks.Enqueue(new SpiderUrl(startingUrl, startingUrl));

			string referrer = __queuedLinks.Peek().ReferringUrl;
			string lastUrl = __queuedLinks.Peek().Url;

			GetFirstPage(__queuedLinks.Dequeue());

			while (__queuedLinks.Count > 0)
			{
				SpiderUrl url = __queuedLinks.Dequeue();
				Agent agent = new Agent(url, _id, _settings, _cookies, _headers);

				if (!_visitedUrlHashes.Contains(agent.Root))
				{
					agent.Run();

					_cookies = agent.Cookies;
					_headers = agent.Headers;

					if (agent.Response == null || agent.Response.StatusCode != HttpStatusCode.OK)
					{
						ConsoleColors.SetConsoleColor((byte)ConsoleColor.Red);
					}
					else if(ContainsWords(agent.ToString()))
					{
						ConsoleColors.SetConsoleColor((byte)ConsoleColor.Red);
					}
					else
					{
						ConsoleColors.SetConsoleColor((byte)ConsoleColor.Green);
					}

					LogMessage(String.Format("{0}", agent), true);

					if (_settings.ContainsKey("onlyFollowUniques") && bool.Parse(_settings["onlyFollowUniques"]))
					{
						_visitedUrlHashes.Add(agent.Root);
					}

					_id++;

					AddLinksFor(agent);

					if (_settings.ContainsKey("maxRemainingMemoryAtExit"))
					{
						if (RamCounter.MegabytesAvailable < float.Parse(_settings["maxRemainingMemoryAtExit"]))
						{
							SaveRemainingQueue();
						}
					}

					if (agent.SearchValueFound)
					{
						Console.WriteLine("SEARCH VALUE FOUND: {0}, PAGE: {1}", _settings["searchForText"], agent.Root);
						SaveRemainingQueue();
					}
				}
			}
		}

		#endregion

		#region Private Methods

		private void SetQueueUsingStartUpQueue()
		{
			Console.Write("Using startup queue to begin testing...");

			StartUpQueue startUpQueue = new StartUpQueue();
			int queueLength = startUpQueue.Count;

			Console.Write("{0} entries to be loaded...", queueLength);

			for (int startUpQueueIndex = 0; startUpQueueIndex < queueLength; startUpQueueIndex++)
			{
				__queuedLinks.Enqueue(startUpQueue[startUpQueueIndex]);
				Console.Write(".");

				Thread.Sleep(150);
			}
		}

		private void SaveRemainingQueue()
		{
			StartUpQueue startUpQueue = new StartUpQueue();
			startUpQueue.Save(__queuedLinks);
		}

		private bool ContainsWords(string source)
		{
			bool result = false;

			if (_settings.ContainsKey("logErrorOnWords"))
			{
				string[] words = _settings["logErrorOnWords"].Split(new char[] { ',' });
				int wordsIndex = 0;

				while (wordsIndex < words.Length && !result)
				{
					string word = words[wordsIndex];

					if (source.Contains(word))
					{
						result = true;
					}

					wordsIndex++;
				}
			}

			return result;
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
			catch (Exception ex)
			{
				LogError(String.Format("Exception caught! {0}; {1}; InnerException {2}; {3}", ex.Message, ex.StackTrace, ex.InnerException.Message, ex.InnerException.StackTrace));
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
			Agent agent = new Agent(url, _id, _settings, _cookies, _headers);

			return agent;
		}

		private void AddLinksFor(Agent agent)
		{
			foreach (string url in agent.Urls)
			{
				__queuedLinks.Enqueue(new SpiderUrl(url, agent.Root));
			}
		}

		private void InitializeCookie()
		{
			_cookies = new CookieCollection();

			if (_settings.ContainsKey("acceptCookies") && bool.Parse(_settings["acceptCookies"]))
			{
				if (_settings.ContainsKey("defaultCookies"))
				{
					string[] cookiePairs = _settings["defaultCookies"].Split(new char[] { '&' });

					foreach (string cookiePair in cookiePairs)
					{
						if (cookiePair.Contains("="))
						{
							string[] keyAndValue = cookiePair.Split(new char[] { '=' });

							Cookie nextCookie = new Cookie(keyAndValue[0], keyAndValue[1]);
							nextCookie.Domain = _settings["domain"];
							_cookies.Add(nextCookie);
						}
					}
				}
			}
		}

		private void InitializeHeaders()
		{
			_headers = new WebHeaderCollection();

			if (_settings.ContainsKey("defaultHeaders"))
			{
				string[] headerPairs = _settings["defaultHeaders"].Split(new char[] { '&' });

				foreach (string headerPair in headerPairs)
				{
					if (headerPair.Contains("="))
					{
						string[] keyAndValue = headerPair.Split(new char[] { '=' });

						_headers.Add(keyAndValue[0], keyAndValue[1]);
					}
				}
			}
		}

		private void InitializeLogFile()
		{
			if (File.Exists(LogFileName))
			{
				File.Delete(LogFileName);
			}

			LogMessage("\"Id\", \"Response\", \"Url\", \"Title\", \"Full Url\", \"Referring Page\", \"Elapsed Time\"", false);
		}

		private void LogMessage(string msg, bool writeToConsole)
		{
			Exception innerException = null;

			do
			{
				innerException = null;

				try
				{
					using (StreamWriter writer = new StreamWriter(new FileStream(_logFileName, FileMode.Append)))
					{
						writer.WriteLine(String.Format("{0}", msg));
						writer.Flush();
					}
				}
				catch (IOException ex)
				{
					innerException = ex;
					LogError(String.Concat(ex.Message, ex.Source, ex.StackTrace));
				}
			} while (innerException != null);

			if (writeToConsole)
			{
				Console.WriteLine(String.Format("{0}", msg));
			}
		}

		private void LogError(string errmsg)
		{
			using (StreamWriter writer = new StreamWriter(new FileStream("SpiderErrorLog.log", FileMode.Append)))
			{
				writer.WriteLine(String.Format("{0}", errmsg));
				writer.Flush();
			}
		}

		#endregion

		static void Main()
		{

			Console.CancelKeyPress += new ConsoleCancelEventHandler(CleanUpSpider);

			new Program();
		}

		static void CleanUpSpider(object sender, ConsoleCancelEventArgs e)
		{
			StartUpQueue startUpQueue = new StartUpQueue();
			startUpQueue.Save(QueuedLinks);
		}
	}
}

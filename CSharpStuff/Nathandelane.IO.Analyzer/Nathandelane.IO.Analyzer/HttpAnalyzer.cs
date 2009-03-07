using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace Nathandelane.IO.Analyzer
{
	public class HttpAnalyzer : WebAnalyzer
	{
		#region Fields

		private string[] _returnKey;
		private string[] _cookies;
		private UserAgent _agentString;
		private XDocument _document;
		private WebHeaderCollection _requestHeaders;
		private WebHeaderCollection _responseHeaders;

		#endregion

		#region Properties

		public UserAgent Agent
		{
			get { return _agentString; }
			set { _agentString = value; }
		}

		public XDocument Document
		{
			get { return _document; }
		}

		public WebHeaderCollection RequestHeaders
		{
			get { return _requestHeaders; }
		}

		public WebHeaderCollection ResponseHeaders
		{
			get { return _responseHeaders; }
		}

		public string Domain
		{
			get
			{
				string retVal = Location;

				int domainStart = (Type == WebAnalyzerType.Https) ? (Location.IndexOf("https://") + "https://".Length) : (Location.IndexOf("http://") + "http://".Length);
				int domainEnd = (Location.IndexOf("/", domainStart) > -1) ? Location.IndexOf("/", domainStart) : Location.Length;
				retVal = Location.Substring(domainStart, (domainEnd - domainStart));

				return retVal;
			}
		}

		#endregion

		#region Constructors

		public HttpAnalyzer(string[] parameters)
			: base(WebAnalyzerType.Http, parameters[1])
		{
			if (parameters[1].ToLower().StartsWith("https://"))
			{
				Type = WebAnalyzerType.Https;
			}

			_returnKey = new string[0];
			_cookies = new string[0];

			Timeout = 30000;
			Agent = UserAgent.InternetExplorer;

			ParseParameters(parameters);
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			HttpWebRequest request = WebRequest.Create(Location) as HttpWebRequest;
			request.UserAgent = Agent.AgentString;
			request.Timeout = Timeout * 1000;
			request.CookieContainer = new CookieContainer();
			request.CookieContainer.Add(SetCookies());

			_requestHeaders = request.Headers;

			HttpWebResponse response = request.GetResponse() as HttpWebResponse;

			_responseHeaders = response.Headers;

			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				HtmlDocument document = new HtmlDocument();
				document.LoadHtml(reader.ReadToEnd());
				document.OptionOutputAsXml = true;
				
				using(StringWriter stringWriter = new StringWriter())
				{
					document.Save(stringWriter);

					_document = XDocument.Parse(stringWriter.GetStringBuilder().ToString());
				}
			}

			DisplayResults();
		}

		#endregion

		#region Private Methods

		private void ParseParameters(string[] parameters)
		{
			foreach (string parameter in parameters)
			{
				if (parameter.StartsWith("--returnKey="))
				{
					string[] returnKeys = parameter.Substring("--returnKey=".Length).Split(new char[] { ',' });

					_returnKey = returnKeys;
				}
				else if (parameter.StartsWith("--timeout="))
				{
					string timeout = parameter.Substring("--timeout=".Length);

					Timeout = int.Parse(timeout);
				}
				else if (parameter.StartsWith("--cookies="))
				{
					string[] cookies = parameter.Substring("--cookies=".Length).Split(new char[] { ',' });

					_cookies = cookies;
				}
				else if (parameter.StartsWith("--userAgent="))
				{
					string userAgent = parameter.Substring("--userAgent=".Length);

					Agent = UserAgent.ByName(userAgent);
				}
			}
		}

		private CookieCollection SetCookies()
		{
			CookieCollection cookies = new CookieCollection();

			if (_cookies.Length > 0)
			{
				foreach (string nextCookie in _cookies)
				{
					string[] pair = nextCookie.Split(new char[] { '=' });
					
					Cookie cookie = new Cookie(pair[0], pair[1], "/", Domain);

					cookies.Add(cookie);
				}
			}

			return cookies;
		}

		private void DisplayResults()
		{
			ProcessReturnKey();
		}

		private void ProcessReturnKey()
		{
			if (_returnKey != null && _returnKey.Length > 0)
			{
				foreach (string nextItem in _returnKey)
				{
					switch (nextItem)
					{
						case "RequestHeaders":
							Console.Write("$requestHeaders=[{0}];", DisplayHeaders(RequestHeaders));
							break;
						case "ResponseHeaders":
							Console.Write("$responseHeaders=[{0}];", DisplayHeaders(ResponseHeaders));
							break;
						case "Data":
							Console.Write("$data={0}", DisplayData());
							break;
						case "Default":
							Console.Write("$requestHeaders=[{0}]; $responseHeaders=[{1}];", DisplayHeaders(RequestHeaders), DisplayHeaders(ResponseHeaders));
							break;
						default:
							break;
					}

					Console.Write(" ");
				}
			}
			else
			{
				Console.Write("$requestHeaders=[{0}]; $responseHeaders=[{1}];", DisplayHeaders(RequestHeaders), DisplayHeaders(ResponseHeaders));
			}
		}

		private string DisplayData()
		{
			return Document.ToString(SaveOptions.DisableFormatting);
		}

		private string DisplayHeaders(WebHeaderCollection headers)
		{
			StringBuilder sb = new StringBuilder();

			string[] keys = headers.AllKeys;
			foreach (string key in keys)
			{
				sb = sb.Append(String.Format("{0}={1}, ", key, headers[key]));
			}

			string result = sb.ToString();

			return result.Substring(0, result.Length - 2);
		}

		#endregion

		#region Static Methods

		public static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} --type=HttpAnalyzer url [--returnKeys=RequestHeaders,ResponseHeaders,Data] [--timeout=timeoutInSeconds] [--cookies=cookie1=value[,cookieN=value]] [--userAgent=userAgentName]", Assembly.GetEntryAssembly().GetName().Name);
		}

		#endregion
	}
}

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

		private UserAgent _agentString;
		private XDocument _document;

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

		#endregion

		#region Constructors

		public HttpAnalyzer(string host)
			: base(WebAnalyzerType.Http, host)
		{
			if (host.ToLower().StartsWith("https://"))
			{
				Type = WebAnalyzerType.Https;
			}

			Agent = UserAgent.InternetExplorer;
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			HttpWebRequest request = WebRequest.Create(Location) as HttpWebRequest;
			request.UserAgent = Agent.AgentString;
			request.Timeout = Timeout * 1000;

			HttpWebResponse response = request.GetResponse() as HttpWebResponse;

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
		}

		#endregion

		#region Static Methods

		public static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} --type=HttpAnalyzer url [timeoutInSeconds]", Assembly.GetEntryAssembly().GetName().Name);
		}

		#endregion
	}
}

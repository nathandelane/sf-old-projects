using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace Nathandelane.IO.Analyzer
{
	public class HttpAnalyzer : WebAnalyzer
	{
		#region Fields

		private UserAgent _agentString;
		private int _timeout;

		#endregion

		#region Properties

		public UserAgent Agent
		{
			get { return _agentString; }
			set { _agentString = value; }
		}

		public int Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}

		#endregion

		#region Constructors

		public HttpAnalyzer(string host)
			: base(WebAnalyzerType.Http, new Uri(host))
		{
			if (host.ToLower().StartsWith("https://"))
			{
				Type = WebAnalyzerType.Https;
			}

			Agent = UserAgent.InternetExplorer;
			Timeout = 30;
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			HttpWebRequest request = WebRequest.Create(Location) as HttpWebRequest;
			request.UserAgent = Agent.AgentString;
			request.Timeout = Timeout;

			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
		}

		#endregion
	}
}

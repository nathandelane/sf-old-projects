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
		#endregion

		#region Properties
		#endregion

		#region Constructors

		public HttpAnalyzer(string host)
			: base(WebAnalyzerType.Http, new Uri(host))
		{
			if (host.ToLower().StartsWith("https://"))
			{
				Type = WebAnalyzerType.Https;
			}
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			HttpWebRequest request = WebRequest.Create(Location) as HttpWebRequest;
		}

		#endregion
	}
}

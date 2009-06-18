using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Net.HttpAnalyzer
{
	public class AgentDefaults
	{
		#region Fields

		public static readonly string UserAgentHeader = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.11) Gecko/2009060215 Firefox/3.0.11 (.NET CLR 3.5.30729)";
		public static readonly string AcceptHeader = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
		public static readonly string AcceptLanguageHeader = "en-us,en;q=0.5";
		public static readonly string AcceptEncodingHeader = "gzip,deflate";
		public static readonly string AcceptCharsetHeader = "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
		public static readonly int? TimeoutHeader = 300;

		#endregion
	}
}

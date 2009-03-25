using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Analyzer
{
	public class UserAgent
	{
		#region Fields

		private string _agentString;

		#endregion

		#region Properties

		public string AgentString
		{
			get { return _agentString; }
		}

		#endregion

		#region Constructors

		private UserAgent(string agentString)
		{
			_agentString = agentString;
		}

		#endregion

		#region Static Members

		public static readonly UserAgent Empty = new UserAgent(String.Empty);
		public static readonly UserAgent InternetExplorer = new UserAgent("Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; Media Center PC 5.0; .NET CLR 1.1.4322)");
		public static readonly UserAgent InternetExplorer8 = new UserAgent("Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; Media Center PC 5.0; .NET CLR 1.1.4322)");
		public static readonly UserAgent InternetExplorer7 = new UserAgent("Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SU 3.1; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; .NET CLR 1.1.4322; Tablet PC 2.0; .NET CLR 3.5.21022)");
		public static readonly UserAgent InternetExplorer6 = new UserAgent("Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; NeosBrowser; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
		public static readonly UserAgent GoogleChrome = new UserAgent("Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/525.13 (KHTML, like Gecko) Chrome/0.2.149.27 Safari/525.13");
		public static readonly UserAgent ELinks = new UserAgent("ELinks/0.11.3-5ubuntu2-lite (textmode; Windows; Windows NT 6.0; 126x37-2)");
		public static readonly UserAgent Emacs = new UserAgent("Emacs-W3/4.0pre.46 URL/p4.0pre.46 (i686-pc-linux; X11)");
		public static readonly UserAgent Epiphany = new UserAgent("Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/420+ (KHTML, like Gecko)");
		public static readonly UserAgent Firefox = new UserAgent("Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.3) Gecko/2008092510 Ubuntu/8.04 (hardy) Firefox/3.0.3");
		public static readonly UserAgent Firefox3 = new UserAgent("Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.3) Gecko/2008092510 Ubuntu/8.04 (hardy) Firefox/3.0.3");
		public static readonly UserAgent Firefox2 = new UserAgent("Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.8.1.14) Gecko/20080821 Firefox/2.0.0.14");
		public static readonly UserAgent Firefox1_5 = new UserAgent("Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.8.0.12) Gecko/20070803 Firefox/1.5.0.12 Fink Community Edition");
		public static readonly UserAgent Flock = new UserAgent("Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.3) Gecko/2008100716 Firefox/3.0.3 Flock/2.0");
		public static readonly UserAgent IceApe = new UserAgent("Mozilla/5.0 (X11; U; GNU/kFreeBSD i686; en-US; rv:1.8.1.16) Gecko/20080702 Iceape/1.1.11 (Debian-1.1.11-1)");
		public static readonly UserAgent IceCat = new UserAgent("Mozilla/5.0 (X11; U; GNU/kFreeBSD i686; en-US; rv:1.9.0.1) Gecko/2008071502 Iceweasel/3.0.1 (Debian-3.0.1-1)");
		public static readonly UserAgent IceWeasel = new UserAgent("Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.1) Gecko/2008072716 IceCat/3.0.1-g1");
		public static readonly UserAgent Opera = new UserAgent("Opera/9.60 (Windows NT 6.0; U; en) Presto/2.1.1");
		public static readonly UserAgent Safari = new UserAgent("Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US) AppleWebKit/525.19 (KHTML, like Gecko) Version/3.1.2 Safari/525.21");

		public static UserAgent ByName(string name)
		{
			UserAgent userAgent = UserAgent.Empty;

			switch (name)
			{
				case "Empty":
					userAgent = UserAgent.Empty;
					break;
				case "InternetExplorer":
					userAgent = UserAgent.InternetExplorer;
					break;
				case "InternetExplorer8":
					userAgent = UserAgent.InternetExplorer8;
					break;
				case "InternetExplorer7":
					userAgent = UserAgent.InternetExplorer7;
					break;
				case "InternetExplorer6":
					userAgent = UserAgent.InternetExplorer6;
					break;
				case "GoogleChrome":
					userAgent = UserAgent.GoogleChrome;
					break;
				case "ELinks":
					userAgent = UserAgent.ELinks;
					break;
				case "Emacs":
					userAgent = UserAgent.Emacs;
					break;
				case "Epiphany":
					userAgent = UserAgent.Epiphany;
					break;
				case "Firefox":
					userAgent = UserAgent.Firefox;
					break;
				case "Firefox3":
					userAgent = UserAgent.Firefox3;
					break;
				case "Firefox2":
					userAgent = UserAgent.Firefox2;
					break;
				case "Firefox1_5":
					userAgent = UserAgent.Firefox1_5;
					break;
				case "IceApe":
					userAgent = UserAgent.IceApe;
					break;
				case "IceCat":
					userAgent = UserAgent.IceCat;
					break;
				case "IceWeasel":
					userAgent = UserAgent.IceWeasel;
					break;
				case "Opera":
					userAgent = UserAgent.Opera;
					break;
				case "Safari":
					userAgent = UserAgent.Safari;
					break;
			}

			return userAgent;
		}

		#endregion

		public static string Description()
		{
			return "Empty, InternetExplorer, InternetExplorer8, InternetExplorer7, InternetExplorer6, GoogleChrome, ELinks, Emacs, Epiphany, Firefox, Firefox3, Firefox2, Firefox1_5, IceApe, IceWeasel, Opera, Safari";
		}
	}
}

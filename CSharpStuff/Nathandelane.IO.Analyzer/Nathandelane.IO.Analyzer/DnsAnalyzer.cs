using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.IO.Analyzer
{
	public class DnsAnalyzer : WebAnalyzer
	{
		#region Fields

		private IPHostEntry _hostEntry;
		private IPAddress[] _addresses;

		#endregion

		#region Properties

		public IPHostEntry HostEntry
		{
			get { return _hostEntry; }
		}

		public IPAddress[] Addresses
		{
			get { return _addresses; }
		}

		#endregion

		#region Constructors

		public DnsAnalyzer(string host)
			: base(WebAnalyzerType.Dns, host)
		{
		}

		#endregion

		#region Private Methods

		private void DoForwardLookup()
		{
			_hostEntry = Dns.GetHostByName(Location);
			_addresses = _hostEntry.AddressList;

			Console.Write("Host-name: {0}; ", String.Join(", ", _hostEntry.Aliases));

			StringBuilder sb = new StringBuilder();
			sb.Append("Addresses: ");

			foreach (IPAddress nextAddress in _addresses)
			{
				sb.Append(String.Format("{0}, ", nextAddress));
			}

			string result = (sb.ToString()).Substring(0, sb.Length - 2);

			Console.WriteLine("{0}", result);
		}

		private void DoReverseLookup()
		{
			_hostEntry = Dns.Resolve(Location);

			Console.WriteLine("Host name: {0}", _hostEntry.HostName);
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			if (Regex.IsMatch(Location, "[A-Za-z]+"))
			{
				DoForwardLookup();
			}
			else
			{
				DoReverseLookup();
			}
		}

		#endregion

		#region Static Methods

		public static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} --type=DnsAnalyzer url", Assembly.GetEntryAssembly().GetName().Name);
		}

		#endregion
	}
}

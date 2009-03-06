﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
			: base(WebAnalyzerType.Dns, new Uri(host))
		{
		}

		#endregion

		#region Private Methods

		private void DoForwardLookup()
		{
			_hostEntry = Dns.GetHostByName(Location.Host);
			_addresses = _hostEntry.AddressList;

			Console.WriteLine("Addresses:");

			foreach (IPAddress nextAddress in _addresses)
			{
				Console.WriteLine("{0}", nextAddress);
			}
		}

		private void DoReverseLookup()
		{
			_hostEntry = Dns.Resolve(Location.Host);

			Console.WriteLine("Host name: {0}", _hostEntry.HostName);
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			if (Regex.IsMatch(Location.AbsoluteUri, "[A-Za-z]+"))
			{
				DoForwardLookup();
			}
			else
			{
				DoReverseLookup();
			}
		}

		#endregion
	}
}
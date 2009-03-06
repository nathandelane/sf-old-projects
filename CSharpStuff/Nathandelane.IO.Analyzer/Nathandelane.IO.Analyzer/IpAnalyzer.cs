using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nathandelane.IO.Analyzer
{
	public class IpAnalyzer : WebAnalyzer
	{
		#region Fields

		private Ping _sender;
		private PingOptions _options;

		#endregion

		#region Properties

		public PingOptions Options
		{
			get { return _options; }
			set { _options = value; }
		}

		#endregion

		#region Constructors

		public IpAnalyzer(string host)
			: base(WebAnalyzerType.Ip, host)
		{
			_options = new PingOptions(128, true);
			Timeout = 120;
		}

		public IpAnalyzer(string host, int timeout)
			: base(WebAnalyzerType.Ip, host)
		{
			_options = new PingOptions(128, true);
			Timeout = timeout;
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			byte[] data = ASCIIEncoding.ASCII.GetBytes(new String('0', 32));

			using (_sender = new Ping())
			{
				PingReply reply = _sender.Send(Location, Timeout, data, _options);

				if (reply.Status == IPStatus.Success)
				{
					Console.WriteLine("Address: {0}\nRoundtrip time: {1} ms", reply.Address, reply.RoundtripTime);
				}
			}
		}

		#endregion

		#region Static Methods

		public static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} --type=IpAnalyzer url [timeoutInSeconds]", Assembly.GetEntryAssembly().GetName().Name);
		}

		#endregion
	}
}

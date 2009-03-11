using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Nathandelane.IO.Analyzer
{
	public class IpAnalyzer : WebAnalyzer
	{
		#region Fields

		private Ping _sender;
		private PingOptions _options;
		private int _repeat;

		#endregion

		#region Properties

		public PingOptions Options
		{
			get { return _options; }
			set { _options = value; }
		}

		public int Repeat
		{
			get { return _repeat; }
			set { _repeat = value; }
		}

		#endregion

		#region Constructors

		public IpAnalyzer(string host)
			: base(WebAnalyzerType.Ip, host)
		{
			_options = new PingOptions(128, true);
			_repeat = 1;
			Timeout = 120;
		}

		public IpAnalyzer(string host, int timeout)
			: base(WebAnalyzerType.Ip, host)
		{
			_options = new PingOptions(128, true);
			_repeat = 1;
			Timeout = timeout;
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			Regex regex = new Regex("[\\d]+(.){1}[\\d]+(.){1}[\\d]+(.){1}[\\d]+");
			byte[] data = ASCIIEncoding.ASCII.GetBytes(new String('0', 32));
			IPHostEntry _hostEntry = (regex.IsMatch(Location)) ? Dns.GetHostByAddress(Location) : Dns.GetHostByName(Location);

			Console.WriteLine("Pinging {0} [{1}] with {2} bytes of data:", _hostEntry.HostName, _hostEntry.AddressList[0], data.Length);

			for (int repeatCounter = 0; repeatCounter < Repeat; repeatCounter++)
			{
				using (_sender = new Ping())
				{
					PingReply reply = _sender.Send(Location, Timeout, data, _options);

					if (reply.Status == IPStatus.Success)
					{
						Console.WriteLine("Reply from {0}: bytes={1} time={2}ms TTL={3}", reply.Address, data.Length, reply.RoundtripTime, Options.Ttl);
					}
					else
					{
						Console.WriteLine("Pinging {0} resulted in {1}", Location, reply.Status);
					}
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

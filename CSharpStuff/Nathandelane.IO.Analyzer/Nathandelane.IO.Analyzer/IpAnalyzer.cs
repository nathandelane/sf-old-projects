using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Analyzer
{
	public class IpAnalyzer : WebAnalyzer
	{
		#region Fields

		private Ping _sender;
		private PingOptions _options;
		private int _timeout;

		#endregion

		#region Properties

		public PingOptions Options
		{
			get { return _options; }
			set { _options = value; }
		}

		public int Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}

		#endregion

		#region Constructors

		public IpAnalyzer(string host)
			: base(WebAnalyzerType.Ip, new Uri(host))
		{
			_options = new PingOptions(128, true);
			_timeout = 120;
		}

		public IpAnalyzer(string host, int timeout)
			: base(WebAnalyzerType.Ip, new Uri(host))
		{
			_options = new PingOptions(128, true);
			_timeout = timeout;
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			byte[] data = ASCIIEncoding.ASCII.GetBytes(new String('0', 32));

			using (_sender = new Ping())
			{
				PingReply reply = _sender.Send(Location.Host, _timeout, data, _options);

				if (reply.Status == IPStatus.Success)
				{
					Console.WriteLine("Address: {0}\nRoundtrip time: {1}", reply.Address, reply.RoundtripTime);
				}
			}
		}

		#endregion
	}
}

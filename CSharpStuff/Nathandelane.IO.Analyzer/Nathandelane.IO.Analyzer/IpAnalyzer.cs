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
		private byte[] _data;

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

		public byte[] Data
		{
			get { return _data; }
			set { _data = value; }
		}

		#endregion

		#region Constructors

		public IpAnalyzer(string[] args)
			: base(WebAnalyzerType.Ip, args[1])
		{
			_options = new PingOptions(128, true);
			_repeat = 1;
			_data = ASCIIEncoding.ASCII.GetBytes(new String('0', 32));

			Timeout = 120;

			ParseParameters(args);
		}

		#endregion

		#region WebAnalyzer Members

		public override void Run()
		{
			Regex regex = new Regex("[\\d]+(.){1}[\\d]+(.){1}[\\d]+(.){1}[\\d]+");
			IPHostEntry _hostEntry = (regex.IsMatch(Location)) ? Dns.GetHostByAddress(Location) : Dns.GetHostByName(Location);

			Console.WriteLine("Pinging {0} [{1}] with {2} bytes of data:", _hostEntry.HostName, _hostEntry.AddressList[0], _data.Length);

			for (int repeatCounter = 0; repeatCounter < Repeat; repeatCounter++)
			{
				using (_sender = new Ping())
				{
					PingReply reply = _sender.Send(Location, Timeout, _data, _options);

					if (reply.Status == IPStatus.Success)
					{
						Console.WriteLine("Reply from {0}: bytes={1} time={2}ms TTL={3}", reply.Address, _data.Length, reply.RoundtripTime, Options.Ttl);
					}
					else
					{
						Console.WriteLine("Pinging {0} resulted in {1}", Location, reply.Status);
					}
				}
			}
		}

		#endregion

		#region Private Methods

		private void ParseParameters(string[] parameters)
		{
			for (int paramIndex = 2; paramIndex < parameters.Length; paramIndex++)
			{
				string parameter = parameters[paramIndex];

				if (parameter.StartsWith("--ttl="))
				{
					Options.Ttl = int.Parse(parameter.Substring("--ttl=".Length));
				}
				else if (parameter.StartsWith("--timeout="))
				{
					string timeout = parameter.Substring("--timeout=".Length);

					Timeout = int.Parse(timeout);
				}
				else if (parameter.StartsWith("--repeat="))
				{
					_repeat = int.Parse(parameter.Substring("--repeat=".Length));
				}
				else if (parameter.StartsWith("--dataLength="))
				{
					_data = new byte[(new String('0', int.Parse(parameter.Substring("--dataLength=".Length)))).Length];
				}
				else if (parameter.StartsWith("--data="))
				{
					_data = ASCIIEncoding.ASCII.GetBytes(new String('0', int.Parse(parameter.Substring("--dataLength=".Length))));
				}
				else
				{
					Console.WriteLine("Warning, unrecognized parameter found on command-line: {0}. All parameters must use specified case.", parameter);
				}
			}
		}

		#endregion

		#region Static Methods

		public static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} --type=IpAnalyzer url [--timeout=timeoutInSeconds] [--ttl=timeToLiveInNumberOfNodes] [--repeat=timesToRepeat] [--dataLength=numberOfBytes|--data=actualData]", Assembly.GetEntryAssembly().GetName().Name);
		}

		#endregion
	}
}

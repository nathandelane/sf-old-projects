using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Nathandelane.IO.Analyzer;

namespace Nathandelane.IO.Analyzer.ConsoleAnalyzer
{
	class Program
	{
		private Program(string[] args)
		{
			IAnalyzer analyzer = null;
			string analyzerType = String.Empty;

			if (args[0].StartsWith("--type="))
			{
				analyzerType = args[0].Split(new string[] { "--type=" }, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			else
			{
				if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("analyzerdefault")))
				{
					analyzerType = Environment.GetEnvironmentVariable("analyzerdefault");
				}
			}

			switch (analyzerType)
			{
				case "Dns":
				case "DnsAnalyzer":
					analyzer = new DnsAnalyzer(args[1]);
					break;
				case "Ip":
				case "IpAnalyzer":
					analyzer = new IpAnalyzer(args);
					break;
				case "Http":
				case "HttpAnalyzer":
					analyzer = new HttpAnalyzer(args);
					break;
				default:
					Console.WriteLine("Missing --type argument which must come first.");
					throw new ArgumentException(args[0]);
			}

			analyzer.Run();
		}

		static void Main(string[] args)
		{
			if (args.Length == 0 || args[0].Equals("-h") || args[0].Equals("--help") || args[0].Equals("?"))
			{
				DisplayHelp();
			}
			else if (args[0].StartsWith("-h="))
			{
				string analyzerType = args[0].Split(new string[] { "--h=" }, StringSplitOptions.RemoveEmptyEntries)[0];

				DisplayHelpFor(analyzerType);

			}
			else if (args[0].StartsWith("--help="))
			{
				string analyzerType = args[0].Split(new string[] { "--help=" }, StringSplitOptions.RemoveEmptyEntries)[0];

				DisplayHelpFor(analyzerType);
			}
			else
			{
				try
				{
					new Program(args);

					Console.WriteLine();
				}
				catch (Exception ex)
				{
					Console.WriteLine("{0} caught! {1}", ex.GetType().Name, ex.Message);
				}
			}
		}

		private static void DisplayHelpFor(string analyzerType)
		{
			switch (analyzerType)
			{
				case "DnsAnalyzer":
					DnsAnalyzer.DisplayHelp();
					break;
				case "IpAnalyzer":
					IpAnalyzer.DisplayHelp();
					break;
				case "HttpAnalyzer":
					HttpAnalyzer.DisplayHelp();
					break;
				default:
					throw new ArgumentException(analyzerType);
			}
		}

		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} --type=AnalyzerType [options] uri", Assembly.GetEntryAssembly().GetName().Name);
			Console.WriteLine("Valid AnalyzerType values are: {0}", String.Join(", ", Enum.GetNames(typeof(AnalyzerType))));
		}
	}
}

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
			string analyzerType = args[0].Split(new string[] { "--type=" }, StringSplitOptions.RemoveEmptyEntries)[0];

			switch (analyzerType)
			{
				case "DnsAnalyzer":
					analyzer = new DnsAnalyzer(args[1]);
					break;
				case "IpAnalyzer":
					analyzer = (args.Length == 3) ? new IpAnalyzer(args[1], int.Parse(args[2])) : new IpAnalyzer(args[1]);
					break;
				case "HttpAnalyzer":
					analyzer = new HttpAnalyzer(args);
					break;
				default:
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

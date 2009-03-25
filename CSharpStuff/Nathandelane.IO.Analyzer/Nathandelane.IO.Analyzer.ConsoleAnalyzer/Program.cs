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
			int remainingArgsArrayLength = args.Length - 1;
			int remainingArgsStartIndex = 1;
			string analyzerType = String.Empty;
			string[] remainingArgs = new string[remainingArgsArrayLength];

			if (args[0].StartsWith("--type="))
			{
				analyzerType = args[0].Split(new string[] { "--type=" }, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			else
			{
				if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("analyzerdefault", EnvironmentVariableTarget.User)))
				{
					analyzerType = Environment.GetEnvironmentVariable("analyzerdefault", EnvironmentVariableTarget.User);
					remainingArgsArrayLength = args.Length;
					remainingArgsStartIndex = 0;
					remainingArgs = new string[remainingArgsArrayLength];
				}
			}

			Array.Copy(args, remainingArgsStartIndex, remainingArgs, 0, remainingArgsArrayLength);

			switch (analyzerType)
			{
				case "Dns":
				case "DnsAnalyzer":
					analyzer = new DnsAnalyzer(remainingArgs[0]);
					break;
				case "Ip":
				case "IpAnalyzer":
					analyzer = new IpAnalyzer(remainingArgs);
					break;
				case "Http":
				case "HttpAnalyzer":
					analyzer = new HttpAnalyzer(remainingArgs);
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
			if (analyzerType.Contains(","))
			{
				string[] helpTopicParts = analyzerType.Split(new char[] { ',' });

				switch (helpTopicParts[0])
				{
					case "DnsAnalyzer":
						DnsAnalyzer.DisplayHelp();
						break;
					case "IpAnalyzer":
						IpAnalyzer.DisplayHelp();
						break;
					case "HttpAnalyzer":
						HttpAnalyzer.DisplayHelpFor(helpTopicParts[1]);
						break;
					default:
						throw new ArgumentException(analyzerType);
				}
			}
			else
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
		}

		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} [--type=AnalyzerType [options] uri | --helpAnalyzerType[|helpTopic] ]", Assembly.GetEntryAssembly().GetName().Name);
			Console.WriteLine("Valid AnalyzerType values are: {0}", String.Join(", ", Enum.GetNames(typeof(AnalyzerType))));
		}
	}
}

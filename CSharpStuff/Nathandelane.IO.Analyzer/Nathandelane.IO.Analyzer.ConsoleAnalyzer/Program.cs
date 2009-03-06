﻿using System;
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
			WebAnalyzer analyzer = null;
			string analyzerType = args[0].Split(new string[] { "--type=" }, StringSplitOptions.RemoveEmptyEntries)[0];

			switch (analyzerType)
			{
				case "DnsAnalyzer":
					analyzer = new DnsAnalyzer(args[1]);
					break;
				case "IpAnalyzer":
					analyzer = new IpAnalyzer(args[1]);
					break;
				case "HttpAnalyzer":
					analyzer = new HttpAnalyzer(args[1]);
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
			else
			{
				new Program(args);
			}
		}

		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} --type=AnalyzerType [options] uri", Assembly.GetEntryAssembly().GetName().Name);
			Console.WriteLine("Valid AnalyzerType values are: {0}", String.Join(", ", Enum.GetNames(typeof(AnalyzerType))));
		}
	}
}

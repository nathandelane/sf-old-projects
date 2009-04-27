using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Configuration;

namespace Nathandelane.IO.CopyFiles
{
	public class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				DisplayHelp();
			}
			else
			{
				if (ArgsAreValid(args))
				{
				}
			}
		}

		private static bool ArgsAreValid(string[] args)
		{
			bool result = true;
			string[] sources = args[0].Split(new char[] { ',' });
			string[] destinations = args[1].Split(new char[] { ',' });

			result = SourceIsValid(sources);
			result = DestinationIsValid(destinations);

			return result;
		}

		private static bool SourceIsValid(string[] sources)
		{
			bool result = false;

			using (Impersonator impersonator = new Impersonator(ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["domainName"], ConfigurationManager.AppSettings["password"]))
			{
				foreach (string source in sources)
				{
					if (File.Exists(source))
					{
						result = true;
					}
					else if (Directory.Exists(source))
					{
						result = true;
					}
				}
			}
			
			return result;
		}

		private static bool DestinationIsValid(string[] distinations)
		{
			bool result = true;

			using (Impersonator impersonator = new Impersonator(ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["domainName"], ConfigurationManager.AppSettings["password"]))
			{
				foreach (string destination in distinations)
				{
					if (File.Exists(destination))
					{
						result = true;
					}
					else if (Directory.Exists(destination))
					{
						result = true;
					}
				}
			}

			return result;
		}

		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} source[,source1,...,sourceN] destination[,destination1,...,destinationN] archiveMethod[,archiveMethod2,...archiveMethodN]", Assembly.GetEntryAssembly().GetName().Name);
		}
	}
}

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
					ArchiveMethod unpackMethod = ArchiveMethod.Null;

					if (args.Length == 3)
					{
						unpackMethod = (ArchiveMethod)Enum.Parse(typeof(ArchiveMethod), args[2]);
					}

					CopyResult result = CopyFiles.Copy(args[0], args[1], unpackMethod);

					Console.WriteLine("{0}", result.ToString());
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

			using (Impersonator impersonator = new Impersonator())
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
			bool result = false;

			using (Impersonator impersonator = new Impersonator())
			{
				foreach (string destination in distinations)
				{
					if (!File.Exists(destination) && !Directory.Exists(destination))
					{
						result = true;
					}
					else
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nathandelane.IO.Grep
{
	class Program
	{
		private Program(string regex, string[] fileNames)
		{
			if (!Agent.FilesExist(fileNames))
			{
				StringBuilder sb = new StringBuilder();
				foreach(string f in Agent.FilesNotExisting)
				{
					sb.Append(String.Concat(f, ", "));
				}
				Console.WriteLine("Warning, the following files could not be found to exist: {0}", sb.ToString());
			}

			Agent agent = new Agent(regex, fileNames);
			agent.Run();
		}

		static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				DisplayHelp();
			}
			else
			{
				List<string> fileNames = new List<string>();

				for(int i = 1; i < args.Length; i++)
				{
					fileNames.Add(args[i]);
				}

				new Program(args[0], fileNames.ToArray());
			}
		}

		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} <regex> filenames", Assembly.GetEntryAssembly().GetName().Name);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Nathandelane.Net.WebGet;

namespace Nathandelane.Net.WebGet.ConsoleWebGet
{
	class Program
	{
		private Program(string[] urls)
		{
			foreach (string nextUrl in urls)
			{
				Agent agent = new Agent(nextUrl, String.Empty, false);
				agent.Run();
			}
		}

		static void Main(string[] args)
		{
			if (args.Length < 1)
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
			Console.WriteLine("Usage: {0} url [url2 urln...]", Assembly.GetEntryAssembly().GetName().Name);
		}
	}
}

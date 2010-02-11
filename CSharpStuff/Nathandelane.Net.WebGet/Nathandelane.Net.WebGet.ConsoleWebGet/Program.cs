using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Nathandelane.Net.WebGet;
using System.Threading;

namespace Nathandelane.Net.WebGet.ConsoleWebGet
{
	class Program
	{
		private Program(string[] urls)
		{
			for (int urlIndex = 0; urlIndex < urls.Length; urlIndex++)
			{
				string nextUrl = urls[urlIndex];
				Agent nextAgent = new Agent(nextUrl, String.Empty, false);

				ThreadPool.QueueUserWorkItem(nextAgent.Run, urlIndex);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nathandelane.IO.Query
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				DisplayHelp();
			}
			else
			{
			}
		}

		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} [directory] query", Assembly.GetEntryAssembly().GetName().Name);
		}
	}
}

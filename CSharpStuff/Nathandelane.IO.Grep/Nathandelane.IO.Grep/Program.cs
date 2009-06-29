using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nathandelane.IO.Grep
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				DisplayHelp();
			}
		}

		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} <regex> filenames", Assembly.GetEntryAssembly().GetName().Name);
		}
	}
}

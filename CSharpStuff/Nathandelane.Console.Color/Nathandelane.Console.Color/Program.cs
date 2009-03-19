using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Nathandelane.Win32;

namespace Nathandelane.Console.Color
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				DisplayHelp();
			}
			else
			{
				ConsoleColors.SetConsoleColor((byte)(Enum.Parse(typeof(ConsoleColor), args[0])));
			}
		}

		private static void DisplayHelp()
		{
			System.Console.WriteLine("Usage: {0} [Black|Blue|Cyan|DarkBlue|DarkCyan|DarkGray|DarkGreen|DarkMagenta|DarkRed|DarkYellow|Gray|Green|Magenta|Red|White|Yellow]", Assembly.GetEntryAssembly().GetName().Name);
		}
	}
}

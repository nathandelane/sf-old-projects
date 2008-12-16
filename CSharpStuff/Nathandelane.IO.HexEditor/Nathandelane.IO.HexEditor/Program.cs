using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.HexEditor
{
	class Program
	{
		private HexFile _hexFile;

		public Program()
		{
			_hexFile = null;

			new InteractiveCommandInterpreter();
		}

		public Program(string filePath)
		{
			try
			{
				_hexFile = new HexFile(filePath);

				new InteractiveCommandInterpreter(_hexFile);
			}
			catch (Exception)
			{
				Console.WriteLine("Could not load file named {0}.", filePath);
			}
		}

		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				new Program(args[0]);
			}
			else
			{
				new Program();
			}
		}

		private void DisplayHelp()
		{
			Console.WriteLine("Help file for Nathandelane.IO.HexEditor.");
			Console.WriteLine("Usage: Nathandelane.IO.HexEditor filePath\n");
		}
	}
}

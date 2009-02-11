using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.DataDump
{
	class Program
	{
		private Program(DataDumpSource source, string destination)
		{
			using (DataDump dd = new DataDump(source, destination))
			{
				dd.Dump();
			}
		}

		static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				DisplayHelp();
			}
			else
			{
				int numberOfBytes = -1;
				string source = args[0];
				string destination = args[1];

				if (args.Length >= 3)
				{
					numberOfBytes = int.Parse(args[2]);
				}

				DataDumpSource ddSource = CreateDataDumpSource(source, destination, numberOfBytes);

				new Program(ddSource, destination);
			}
		}

		private static DataDumpSource CreateDataDumpSource(string source, string destination, int numberOfBytes)
		{
			DataDumpSource ddSource;
			DataDumpSourceType ddType;

			switch (source.ToLower())
			{
				case "random":
					ddType = DataDumpSourceType.Random;
					break;
				case "zero":
					ddType = DataDumpSourceType.Zero;
					break;
				default:
					ddType = DataDumpSourceType.File;
					break;
			}

			if (ddType == DataDumpSourceType.File)
			{
				ddSource = new DataDumpSource(source, numberOfBytes);
			}
			else
			{
				ddSource = new DataDumpSource(ddType, numberOfBytes);
			}

			return ddSource;
		}

		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: DataDump source destination [num]");
		}
	}
}

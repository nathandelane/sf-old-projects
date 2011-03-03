using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml.Linq;
using System.IO;

namespace Nathandelane.TestingTools.WebTesting.Driver
{
	class Program
	{
		#region Fields

		private static string __testListFileName = "TestList.xml";

		private XDocument _testList;

		#endregion

		#region Constructors

		private Program(FileInfo testListFile)
		{
			_testList = XDocument.Load(testListFile.OpenRead());
		}

		#endregion

		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Usage: WebTestDriver <TestListFileName>");
			}
			else
			{
				FileInfo testListFileInfo = new FileInfo(args[0]);

				if (testListFileInfo.Exists)
				{
					Program program = new Program(testListFileInfo);
				}
				else
				{
					Console.WriteLine("Could not load test list file {0}.", testListFileInfo.Name);
				}
			}
		}
	}
}

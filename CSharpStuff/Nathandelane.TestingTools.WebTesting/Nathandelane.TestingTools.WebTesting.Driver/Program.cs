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

		/// <summary>
		/// Runs the tests found in the TestList.xml.
		/// </summary>
		private void Run()
		{
			IEnumerable<XElement> assemblies = _testList.Document.Descendants(XName.Get("assembly"));

			if (assemblies.Count<XElement>() > 0)
			{
				foreach (XElement nextAssembly in assemblies)
				{
					string assemblyName = nextAssembly.Attribute(XName.Get("name")).Value;

					if (!assemblyName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
					{
						assemblyName = String.Concat(assemblyName, ".dll");
					}

					Assembly assembly = Assembly.LoadFrom(assemblyName);
					Type[] types = assembly.GetTypes();

					foreach (Type nextType in types)
					{
						if (nextType.BaseType == typeof(WebTest))
						{
							WebTest nextWebTest = Activator.CreateInstance(nextType) as WebTest;

							if (String.IsNullOrEmpty(nextWebTest.Name))
							{
								Console.Write("{0}: ", nextType);
							}
							else
							{
								Console.Write("{0}: ", nextWebTest.Name);
							}

							IEnumerator<WebTestRequest> webTestRequests = nextWebTest.GetRequestEnumerator();
							
							while (webTestRequests.MoveNext())
							{
								WebTestRequest nextRequest = webTestRequests.Current;
								nextRequest.Execute();
							}

							Console.WriteLine("{0}", nextWebTest.Outcome);
						}
					}
				}
			}
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
					program.Run();
				}
				else
				{
					Console.WriteLine("Could not load test list file {0}.", testListFileInfo.Name);
				}
			}
		}
	}
}

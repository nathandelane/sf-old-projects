using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Web;

namespace ConvertTestLinkTestCases
{
	class Program
	{
		#region Fields

		private IList<FileInfo> _filesToBeConverted;
		private DirectoryInfo _currentWorkingDirectory;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the number of files in the list of files to be converted.
		/// </summary>
		private int FileCount
		{
			get { return _filesToBeConverted.Count; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of Program.
		/// </summary>
		/// <param name="files"></param>
		private Program(string[] files)
		{
			_currentWorkingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory().ToString());
			_filesToBeConverted = new List<FileInfo>();

			foreach (string nextFile in files)
			{
				if (nextFile.Contains("*"))
				{
					IList<string> parts = new List<string>(nextFile.Split(new char[] { '\\' }));
					string wildcard = parts.Last();
					parts.RemoveAt(parts.Count - 1);
					parts[0] = "C:\\";
					
					_currentWorkingDirectory = new DirectoryInfo(Path.Combine(parts.ToArray<string>()));

					FileInfo[] filesFromWildcard = _currentWorkingDirectory.GetFiles(wildcard);

					foreach (FileInfo nextFileFromWildcard in filesFromWildcard)
					{
						_filesToBeConverted.Add(nextFileFromWildcard);
					}
				}
				else
				{
					if (File.Exists(Path.Combine(_currentWorkingDirectory.FullName, nextFile)))
					{
						_filesToBeConverted.Add(new FileInfo(Path.Combine(_currentWorkingDirectory.FullName, nextFile)));
					}
					else
					{
						Console.WriteLine("Could not find file: {0}", nextFile);
					}
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Runs through conversion process.
		/// </summary>
		private void Run()
		{
			foreach (FileInfo nextFile in _filesToBeConverted)
			{
				XDocument nextXDocument = XDocument.Load(nextFile.OpenRead());
				XElement[] testCaseXElements = (from d in nextXDocument.Root.Descendants() where d.Name.LocalName.Equals("testcase") select d).ToArray<XElement>();
				XElement[] expectedResults = (from d in nextXDocument.Root.Descendants() where d.Name.LocalName.Equals("expectedresults") select d).ToArray<XElement>();
				IEnumerable<XElement> stepsXElements = from d in nextXDocument.Root.Descendants() where d.Name.LocalName.Equals("steps") select d;
				int testCaseIndex = 0;

				foreach (XElement nextStepsElement in stepsXElements)
				{
					Console.WriteLine("Test Case: {0} ({1})", testCaseXElements[testCaseIndex].Attribute("name").Value, nextFile.Name);
					Console.WriteLine("Converting: \r\n{0}", nextStepsElement.Value);

					string stepsDocString = String.Format("<steps>{0}</steps>", nextStepsElement.Value.Replace("&nbsp;", " ").Replace("&Ntilde;", "~").Replace("&ntilde;", "~").Replace("&fnof;", "Æ").Replace("&ndash;", "-").Replace("o:", String.Empty).Replace("&rsquo;", "'").Replace("&agrave;", "â").Replace("&eacute;", "ê").Replace("&egrave", "è").Replace("&", "&amp;"));//HttpUtility.HtmlDecode(String.Format("<steps>{0}</steps>", nextStepsElement.Value));//.Replace("&nbsp;", " ").Replace("&Ntilde;", "~").Replace("&fnof;", "Æ").Replace("&ndash;", "-").Replace("o:", String.Empty).Replace("&rsquo;", "'").Replace("&agrave;", "`")));
					XElement oldSteps = XElement.Parse(stepsDocString);
					XElement newSteps = nextStepsElement;
					newSteps.RemoveAll();

					if (oldSteps.HasElements && oldSteps.Descendants().FirstOrDefault().HasElements)
					{
						IEnumerable<XElement> oldLiSteps = from d in oldSteps.Descendants().FirstOrDefault().Descendants() where d.Name.LocalName.Equals("li") select d;
						int totalSteps = oldLiSteps.Count<XElement>();
						int stepCounter = 1;

						foreach (XElement nextLiStep in oldLiSteps)
						{
							string expectedResultsValue = String.Empty;

							if (stepCounter == totalSteps)
							{
								expectedResultsValue = expectedResults[testCaseIndex].Value;
							}

							newSteps.Add(
								new XElement("step",
									new XElement("step_number", String.Format("{0}", stepCounter)),
									new XElement("actions", nextLiStep.Value),
									new XElement("expectedresults", expectedResultsValue),
									new XElement("execution_type", "1")
								)
							);

							stepCounter += 1;
						}
					}

					Console.WriteLine("Converted Element: \r\n{0}", newSteps);
					Console.WriteLine("Expected Results: \r\n{0}", expectedResults[testCaseIndex].Value);
					Console.WriteLine(new String('-', 32));

					testCaseIndex++;
				}

				string newFilePath = Path.Combine(_currentWorkingDirectory.FullName, String.Format("converted-{0}", nextFile.Name));
				FileInfo newFile = new FileInfo(newFilePath);

				using (StreamWriter swriter = new StreamWriter(newFile.OpenWrite()))
				{
					string document = nextXDocument.ToString();
					swriter.Write(document);
				}
			}
		}

		#endregion

		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Usage: {0} <Files>", Assembly.GetEntryAssembly().FullName);
			}
			else
			{
				Program program = new Program(args);

				if (program.FileCount > 0)
				{
					program.Run();
				}
				else
				{
					Console.WriteLine("No Files were found to convert.{0}Usage: {1} <Files>", Environment.NewLine, Assembly.GetEntryAssembly().FullName);
				}
			}
		}
	}
}

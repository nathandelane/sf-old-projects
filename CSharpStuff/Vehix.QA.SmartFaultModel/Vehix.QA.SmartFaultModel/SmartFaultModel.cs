using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Vehix.QA.SmartFaultModel
{
	class SmartFaultModel
	{
		private List<FileInfo> _files;
		private Dictionary<string, List<string>> _dependencies;

		public SmartFaultModel()
		{
			_files = new List<FileInfo>();
			_dependencies = new Dictionary<string, List<string>>();

			string trunkPath;

			trunkPath = ConfigurationManager.AppSettings["trunkPath"];

			GetFiles(new DirectoryInfo(trunkPath));
			SearchFilesForDependencies();
			FlushDependencies();
		}

		private void FlushDependencies()
		{
			using (StreamWriter writer = new StreamWriter("SmartFaultModel.log"))
			{
				foreach (string key in _dependencies.Keys)
				{
					Console.WriteLine("{0}", key);
					writer.WriteLine("*{0}", key);

					foreach (string value in _dependencies[key])
					{
						Console.WriteLine("\tWriting {0}", value);
						writer.WriteLine("\t{0}", value);
					}
				}
			}
		}

		private void SearchFilesForDependencies()
		{
			foreach (FileInfo file in _files)
			{
				Console.WriteLine("Reading {0}", file.Name);

				using (StreamReader reader = new StreamReader(file.FullName))
				{
					bool getOut = false;

					while (!getOut)
					{
						string line = reader.ReadLine();

						if (!String.IsNullOrEmpty(line))
						{
							if (line.StartsWith("using Vehix"))
							{
								string val = line.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries)[1];
								Console.WriteLine("\tAdding {0}", val.Substring(0, (val.Length - 1)));

								if (!_dependencies.ContainsKey(val.Substring(0, (val.Length - 1))))
								{
									_dependencies.Add(val.Substring(0, (val.Length - 1)), new List<string>());
								}

								_dependencies[val.Substring(0, (val.Length - 1))].Add(file.FullName);
							}

							Console.Write(".");

							if (line.StartsWith("class") || line.StartsWith("namespace"))
							{
								getOut = true;
							}
						}
						else
						{
							getOut = true;
						}
					}
				}
			}
		}

		private void GetFiles(DirectoryInfo directory)
		{
			FileInfo[] filesInDir = directory.GetFiles("*.cs");

			foreach (FileInfo fInfo in filesInDir)
			{
				if (!fInfo.Name.Contains("AssemblyInfo") && !fInfo.Name.Contains("Tests") && !fInfo.Name.Contains("Test"))
				{
					_files.Add(fInfo);

					Console.WriteLine("{0}", fInfo.Name);
				}
			}

			DirectoryInfo[] dirsInDir = directory.GetDirectories();

			foreach (DirectoryInfo dir in dirsInDir)
			{
				GetFiles(dir);
			}
		}

		static void Main(string[] args)
		{
			new SmartFaultModel();
		}
	}
}

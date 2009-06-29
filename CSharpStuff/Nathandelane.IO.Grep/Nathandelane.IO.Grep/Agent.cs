using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Nathandelane.IO.Grep
{
	public class Agent
	{
		#region Fields

		private static List<string> __filesNotExisting = new List<string>();

		private Regex _regex;
		private string[] _fileNames;

		#endregion

		#region Properties

		public static string[] FilesNotExisting
		{
			get { return __filesNotExisting.ToArray(); }
		}

		#endregion

		#region Constructors

		public Agent(string regex, string[] fileNames)
		{
			_regex = new Regex(regex);
			_fileNames = fileNames;
		}

		#endregion

		#region Methods

		public static bool FilesExist(string fileName)
		{
			bool result = true;

			if (!File.Exists(fileName))
			{
				result = false;
				__filesNotExisting.Add(fileName);
			}

			return result;
		}

		public static bool FilesExist(string[] fileNames)
		{
			bool result = true;

			foreach (string fileName in fileNames)
			{
				if (!File.Exists(fileName))
				{
					result = false;
					__filesNotExisting.Add(fileName);
				}
			}

			return result;
		}

		public void Run()
		{
			foreach (string nextFileName in _fileNames)
			{
				if (File.Exists(nextFileName))
				{
					Console.WriteLine("{0}", nextFileName);
					List<string> lines = new List<string>();

					using (StreamReader reader = new StreamReader(nextFileName))
					{
						while (!reader.EndOfStream)
						{
							lines.Add(reader.ReadLine());
						}
					}

					int lineCounter = 0;
					foreach (string line in lines)
					{
						if (_regex.IsMatch(line))
						{
							Console.WriteLine("{0}: {1}", lineCounter, line);
						}
						lineCounter++;
					}
				}
			}
		}

		#endregion
	}
}

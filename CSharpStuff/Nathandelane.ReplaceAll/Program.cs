using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nathandelane.ReplaceAll
{
	class Program
	{
		private Program()
		{
			Configuration configuration = new Configuration();

			Run(configuration);
		}

		private void Run(Configuration configuration)
		{
			foreach (Replacement replacement in configuration.Replacements)
			{
				Console.WriteLine("Now running {0}...", replacement.Name);

				foreach (string path in replacement.FilePaths)
				{
					Console.WriteLine("Changing {0} to {1} in file at {2}", replacement.OldValue, replacement.NewValue, path);

					string data = String.Empty;
					string newData = String.Empty;

					using (StreamReader reader = new StreamReader(path))
					{
						data = reader.ReadToEnd();
					}

					newData = data.Replace(replacement.OldValue, replacement.NewValue);

					using (StreamWriter writer = new StreamWriter(path))
					{
						writer.Write(newData);
					}

					Console.WriteLine("Checking for any instances of {0} in the file now...", replacement.OldValue);

					using (StreamReader reader = new StreamReader(path))
					{
						data = reader.ReadToEnd();
					}

					if (data.Contains(replacement.OldValue))
					{
						Console.WriteLine("Changes could not be completed for {0}.", path);
					}
					else
					{
						Console.WriteLine("Completed all changes for {0}.", path);
					}
				}

				Console.WriteLine("Completed all changes for {0}.", replacement.Name);
			}
		}

		static void Main(string[] args)
		{
			new Program();
		}
	}
}

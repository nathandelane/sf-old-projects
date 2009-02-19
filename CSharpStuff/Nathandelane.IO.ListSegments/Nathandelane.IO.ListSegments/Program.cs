using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.ListSegments
{
	class Program
	{
		#region Constructors

		private Program(string[] args)
		{
			string directory = String.Empty;
			IList<string> filters = new List<string>();

			if (args.Length == 0)
			{
				using (ListSegments ls = new ListSegments())
				{
					ls.Run();
				}
			}
			else
			{
				foreach (string argument in args)
				{
					if (argument.StartsWith("-"))
					{
						ParseArgument(argument);
					}
					else if (argument.Contains(@"\"))
					{
						directory = argument;
					}
					else
					{
						filters.Add(argument);
					}
				}

				ExecuteListSegments(directory, filters.ToArray());
			}
		}

		#endregion

		#region Private Methods

		private void ExecuteListSegments(string directory, string[] filters)
		{
			if (filters.Length == 0)
			{
				if (String.IsNullOrEmpty(directory))
				{
					using (ListSegments ls = new ListSegments())
					{
						ls.Run();
					}
				}
				else
				{
					using (ListSegments ls = new ListSegments(directory))
					{
						ls.Run();
					}
				}
			}
			else
			{
				if (String.IsNullOrEmpty(directory))
				{
					using (ListSegments ls = new ListSegments(filters))
					{
						ls.Run();
					}
				}
				else
				{
					using (ListSegments ls = new ListSegments(directory, filters))
					{
						ls.Run();
					}
				}
			}
		}

		private void ParseArgument(string argument)
		{
			switch (argument)
			{
				case "-h":
				case "--help":
					DisplayHelp();
					break;
			}
		}

		private void DisplayHelp()
		{
			Console.WriteLine("Usage: ListSegments [options] [dir] [filter1[ filter2 filtern...]]");
			Console.WriteLine("Options");
			Console.WriteLine("-h, --help");
			Console.WriteLine("      Displays this help message.");
		}

		#endregion

		static void Main(string[] args)
		{
			new Program(args);
		}
	}
}

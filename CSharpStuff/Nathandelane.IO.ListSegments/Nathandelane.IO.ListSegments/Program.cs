using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.ListSegments
{
	class Program
	{
		#region Fields

		private string _directory;
		private IList<string> _filters;
		private IList<Option> _options;

		#endregion

		#region Constructors

		private Program(string[] args)
		{
			_directory = String.Empty;
			_filters = new List<string>();
			_options = new List<Option>();

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
						_directory = argument;
					}
					else
					{
						_filters.Add(argument);
					}
				}

				ExecuteListSegments(_directory, _filters);
			}
		}

		#endregion

		#region Private Methods

		private void ExecuteListSegments(string directory, IList<string> filters)
		{
			if (filters.Count == 0)
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
			switch (argument.Split(new char[] { '=' })[0])
			{
				case "-h":
				case "--help":
					DisplayHelp();
					break;
				case "-c":
				case "--columns":
					SetDisplayColumns(argument);
					break;
			}
		}

		private void SetDisplayColumns(string argument)
		{
			string[] columns = (argument.Split(new char[] { '=' })[1]).Split(new char[] { ';' });
			_options.Add(new Option("columns", new List<string>(columns.AsEnumerable())));
		}

		private void DisplayHelp()
		{
			Console.WriteLine("Usage: ListSegments [options] [dir] [filter1[ filter2 filtern...]]");
			Console.WriteLine("Options");
			Console.WriteLine("-h, --help");
			Console.WriteLine("      Displays this help message.");

			Environment.Exit(0);
		}

		#endregion

		static void Main(string[] args)
		{
			new Program(args);
		}
	}
}

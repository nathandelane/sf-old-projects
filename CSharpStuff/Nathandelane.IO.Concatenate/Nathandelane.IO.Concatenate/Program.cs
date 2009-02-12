using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Concatenate
{
	class Program
	{
		#region Fields

		private OptionMap _optionMap;

		#endregion

		#region Constructor

		private Program(string[] args)
		{
			_optionMap = new OptionMap();

			string options = ParseOptions(args);
			string[] fileNames = ParseFileNames(args);

			using (Concatenate cat = new Concatenate(fileNames, options))
			{
				cat.Run();
			}
		}

		#endregion

		#region Private Methods

		private string[] ParseFileNames(string[] args)
		{
			List<string> fileNames = new List<string>();

			foreach (string arg in args)
			{
				if (!arg.StartsWith("-"))
				{
					if (File.Exists(arg))
					{
						fileNames.Add(arg);
					}
				}
			}

			return fileNames.ToArray();
		}

		private string ParseOptions(string[] args)
		{
			string options = String.Empty;

			foreach (string arg in args)
			{
				if ((arg.StartsWith("-") || arg.StartsWith("--")) && _optionMap.OptionIsValid(arg))
				{
					options = String.Concat(options, _optionMap[arg]);
				}
			}

			return options;
		}

		#endregion

		static void Main(string[] args)
		{
			new Program(args);
		}
	}
}

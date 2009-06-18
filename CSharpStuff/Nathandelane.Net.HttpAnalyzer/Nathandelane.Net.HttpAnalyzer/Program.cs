using System;
using System.Collections.Generic;
using System.Text;
using Nathandelane.Net.HttpAnalyzer.Utility;

namespace Nathandelane.Net.HttpAnalyzer
{
	class Program
	{
		private Program(Arguments parsedArguments)
		{
			Uri uri = null;

			try
			{
				 Uri.TryCreate(parsedArguments["uri"], UriKind.Absolute, out uri);
			}
			catch (Exception ex)
			{
			}
		}

		static void Main(string[] args)
		{
			Arguments parsedArguments = Arguments.Parse(args);

			new Program(parsedArguments);
		}
	}
}

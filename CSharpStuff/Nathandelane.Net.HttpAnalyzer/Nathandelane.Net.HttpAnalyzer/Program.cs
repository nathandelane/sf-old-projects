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
				if (parsedArguments.Contains("uri"))
				{
					if (Uri.TryCreate(parsedArguments["uri"], UriKind.Absolute, out uri))
					{
						using (Agent agent = new Agent(uri))
						{
							agent.Run();
						}
					}
				}
				else
				{
					throw new ArgumentNullException("uri");
				}
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine("The parameter named {0} is required and could not be found on the command line. Please use --{0} -{0} or /{0}", ex.ParamName);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception caught! {0}", ex.Message);
				Console.WriteLine("{0}", ex.StackTrace);
			}
		}

		static void Main(string[] args)
		{
			Arguments parsedArguments = Arguments.Parse(args);

			if (parsedArguments.Contains("help"))
			{
				Agent.DisplayHelp();
			}
			else
			{
				new Program(parsedArguments);
			}
		}
	}
}

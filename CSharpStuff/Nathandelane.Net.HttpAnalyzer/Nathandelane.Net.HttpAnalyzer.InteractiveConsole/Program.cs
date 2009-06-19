using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.Net.HttpAnalyzer.InteractiveConsole.Utility;

namespace Nathandelane.Net.HttpAnalyzer.InteractiveConsole
{
	class Program
	{
		private Program()
		{
			Agent agent = null;
			string userInput = String.Empty;

			while (!userInput.Equals("q"))
			{
				userInput = Console.ReadLine();

				string[] args = userInput.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

				Arguments parsedArguments = Arguments.Parse(args);
				Uri uri = null;

				if (parsedArguments.Contains("uri"))
				{
					Uri.TryCreate(parsedArguments["uri"], UriKind.Absolute, out uri);

					using (agent = new Agent(uri))
					{
						agent.Run();

						if (!parsedArguments.Contains("suppress"))
						{
							if (!parsedArguments.Contains("scrub"))
							{
								Console.WriteLine("Response: {0}", agent);
							}
							else
							{
								Console.WriteLine("{0}", agent);
							}
						}

						if (parsedArguments.Contains("find"))
						{
							string value = agent.Document.DocumentNode.SelectSingleNode(parsedArguments["find"]).InnerHtml;

							if (!parsedArguments.Contains("scrub"))
							{
								Console.WriteLine("Find Results: {0}", value);
							}
							else
							{
								Console.WriteLine("{0}", value);
							}
						}
					}
				}
			}
		}

		static void Main(string[] args)
		{
			new Program();
		}
	}
}

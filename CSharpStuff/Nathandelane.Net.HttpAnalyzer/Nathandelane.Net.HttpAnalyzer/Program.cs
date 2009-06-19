using System;
using System.Collections.Generic;
using System.Text;
using Nathandelane.Net.HttpAnalyzer.Utility;
using HtmlAgilityPack;

namespace Nathandelane.Net.HttpAnalyzer
{
	class Program
	{
		#region Constructors

		private Program()
		{
			Run();
		}
		
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

		#endregion

		#region Methods

		private void Run()
		{
			Agent agent = null;
			string userInput = String.Empty;

			while (!userInput.Equals("q"))
			{
				Console.Write("{0}> ", Environment.NewLine);
				userInput = Console.ReadLine();

				if (userInput.Equals("help"))
				{
					Agent.DisplayHelp();
				}
				else if (!userInput.Equals("q"))
				{
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
								HtmlNodeCollection nodes = Find(agent.Document, parsedArguments["find"]);
								string value = String.Empty;

								foreach (HtmlNode node in nodes)
								{
									value = String.Concat(value, node.InnerHtml, Environment.NewLine);
								}

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
					else
					{
						Console.WriteLine("The parameter named uri is required and could not be found on the command line. Please use --uri -uri or /uri{0}", Environment.NewLine);
					}
				}
			}
		}

		private HtmlNodeCollection Find(HtmlDocument document, string xpath)
		{
			HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xpath);

			return nodes;
		}

		#region Entry Point

		static void Main(string[] args)
		{
			if (args.Length > 0)
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
			else
			{
				new Program();
			}
		}

		#endregion

		#endregion
	}
}

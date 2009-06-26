using System;
using System.Collections.Generic;
using System.Text;
using Nathandelane.Net.HttpAnalyzer.Utility;
using HtmlAgilityPack;
using System.IO;
using System.Net;

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
			Run(parsedArguments);
		}

		#endregion

		#region Methods

		private void Run(Arguments parsedArguments)
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
							if (parsedArguments.Contains("headers"))
							{
								agent.Headers = new WebHeaderCollection();
								string[] headers = parsedArguments["headers"].Split(new char[] { '&' });
								foreach (string header in headers)
								{
									string[] pair = header.Split(new char[] { '=' });
									if (pair.Length == 2)
									{
										agent.Headers.Add(pair[0], pair[1]);
									}
								}
							}

							if (parsedArguments.Contains("cookies"))
							{
								agent.Cookies = new CookieCollection();
								string[] cookies = parsedArguments["cookies"].Split(new char[] { '&' });
								foreach (string cookie in cookies)
								{
									string[] pair = cookie.Split(new char[] { '=' });
									if (pair.Length == 2)
									{
										agent.Cookies.Add(new Cookie(pair[0], pair[1]));
									}
								}
							}

							if (parsedArguments.Contains("proxy"))
							{
								string[] parts = parsedArguments["proxy"].Split(new char[] { ':' });
								if (parts.Length == 2)
								{
									agent.Proxy = new WebProxy(parts[0], int.Parse(parts[1]));
								}
							}

							agent.Run();

							HandleArguments(parsedArguments, agent);
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

		private void Run()
		{
			Agent agent = null;
			string userInput = String.Empty;
			ClearConsole clearConsole = new ClearConsole();

			while (!userInput.Equals("q"))
			{
				Console.Write("{0}> ", Environment.NewLine);
				userInput = Console.ReadLine();

				if (userInput.Equals("help") || userInput.Equals("--help") || userInput.Equals("-help") || userInput.Equals("/help"))
				{
					Agent.DisplayHelp();
				}
				else if (userInput.Equals("clear"))
				{
					clearConsole.Clear();
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
							if (parsedArguments.Contains("headers"))
							{
								agent.Headers = new WebHeaderCollection();
								string[] headers = parsedArguments["headers"].Split(new char[] { '&' });
								foreach (string header in headers)
								{
									string[] pair = header.Split(new char[] { '=' });
									if (pair.Length == 2)
									{
										agent.Headers.Add(pair[0], pair[1]);
									}
								}
							}

							if (parsedArguments.Contains("cookies"))
							{
								agent.Cookies = new CookieCollection();
								string[] cookies = parsedArguments["cookies"].Split(new char[] { '&' });
								foreach (string cookie in cookies)
								{
									string[] pair = cookie.Split(new char[] { '=' });
									if (pair.Length == 2)
									{
										agent.Cookies.Add(new Cookie(pair[0], pair[1]));
									}
								}
							}

							agent.Run();

							HandleArguments(parsedArguments, agent);
						}
					}
					else
					{
						Console.WriteLine("The parameter named uri is required and could not be found on the command line. Please use --uri -uri or /uri{0}", Environment.NewLine);
					}
				}
			}
		}

		private void HandleArguments(Arguments parsedArguments, Agent agent)
		{
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
				string attributes = String.Empty;

				for (int nodesIndex = 0; nodesIndex < nodes.Count; nodesIndex++)
				{
					if (!parsedArguments.Contains("no-attributes"))
					{
						if (parsedArguments.Contains("attributes"))
						{
							attributes = "(";

							string[] attribNames = parsedArguments["attributes"].Split(new char[] { ',' });
							foreach (string attribName in attribNames)
							{
								if (attribName.ToLower().Equals("innerhtml") || attribName.ToLower().Equals("innertext"))
								{
									switch (attribName.ToLower())
									{
										case "innerhtml":
											attributes = String.Concat(attributes, String.Format("innerhtml={0};", nodes[nodesIndex].InnerHtml));
											break;
										case "innertext":
											attributes = String.Concat(attributes, String.Format("innertext={1};", nodes[nodesIndex].InnerText));
											break;
									}
								}
								else
								{
									attributes = String.Concat(attributes, String.Format("{0}={1};", nodes[nodesIndex].Attributes[attribName].Name, nodes[nodesIndex].Attributes[attribName].Value));
								}
							}

							attributes = String.Format("{0}) ", attributes);
						}
						else
						{
							attributes = "(";

							for (int attributesIndex = 0; attributesIndex < nodes[nodesIndex].Attributes.Count; attributesIndex++)
							{
								attributes = String.Concat(attributes, String.Format("{0}={1};", nodes[nodesIndex].Attributes[attributesIndex].Name, nodes[nodesIndex].Attributes[attributesIndex].Value));
							}

							attributes = String.Format("{0}) ", attributes);
						}
					}

					if (!parsedArguments.Contains("no-innerhtml"))
					{
						if (!parsedArguments.Contains("scrub"))
						{
							value = String.Concat(value, String.Format("{0}:{1}{2}{3}", nodesIndex, attributes, nodes[nodesIndex].InnerHtml, Environment.NewLine));
						}
						else
						{
							value = String.Concat(value, String.Format("{0}{1}{2}", attributes, nodes[nodesIndex].InnerHtml, Environment.NewLine));
						}
					}
					else
					{
						if (!parsedArguments.Contains("scrub"))
						{
							value = String.Concat(value, String.Format("{0}:{1}{2}", nodesIndex, attributes, Environment.NewLine));
						}
						else
						{
							value = String.Concat(value, String.Format("{0}{1}", attributes, Environment.NewLine));
						}
					}
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

			if (parsedArguments.Contains("data"))
			{
				string data = String.Empty;
				
				using(StringWriter writer = new StringWriter())
				{
					agent.Document.OptionOutputAsXml = true;
					agent.Document.Save(writer);

					data = writer.GetStringBuilder().ToString();
				}

				if (!parsedArguments.Contains("scrub"))
				{
					Console.WriteLine("Data: {0}", data);
				}
				else
				{
					Console.WriteLine("{0}", data);
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

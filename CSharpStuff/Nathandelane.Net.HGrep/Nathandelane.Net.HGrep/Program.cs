using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Nathandelane.Net.HGrep
{
	class Program
	{
		#region Fields

		private ArgumentCollection _arguments;
		private string _data;

		#endregion

		#region Constructors

		private Program(string[] args)
		{
			Uri uri = null;

			_arguments = ArgumentCollection.Parse(args);

			if (_arguments.ContainsKey(ArgumentCollection.HelpArg) || _arguments.Count == 0)
			{
				Console.WriteLine("HGrep --url=<fully qualified url> [options]");
				Console.WriteLine("Options may be qualified by --, -, or /");
				Console.WriteLine("The available options include:");
				Console.WriteLine("{0,-20}XPath expression used to find a specific element of elements in the document.", "find");
				Console.WriteLine("{0,-20}Regular expression used to find a specific element of elements in the document. (This is experimental)", "find-regexp");
				Console.WriteLine("{0,-20}Displays this help.", "help");
				Console.WriteLine("{0,-20}Returns only the specified attributes of an XPath query", "return-attributes");
				Console.WriteLine("{0,-20}Displays the response headers of the request.", "return-headers");
				Console.WriteLine("{0,-20}Removes or scrubs the data headers.", "scrub");
			}
			else if (_arguments.ContainsKey(ArgumentCollection.UriArg))
			{
				if (Uri.TryCreate(_arguments[ArgumentCollection.UriArg] as string, UriKind.Absolute, out uri))
				{
					Agent agent = new Agent(uri);
					agent.Run();

					if (agent.Response.ContentEncoding.ToLower().Equals("gzip"))
					{
						using (StreamReader reader = new StreamReader(new GZipInputStream(agent.Response.GetResponseStream())))
						{
							_data = reader.ReadToEnd();
						}
					}
					else
					{
						using (StreamReader reader = new StreamReader(agent.Response.GetResponseStream()))
						{
							_data = reader.ReadToEnd();
						}
					}

					if (_arguments.ContainsKey(ArgumentCollection.ReturnHeadersArg) || _arguments.Count == 1)
					{
						DisplayResponseHeaders(agent);
					}

					if (_arguments.ContainsKey(ArgumentCollection.FindArg))
					{
						DisplayFind();
					}

					if (_arguments.ContainsKey(ArgumentCollection.FindRegexpArg))
					{
						DisplayRegexpFind();
					}
				}
			}
			else
			{
				Console.WriteLine("Required argument uri is missing.");
			}
		}

		#endregion

		#region Methods

		private void DisplayResponseHeaders(Agent agent)
		{
			string[] keys = agent.Response.Headers.AllKeys;

			if (!_arguments.ContainsKey(ArgumentCollection.ScrubArg))
			{
				Console.WriteLine("Response Headers:");
			}

			foreach (string key in keys)
			{
				Console.WriteLine("{0,-40}{1}", key, agent.Response.Headers[key]);
			}
		}

		private void DisplayFind()
		{
			if (!_arguments.ContainsKey(ArgumentCollection.ScrubArg))
			{
				Console.WriteLine("Nodes Found:");
			}

			XPathEvaluator evaluator = new XPathEvaluator(_data);
			HtmlNodeCollection nodes = evaluator.Select(_arguments[ArgumentCollection.FindArg] as string);

			if (nodes != null)
			{
				foreach (HtmlNode nextNode in nodes)
				{
					StringBuilder nodeValue = new StringBuilder();
					nodeValue.Append(nextNode.Name);
					nodeValue.Append(" [");

					HtmlAttributeCollection attributes = nextNode.Attributes;
					if (_arguments.ContainsKey(ArgumentCollection.ReturnAttributesArg))
					{
						foreach (string attr in (string[])_arguments[ArgumentCollection.ReturnAttributesArg])
						{
							if (!attr.Equals("inner-text") && !attr.Equals("inner-html"))
							{
								nodeValue.Append(String.Concat("[", attr, "='", attributes[attr].Value, "']"));
							}
						}
					}
					else
					{
						foreach (HtmlAttribute nextAttribute in attributes)
						{
							nodeValue.Append(String.Concat("[", nextAttribute.Name, "='", nextAttribute.Value, "']"));
						}
					}

					nodeValue.Append("] = ");
					if (_arguments.ContainsKey(ArgumentCollection.ReturnAttributesArg))
					{
						foreach (string attr in (string[])_arguments[ArgumentCollection.ReturnAttributesArg])
						{
							if (attr.Equals("inner-html"))
							{
								nodeValue.Append(String.Concat(nextNode.InnerHtml, " "));
							}
							else if (attr.Equals("inner-text"))
							{
								nodeValue.Append(String.Concat(nextNode.InnerText, " "));
							}
						}
					}
					else
					{
						nodeValue.Append(nextNode.InnerHtml);
					}

					Console.WriteLine("{0}", nodeValue);
				}
			}
			else
			{
				Console.WriteLine("No elements were found using {0}.", _arguments[ArgumentCollection.FindArg] as string);
			}
		}

		private void DisplayRegexpFind()
		{
			if (!_arguments.ContainsKey(ArgumentCollection.ScrubArg))
			{
				Console.WriteLine("Matches Found (Experimental):");
			}

			RegexpEvaluator evaluator = new RegexpEvaluator(_data);
			MatchCollection matches = evaluator.Select(_arguments[ArgumentCollection.FindRegexpArg] as string);

			if (matches != null)
			{
				Console.WriteLine("/{0}/", _arguments[ArgumentCollection.FindRegexpArg] as string);

				foreach (Match nextMatch in matches)
				{
					Console.WriteLine("{0}", nextMatch);
				}
			}
			else
			{
				Console.WriteLine("No matches were found using {0}.", _arguments[ArgumentCollection.FindRegexpArg] as string);
			}
		}

		#endregion

		static void Main(string[] args)
		{
			try
			{
				new Program(args);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: {0}", e.Message);
			}
		}
	}
}

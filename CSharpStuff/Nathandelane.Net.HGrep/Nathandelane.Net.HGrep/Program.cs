using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
using HtmlAgilityPack;

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

			if (_arguments.ContainsKey("help") || _arguments.Count == 0)
			{
				Console.WriteLine("HGrep --url=<fully qualified url> [options]");
				Console.WriteLine("Options may be qualified by --, -, or /");
				Console.WriteLine("The available options include:");
				Console.WriteLine("{0,-20}XPath expression used to find a specific element of elements in the document.", "find");
				Console.WriteLine("{0,-20}Displays this help.", "help");
				Console.WriteLine("{0,-20}Displays the response headers of the request.", "return-headers");
			}
			else if (_arguments.ContainsKey("uri"))
			{
				if (Uri.TryCreate(_arguments["uri"] as string, UriKind.Absolute, out uri))
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

					if (_arguments.ContainsKey("return-headers") || _arguments.Count == 1)
					{
						DisplayResponseHeaders(agent);
					}

					if (_arguments.ContainsKey("find"))
					{
						DisplayFind();
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

			Console.WriteLine("Response Headers:");
			foreach (string key in keys)
			{
				Console.WriteLine("{0,-40}{1}", key, agent.Response.Headers[key]);
			}
		}

		private void DisplayFind()
		{
			Console.WriteLine("Nodes Found:");
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(_data);

			HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(_arguments["find"] as string);
			if (nodes != null)
			{
				foreach (HtmlNode nextNode in nodes)
				{
					StringBuilder nodeValue = new StringBuilder();
					nodeValue.Append(nextNode.Name);
					nodeValue.Append(" [");

					HtmlAttributeCollection attributes = nextNode.Attributes;
					foreach (HtmlAttribute nextAttribute in attributes)
					{
						nodeValue.Append(String.Concat("[", nextAttribute.Name, "='", nextAttribute.Value, "']"));
					}

					nodeValue.Append("] = ");
					nodeValue.Append(nextNode.InnerHtml);

					Console.WriteLine("{0}", nodeValue);
				}
			}
			else
			{
				Console.WriteLine("No elements were found using {0}.", _arguments["find"] as string);
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

/*  Copyright (C) 2009, Nathandelane.
	License:
	Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.

	This program is free software; you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation; either version 3, or (at your option)
	any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
	02111-1307, USA.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using HtmlAgilityPack;
using ICSharpCode.SharpZipLib.GZip;

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
			_arguments = ArgumentCollection.Parse(args);

			Run();
		}

		#endregion

		#region Methods

		private void Run()
		{
			Uri uri = null;

			if (_arguments.Count == 0)
			{
				Help.GetHelpFor(String.Empty);
			}
			else if (_arguments.ContainsKey(ArgumentCollection.HelpArg))
			{
				Help.GetHelpFor(_arguments[ArgumentCollection.HelpArg] as string);
			}
			else if (_arguments.ContainsKey(ArgumentCollection.VersionArg))
			{
				Console.WriteLine("HGrep version {0} Copyright (C) 2009, Nathandelane, HGrep", Assembly.GetEntryAssembly().GetName().Version);
				Console.WriteLine("For help type HGrep -help");
			}
			else if (_arguments.ContainsKey(ArgumentCollection.UriArg))
			{
				if (Uri.TryCreate(_arguments[ArgumentCollection.UriArg] as string, UriKind.Absolute, out uri))
				{
					Agent agent = null;

					if (_arguments.ContainsKey(ArgumentCollection.IgnoreBadCertsArg))
					{
						if (_arguments.ContainsKey(ArgumentCollection.PostBodyArg))
						{
							if (_arguments.ContainsKey(ArgumentCollection.TimeoutArg))
							{
								agent = new Agent(uri, _arguments[ArgumentCollection.PostBodyArg] as string, true, (int)_arguments[ArgumentCollection.TimeoutArg]);
							}
							else
							{
								agent = new Agent(uri, _arguments[ArgumentCollection.PostBodyArg] as string, true);
							}
						}
						else
						{
							if (_arguments.ContainsKey(ArgumentCollection.TimeoutArg))
							{
								agent = new Agent(uri, true, (int)_arguments[ArgumentCollection.TimeoutArg]);
							}
							else
							{
								agent = new Agent(uri, true);
							}
						}
					}
					else
					{
						if (_arguments.ContainsKey(ArgumentCollection.PostBodyArg))
						{
							if (_arguments.ContainsKey(ArgumentCollection.TimeoutArg))
							{
								agent = new Agent(uri, _arguments[ArgumentCollection.PostBodyArg] as string, (int)_arguments[ArgumentCollection.TimeoutArg]);
							}
							else
							{
								agent = new Agent(uri, _arguments[ArgumentCollection.PostBodyArg] as string);
							}
						}
						else
						{
							if (_arguments.ContainsKey(ArgumentCollection.TimeoutArg))
							{
								agent = new Agent(uri, (int)_arguments[ArgumentCollection.TimeoutArg]);
							}
							else
							{
								agent = new Agent(uri);
							}
						}
					}

					agent.Run();

					if (agent.Response.ContentEncoding.ToLower().Equals("gzip"))
					{
						using (StreamReader reader = new StreamReader(new GZipInputStream(agent.Response.GetResponseStream())))
						{
							_data = reader.ReadToEnd();

							FormatData();
						}
					}
					else
					{
						using (StreamReader reader = new StreamReader(agent.Response.GetResponseStream()))
						{
							_data = reader.ReadToEnd();

							FormatData();
						}
					}

					DispatchPostProcessing(agent);
				}
				else
				{
					Console.WriteLine("Exception occurred! Uri is malformed");
					Environment.Exit(1);
				}
			}
			else
			{
				Console.WriteLine("Required argument uri is missing.");
			}
		}

		private void FormatData()
		{
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(_data);

			using (StringWriter writer = new StringWriter())
			{
				document.OptionOutputAsXml = true;
				document.Save(writer);

				_data = writer.GetStringBuilder().ToString();
			}
		}

		private void DispatchPostProcessing(Agent agent)
		{
			if (_arguments.ContainsKey(ArgumentCollection.ReturnHeadersArg) && !_arguments.ContainsKey(ArgumentCollection.ScrubArg))
			{
				DisplayResponseHeaders(agent);
			}
			else if (!_arguments.ContainsKey(ArgumentCollection.ScrubArg))
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

			if (_arguments.ContainsKey(ArgumentCollection.ReturnDataArg))
			{
				DisplayResponseData();
			}

			if (_arguments.ContainsKey(ArgumentCollection.ReturnUrlArg))
			{
				DisplayResponseUrl(agent);
			}
		}

		private void DisplayResponseUrl(Agent agent)
		{
			if (!_arguments.ContainsKey(ArgumentCollection.CleanArg))
			{
				Console.WriteLine("Response URL:");
			
			}
			Console.WriteLine("{0}", agent.Response.ResponseUri);
		}

		private void DisplayResponseData()
		{
			if (!_arguments.ContainsKey(ArgumentCollection.CleanArg))
			{
				Console.WriteLine("Response Data:");
			}

			Console.WriteLine(_data);
		}

		private void DisplayResponseHeaders(Agent agent)
		{
			string[] keys = agent.Response.Headers.AllKeys;

			if (!_arguments.ContainsKey(ArgumentCollection.CleanArg))
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
			if (!_arguments.ContainsKey(ArgumentCollection.CleanArg))
			{
				Console.WriteLine("Nodes Found:");
			}

			XPathEvaluator evaluator = new XPathEvaluator(_data);
			HtmlNodeCollection nodes = evaluator.Select(_arguments[ArgumentCollection.FindArg] as string);

			if (nodes != null)
			{
				if (_arguments.ContainsKey(ArgumentCollection.CountOnlyArg))
				{
					Console.WriteLine("{0}", nodes.Count);
				}
				else
				{
					foreach (HtmlNode nextNode in nodes)
					{
						StringBuilder nodeValue = new StringBuilder();
						nodeValue.Append(nextNode.Name);
						nodeValue.Append(" [");

						HtmlAttributeCollection attributes = nextNode.Attributes;
						if (_arguments.ContainsKey(ArgumentCollection.ReturnAttributesArg) && !_arguments.ContainsKey(ArgumentCollection.NoAttributesArg))
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

						nodeValue.Append("]");
						if (_arguments.ContainsKey(ArgumentCollection.ReturnAttributesArg))
						{
							foreach (string attr in (string[])_arguments[ArgumentCollection.ReturnAttributesArg])
							{
								if (attr.Equals("inner-html"))
								{
									if (!_arguments.ContainsKey(ArgumentCollection.NoInnerHtmlArg) && !String.IsNullOrEmpty(nextNode.InnerHtml))
									{
										nodeValue.Append(" = ");

										if (_arguments.ContainsKey(ArgumentCollection.EncodeLineBreaksArg))
										{
											nodeValue.Append(String.Concat(nextNode.InnerHtml.Trim().Replace("\n", "\\n").Replace("\r", "\\r"), " "));
										}
										else
										{
											nodeValue.Append(String.Concat(nextNode.InnerHtml.Trim(), " "));
										}
									}
								}
								else if (attr.Equals("inner-text"))
								{
									if (!String.IsNullOrEmpty(nextNode.InnerText))
									{
										nodeValue.Append(" = ");

										if (_arguments.ContainsKey(ArgumentCollection.EncodeLineBreaksArg))
										{
											nodeValue.Append(String.Concat(nextNode.InnerText.Trim().Replace("\n", "\\n").Replace("\r", "\\r"), " "));
										}
										else
										{
											nodeValue.Append(String.Concat(nextNode.InnerText.Trim(), " "));
										}
									}
								}
							}
						}
						else
						{
							if (!_arguments.ContainsKey(ArgumentCollection.NoInnerHtmlArg) && !String.IsNullOrEmpty(nextNode.InnerHtml))
							{
								nodeValue.Append(" = ");
								nodeValue.Append(nextNode.InnerHtml);
							}
						}

						Console.WriteLine("{0}", nodeValue);
					}
				}
			}
			else
			{
				Console.WriteLine("No elements were found using {0}.", _arguments[ArgumentCollection.FindArg] as string);
				Environment.Exit(1);
			}
		}

		private void DisplayRegexpFind()
		{
			if (!_arguments.ContainsKey(ArgumentCollection.CleanArg))
			{
				Console.WriteLine("Matches Found (Experimental):");
			}

			RegexpEvaluator evaluator = new RegexpEvaluator(_data);
			IList<string> matches = evaluator.Select(_arguments[ArgumentCollection.FindRegexpArg] as string);

			if(matches.Count > 0)
			{
				Console.WriteLine("/{0}/", _arguments[ArgumentCollection.FindRegexpArg] as string);

				for(int matchesIndex = 0; matchesIndex < matches.Count; matchesIndex++)
				{
					if (_arguments.ContainsKey(ArgumentCollection.NoNumberingArg))
					{
						Console.WriteLine("{0}", matches[matchesIndex].Trim());
					}
					else
					{
						Console.WriteLine("{0} {1}", matchesIndex, matches[matchesIndex].Trim());
					}
				}
			}
			else
			{
				Console.WriteLine("No matches were found using {0}.", _arguments[ArgumentCollection.FindRegexpArg] as string);
				Environment.Exit(2);
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
				Environment.Exit(1);
			}
		}
	}
}

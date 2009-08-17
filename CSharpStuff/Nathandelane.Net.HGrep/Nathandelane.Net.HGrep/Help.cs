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

namespace Nathandelane.Net.HGrep
{
	public class Help
	{
		#region Static Members

		public static void GetHelpFor(string helpTopic)
		{
			if (String.IsNullOrEmpty(helpTopic))
			{
				DisplayBasicHelp();
			}
			else
			{
				switch (helpTopic)
				{
					case "find":
						DisplayHelpForFind();
						break;
					case "find-regexp":
						DisplayHelpForFindRegexp();
						break;
					case "post-body":
						DisplayHelpForPostBody();
						break;
					default:
						Console.WriteLine("No specific help available for {0}.", helpTopic);
						break;
				}
			}
		}

		private static void DisplayHelpForPostBody()
		{
			Console.WriteLine("Post-Body");
			Console.WriteLine("Causes the request to post a querystring-formatted body of data as in form data");
			Console.WriteLine("As stated the format of this data is a querystring format as in, key1=value1&key2=value2, and so on.");
		}

		private static void DisplayHelpForFindRegexp()
		{
			Console.WriteLine("Find-RegExp");
			Console.WriteLine("Locates data in the datastream using a regular expression.");
			Console.WriteLine("This option is experimental");
			Console.WriteLine("Almost the entire Regular Expression standard is fully supported");
			Console.WriteLine("See http://www.regular-expressions.info/refflavors.html under .NET for further information.");
		}

		private static void DisplayHelpForFind()
		{
			Console.WriteLine("Find");
			Console.WriteLine("Locates elements by their XPath equivalent.");
			Console.WriteLine("XPath Syntax includes at very minimum a node name, but may be more elaborate:");
			Console.WriteLine("{0,-20}Selects all child nodes of nodename.", "nodename");
			Console.WriteLine("{0,-20}Selects from the root node.", "/");
			Console.WriteLine("{0,-20}Selects nodes anywhere in the document.", "//");
			Console.WriteLine("{0,-20}Selects attributes.", "@");
			Console.WriteLine("Here is an example: //div[@id='buildNumber'] selects all nodes that are <div>'s and have an id attribute of 'buildNumber'");
			Console.WriteLine("Another example: //a[contains(@href, 'vehix.com')] selects all anchors that have an href attribute containing the text 'vehix.com'");
			Console.WriteLine("See http://www.w3schools.com/XPath/xpath_syntax.asp for further information.");
		}

		private static void DisplayBasicHelp()
		{
			Console.WriteLine("HGrep url=<fully qualified url> [options]");
			Console.WriteLine("Options may be qualified by --, -, or / and in some terminals and cases you may need to qualify complete arguments with \"..\"");
			Console.WriteLine("The available options include:");
			Console.WriteLine("{0,-30}Displays only the number of nodes that would be returned from find option.", ArgumentCollection.CountOnlyArg);
			Console.WriteLine("{0,-30}XPath expression used to find a specific element of elements in the document.", String.Concat(ArgumentCollection.FindArg, "<xpath>"));
			Console.WriteLine("{0,-30}Regular expression used to find a specific element of elements in the document. (This is experimental)", String.Concat(ArgumentCollection.FindRegexpArg, "=<regex>"));
			Console.WriteLine("{0,-30}Displays this help.", String.Concat(ArgumentCollection.HelpArg, "[=<topic>]"));
			Console.WriteLine("{0,-30}Ignores when a bad SSL/TLS certificate is being used by a server.", ArgumentCollection.IgnoreBadCertsArg);
			Console.WriteLine("{0,-30}Suppresses output of attributes from find option.", ArgumentCollection.NoAttributesArg);
			Console.WriteLine("{0,-30}Suppresses output of inner-html from find option.", ArgumentCollection.NoInnerHtmlArg);
			Console.WriteLine("{0,-30}Suppresses numbering from find-regexp option.", ArgumentCollection.NoNumberingArg);
			Console.WriteLine("{0,-30}Causes the request to post form data.", String.Concat(ArgumentCollection.PostBodyArg, "=<querystring>"));
			Console.WriteLine("{0,-30}Returns only the specified attributes of an XPath query", String.Concat(ArgumentCollection.ReturnAttributesArg, "=<attributes>"));
			Console.WriteLine("{0,-30}Displays the response headers of the request.", ArgumentCollection.ReturnHeadersArg);
			Console.WriteLine("{0,-30}Diaplys the resulting URL from automatic redirects.", ArgumentCollection.ReturnUrlArg);
			Console.WriteLine("{0,-30}Removes or scrubs the data headers.", ArgumentCollection.ScrubArg);
		}

		#endregion
	}
}

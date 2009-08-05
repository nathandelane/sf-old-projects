using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.HGrep
{
	public class Help
	{
		#region Static Members

		public static void GetHelpFor(string helpTopic)
		{
			switch (helpTopic)
			{
				case "find":
					DisplayHelpForFind();
					break;
				case "find-regexp":
					DisplayHelpForFindRegexp();
					break;
				default:
					DisplayBasicHelp();
					break;
			}
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
			Console.WriteLine("HGrep --url=<fully qualified url> [options]");
			Console.WriteLine("Options may be qualified by --, -, or /");
			Console.WriteLine("The available options include:");
			Console.WriteLine("{0,-30}XPath expression used to find a specific element of elements in the document.", "find=<xpath>");
			Console.WriteLine("{0,-30}Regular expression used to find a specific element of elements in the document. (This is experimental)", "find-regexp=<regex>");
			Console.WriteLine("{0,-30}Displays this help.", "help[=<topic>]");
			Console.WriteLine("{0,-30}Returns only the specified attributes of an XPath query", "return-attributes=<attributes>");
			Console.WriteLine("{0,-30}Displays the response headers of the request.", "return-headers");
			Console.WriteLine("{0,-30}Removes or scrubs the data headers.", "scrub");
		}

		#endregion
	}
}

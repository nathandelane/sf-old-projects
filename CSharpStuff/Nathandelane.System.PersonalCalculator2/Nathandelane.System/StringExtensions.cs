using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System
{
	public static class StringExtensions
	{
		/// <summary>
		/// Tokenizes a string based on a set of patterns given as a string array.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="patterns">string[] Array of regex patterns to tokenize by. Order matters.</param>
		/// <returns>string</returns>
		public static string[] Tokenize(this string s, string[] patterns)
		{
			List<string> tokens = new List<string>();
			bool noMatchesFound = false;
			while (!String.IsNullOrEmpty(s) && !noMatchesFound)
			{
				for(int index = 0; index < patterns.Length; index++)
				{
					Regex pattern = new Regex(patterns[index]);
					if (pattern.IsMatch(s))
					{
						string match = pattern.Matches(s)[0].Value;
						tokens.Add(match);
						s = s.Substring(match.Length);
						index = -1;
					}

					if (index == patterns.Length - 1)
					{
						noMatchesFound = true;
					}
				}
			}

			return tokens.ToArray();
		}

		/// <summary>
		/// Capitalizes a string.
		/// </summary>
		/// <param name="s"></param>
		/// <returns>string</returns>
		public static string Capitalize(this string s)
		{
			string source = s;

			if (!String.IsNullOrEmpty(s) && source[0].IsLetter())
			{
				source = String.Concat(source[0].ToUpper(), source.Substring(1));
			}

			return source;
		}
	}
}

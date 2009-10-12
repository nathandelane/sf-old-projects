using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System
{
	public static class StringExtensions
	{
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
						index = 0;
					}

					if (index == patterns.Length - 1)
					{
						noMatchesFound = true;
					}
				}
			}

			return tokens.ToArray();
		}
	}
}

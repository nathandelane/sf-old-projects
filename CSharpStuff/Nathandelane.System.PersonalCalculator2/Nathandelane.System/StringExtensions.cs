using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System
{
	public static class StringExtensions
	{
		public static string Tokenize(this string s, string[] patterns)
		{
			Regex[] regexPatterns = new Regex[patterns.Length];
			for (int index = 0; index < patterns.Length; index++)
			{
				regexPatterns[index] = new Regex(patterns[index]);
			}

			List<string> tokens = new List<string>();
			foreach (Regex pattern in regexPatterns)
			{
				while (pattern.IsMatch(s))
				{
					tokens.Add(pattern.Matches(s)[0]);
				}
			}

			return tokens.ToArray();
		}
	}
}

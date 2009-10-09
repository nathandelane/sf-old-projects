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
			Regex[] regexPatterns = new Regex[patterns.Length];
			for (int index = 0; index < patterns.Length; index++)
			{
				regexPatterns[index] = new Regex(patterns[index]);
			}

			List<string> tokens = new List<string>();

			while (!String.IsNullOrEmpty(s))
			{
				foreach (Regex pattern in regexPatterns)
				{
					if (!String.IsNullOrEmpty(s))
					{
						while (pattern.IsMatch(s))
						{
							string match = pattern.Matches(s)[0].Value;
							tokens.Add(match);
							s = s.Substring(match.Length);
							break;
						}
					}
					else
					{
						break;
					}
				}
			}

			return tokens.ToArray();
		}
	}
}

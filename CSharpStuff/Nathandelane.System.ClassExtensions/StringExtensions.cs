using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nathandelane.System.ClassExtensions
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
						MatchCollection matches = pattern.Matches(s);
						foreach (Match match in matches)
						{
							tokens.Add(match.Value);
							s = s.Replace(match.Value, String.Empty);
						}

						index = -1;
					}
					else
					{
						noMatchesFound = true;
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

		/// <summary>
		/// Joins an array of strings by String.Empty.
		/// </summary>
		/// <param name="s"></param>
		/// <returns>string</returns>
		public static string Join(this string[] s)
		{
			return String.Join(String.Empty, s);
		}

		/// <summary>
		/// Indicates whether string is null or empty.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsNullOrEmpty(this string s)
		{
			return String.IsNullOrEmpty(s);
		}

		/// <summary>
		/// Appends a list of objects to string.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string Append(this string s, params object[] args)
		{
			string[] strArgs = new string[args.Length];

			for (int i = 0; i < args.Length; i++)
			{
				strArgs[i] = (string)args[i];
			}

			return String.Concat(s, strArgs);
		}

		/// <summary>
		/// Prepends a list of objects to string.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string Prepend(this string s, params object[] args)
		{
			string[] strArgs = new string[args.Length];

			for (int i = 0; i < args.Length; i++)
			{
				strArgs[i] = (string)args[i];
			}

			return String.Concat(strArgs, s);
		}

		/// <summary>
		/// Returns whether string contains digits.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool ContainsDigits(this string s)
		{
			Regex digits = new Regex("[\\d]+");

			return digits.IsMatch(s);
		}
	}
}

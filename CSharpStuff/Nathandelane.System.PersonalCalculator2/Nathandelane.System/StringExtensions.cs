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
						MatchCollection matches = pattern.Matches(s);
						foreach (Match match in matches)
						{
							tokens.Add(match.Value);
							s = s.Replace(match, String.Empty);
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
		/// Replaces all matches in a string with value.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="match">Match regex pattern match</param>
		/// <param name="value">String value</param>
		/// <returns></returns>
		public static string Replace(this string s, Match match, string value)
		{
			if (match.Index > 0)
			{
				s = String.Concat(s.Substring(0, match.Index), value, s.Substring(match.Index + match.Length));
			}
			else
			{
				s = String.Concat(value, s.Substring(match.Index + match.Length));
			}

			return s;
		}

		/// <summary>
		/// Replaces all pattern matches in a string with value.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="pattern"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Replace(this string s, Regex pattern, string value)
		{
			if (pattern.IsMatch(s))
			{
				foreach (Match match in pattern.Matches(s))
				{
					s = s.Replace(match, value);
				}
			}

			return s;
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
		/// Appends a list of strings to string.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string Append(this string s, params string[] args)
		{
			return String.Concat(s, args);
		}

		/// <summary>
		/// Prepends a list of strings to string.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string Prepend(this string s, params string[] args)
		{
			return String.Concat(args, s);
		}

		/// <summary>
		/// Appends a list of objects to string.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string Append(this string s, params object[] args)
		{
			return String.Concat(s, args);
		}

		/// <summary>
		/// Prepends a list of objects to string.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string Prepend(this string s, params object[] args)
		{
			return String.Concat(args, s);
		}
	}
}

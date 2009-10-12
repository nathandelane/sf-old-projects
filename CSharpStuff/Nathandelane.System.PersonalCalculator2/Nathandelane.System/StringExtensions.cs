﻿/*  Copyright (C) 2009, Nathandelane.
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
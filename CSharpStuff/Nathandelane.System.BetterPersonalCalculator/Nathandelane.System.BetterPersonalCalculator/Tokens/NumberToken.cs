﻿/*
Nathan Lane, Nathandelane Copyright (C) 2009, Nathandelane.

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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NumberToken : Token
	{
		#region Fields

		private static readonly Regex __numberPattern = new Regex("^(-){0,1}([\\d]+([.]{1}[\\d]+){0,1}|[\\dA-Za-z]+(H|h){1}|[0-7]+(O|o){1}|[01]+(B|b){1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __hexNumberPattern = new Regex("^(-){0,1}[\\dA-Za-z]+(H|h){1}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __octNumberPattern = new Regex("^(-){0,1}[0-7]+(O|o){1}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __binNumberPattern = new Regex("^(-){0,1}[01]+(B|b){1}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		#endregion

		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Number; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Number; }
		}

		#endregion

		#region Constructors

		public NumberToken()
			: base("0")
		{
		}

		public NumberToken(string value)
			: base(value)
		{
		}


		public NumberToken(Token other)
			: base(other)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a Token of type NumberToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token Parse(string line)
		{
			Token token = new NullToken();

			if (NumberToken.__numberPattern.IsMatch(line))
			{
				string matchText = NumberToken.__numberPattern.Matches(line)[0].Value;
				string value = AsDecimal(matchText);

				token = new NumberToken(value);
			}

			return token;
		}

		/// <summary>
		/// Converts a non decimal number to decimal.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private static string AsDecimal(string value)
		{
			string number = value;
			int length = number.Length - 1;

			if (NumberToken.__binNumberPattern.IsMatch(number))
			{
				number = Convert.ToInt64(number.Substring(0, length), 2).ToString();
			}
			else if (NumberToken.__hexNumberPattern.IsMatch(number))
			{
				number = Convert.ToInt64(number.Substring(0, length), 16).ToString();
			}
			else if (NumberToken.__octNumberPattern.IsMatch(number))
			{
				number = Convert.ToInt64(number.Substring(0, length), 8).ToString();
			}

			return number;
		}

		/// <summary>
		/// Gets the whole part of the number.
		/// </summary>
		/// <returns></returns>
		public string WholePart()
		{
			string value = this.ToString();
			int index = -1;

			if ((index = value.IndexOf(".", StringComparison.InvariantCultureIgnoreCase)) > -1)
			{
				value = value.Substring(0, index);
			}

			return value;
		}

		/// <summary>
		/// Gets the fractional part of the number.
		/// </summary>
		/// <returns></returns>
		public string FractionalPart()
		{
			string value = this.ToString();
			int index = -1;

			if ((index = value.IndexOf(".", StringComparison.InvariantCultureIgnoreCase)) > -1)
			{
				value = value.Substring(index + 1);
			}

			return value;
		}

		public string AsHex()
		{
			return String.Concat(Convert.ToString(long.Parse(WholePart()), 16), "h");
		}

		public string AsOct()
		{
			return String.Concat(Convert.ToString(long.Parse(WholePart()), 8), "o");
		}

		public string AsBin()
		{
			return String.Concat(Convert.ToString(long.Parse(WholePart()), 2), "b");
		}

		#endregion
	}
}

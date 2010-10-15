/*
Nathan Lane, Nathandelane Copyright (C) 2010, Nathandelane.

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

namespace Nathandelane.System.Bpc
{
	public class NumberToken : Token
	{
		#region Fields

		private static readonly Regex __numberPattern = new Regex("^(-){0,1}([\\dA-Za-z]+(H|h){1}|[0-7]+(O|o){1}|[01]+(B|b){1}|[\\d]+([.]{1}[\\d]+){0,1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __hexNumberPattern = new Regex("^(-){0,1}[\\dA-Za-z]+(H|h){1}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __octNumberPattern = new Regex("^(-){0,1}[0-7]+(O|o){1}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __binNumberPattern = new Regex("^(-){0,1}[01]+(B|b){1}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private string _representation;

		#endregion

		#region Properties

		/// <summary>
		/// Gets this NumberToken's token type.
		/// </summary>
		public override TokenType Type
		{
			get { return TokenType.Number; }
		}

		/// <summary>
		/// Gets this number token's precedence.
		/// </summary>
		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Number; }
		}

		/// <summary>
		/// Gets this NumberToken's representation.
		/// </summary>
		public string Representation
		{
			get { return _representation; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of NumberToken.
		/// </summary>
		public NumberToken()
			: base("0")
		{
		}

		/// <summary>
		/// Creates an instance of NumberToken.
		/// </summary>
		/// <param name="value"></param>
		public NumberToken(string value)
			: base(value)
		{
			_representation = value;
		}

		/// <summary>
		/// Creates an instance of NumberToken.
		/// </summary>
		/// <param name="other"></param>
		public NumberToken(Token other)
			: base(other)
		{
			_representation = other.ToString();
		}

		/// <summary>
		/// Creates an instance of NumberToken.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="rep"></param>
		public NumberToken(string value, string rep)
			: base(value)
		{
			_representation = rep;
		}

		/// <summary>
		/// Creates an instance of NumberToken.
		/// </summary>
		/// <param name="other"></param>
		/// <param name="rep"></param>
		public NumberToken(Token other, string rep)
			: base(other)
		{
			_representation = rep;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Attempts to parse a token and returns success or failure.
		/// </summary>
		/// <param name="line">String from which to take the next token.</param>
		/// <param name="token">Out parameter to send token to if successful.</param>
		/// <returns></returns>
		public static bool TryParse(string line, out Token token, bool isConstant)
		{
			bool parseSuccessful = false;

			if(isConstant)
			{
				token = ParseAsConstant(line);
			}
			else
			{
				token = Parse(line);
			}

			if (token is NumberToken)
			{
				parseSuccessful = true;
			}

			return parseSuccessful;
		}

		/// <summary>
		/// Attempts to parse a token and returns success or failure.
		/// </summary>
		/// <param name="line">String from which to take the next token.</param>
		/// <param name="token">Out parameter to send token to if successful.</param>
		/// <returns></returns>
		public static bool TryParse(string line, out Token token)
		{
			bool parseSuccessful = false;

			token = new NullToken();

			if ((token = Parse(line)) is NumberToken)
			{
				parseSuccessful = true;
			}

			return parseSuccessful;
		}

		/// <summary>
		/// Gets a Token of type NumberToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public new static Token Parse(string line)
		{
			Token token = new NullToken();

			if (NumberToken.__numberPattern.IsMatch(line))
			{
				string matchText = NumberToken.__numberPattern.Matches(line)[0].Value;
				string value = AsDecimal(matchText);

				token = new NumberToken(value, matchText);
			}

			return token;
		}

		/// <summary>
		/// Gets a Token of type NumberToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token ParseAsConstant(string line)
		{
			Token token = new NullToken();

			if (NumberToken.__numberPattern.IsMatch(line))
			{
				string matchText = NumberToken.__numberPattern.Matches(line)[0].Value;

				token = new NumberToken(matchText, "$");
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

				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("2");
			}
			else if (NumberToken.__octNumberPattern.IsMatch(number))
			{
				number = Convert.ToInt64(number.Substring(0, length), 8).ToString();

				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("8");
			}
			else if (NumberToken.__hexNumberPattern.IsMatch(number))
			{
				number = Convert.ToInt64(number.Substring(0, length), 16).ToString();

				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("16");
			}
			else
			{
				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("10");
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

		/// <summary>
		/// Gets value as a hexadecimal value with h notation.
		/// </summary>
		/// <returns></returns>
		public string AsHex()
		{
			return String.Concat(Convert.ToString(long.Parse(WholePart()), 16), "h");
		}

		/// <summary>
		/// Returns value as an octal value with o notation.
		/// </summary>
		/// <returns></returns>
		public string AsOct()
		{
			return String.Concat(Convert.ToString(long.Parse(WholePart()), 8), "o");
		}

		/// <summary>
		/// Returns value as a binary value with b notation.
		/// </summary>
		/// <returns></returns>
		public string AsBin()
		{
			return String.Concat(Convert.ToString(long.Parse(WholePart()), 2), "b");
		}

		/// <summary>
		/// Returns the negation of this NumberToken.
		/// </summary>
		/// <returns></returns>
		public Token Negate()
		{
			Token result = this;

			if (ToString().StartsWith("-"))
			{
				result = new NumberToken(ToString().Substring(1), Representation);
			}
			else
			{
				result = new NumberToken(String.Concat("-", ToString()), Representation);
			}

			return result;
		}

		#endregion
	}
}

/*
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
	public class BooleanToken : Token
	{
		#region Fields

		private static readonly Regex __booleanPattern = new Regex("^(true|false){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

		private string _representation;

		#endregion

		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Number; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Binary; }
		}

		public string Representation
		{
			get { return _representation; }
		}

		#endregion

		#region Constructors

		public BooleanToken(string value)
			: base(value.ToLowerInvariant())
		{
			_representation = value;
		}

		public BooleanToken(Token other)
			: base(other)
		{
			_representation = other.ToString();
		}

		public BooleanToken(string value, string rep)
			: base(value)
		{
			_representation = rep;
		}

		public BooleanToken(Token other, string rep)
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

			if (token is BooleanToken)
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

			if ((token = Parse(line)) is BooleanToken)
			{
				parseSuccessful = true;
			}

			return parseSuccessful;
		}

		/// <summary>
		/// Gets a Token of type BooleanToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public new static Token Parse(string line)
		{
			Token token = new NullToken();

			if (BooleanToken.__booleanPattern.IsMatch(line))
			{
				string matchText = BooleanToken.__booleanPattern.Matches(line)[0].Value;

				token = new BooleanToken(matchText);
			}

			return token;
		}

		/// <summary>
		/// Gets a Token of type BooleanToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token ParseAsConstant(string line)
		{
			Token token = new NullToken();

			if (BooleanToken.__booleanPattern.IsMatch(line))
			{
				string matchText = BooleanToken.__booleanPattern.Matches(line)[0].Value;

				token = new BooleanToken(matchText, "$");
			}

			return token;
		}

		/// <summary>
		/// Converts boolean to number.
		/// </summary>
		/// <returns></returns>
		public string AsNumber()
		{
			return ToString().Equals("true", StringComparison.InvariantCultureIgnoreCase) ? "1b" : "0b";
		}

		/// <summary>
		/// Returns the inverse of this BooleanToken.
		/// </summary>
		/// <returns></returns>
		public Token Not()
		{
			Token result = this;

			if (ToString().Equals("true", StringComparison.InvariantCultureIgnoreCase))
			{
				result = new BooleanToken("false");
			}
			else
			{
				result = new BooleanToken("true");
			}

			return result;
		}

		#endregion
	}
}

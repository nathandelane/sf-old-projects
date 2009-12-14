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
	public class LastResultToken : NumberToken
	{
		#region Fields

		private static readonly Regex __constantPattern = new Regex("^([$]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		#endregion

		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Constant; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Constant; }
		}

		#endregion

		#region Constructors

		public LastResultToken(string value, string rep)
			: base(value, rep)
		{
		}

		public LastResultToken(Token other, string rep)
			: base(other, rep)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Attempts to parse a token and returns success or failure.
		/// </summary>
		/// <param name="line">String from which to take the next token.</param>
		/// <param name="token">Out parameter to send token to if successful.</param>
		/// <returns></returns>
		public new static bool TryParse(string line, out Token token)
		{
			bool parseSuccessful = false;

			token = Parse(line);

			if (token is BooleanToken || token is NumberToken)
			{
				parseSuccessful = true;
			}

			return parseSuccessful;
		}

		/// <summary>
		/// Gets a Token of type LastResultToken from the beginning of a line of text. The resulting token 
		/// needs to be a value token, like BooleanToken or NumberToken.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public new static Token Parse(string line)
		{
			Token token = new NullToken();

			if (LastResultToken.__constantPattern.IsMatch(line))
			{
				string matchText = LastResultToken.__constantPattern.Matches(line)[0].Value;

				if (matchText.Equals("$", StringComparison.InvariantCultureIgnoreCase))
				{
					token = new ConstantToken(CalculatorContext.GetInstance().GetLastResult(), matchText);

					Token intermediateToken = new NullToken();

					if (NumberToken.TryParse(token.ToString(), out intermediateToken, true)
						|| BooleanToken.TryParse(token.ToString(), out intermediateToken, true))
					{
						token = intermediateToken;
					}
				}
			}

			return token;
		}

		#endregion
	}
}

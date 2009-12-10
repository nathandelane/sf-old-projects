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
	public class FunctionToken : Token
	{
		#region Fields

		private static readonly Regex __functionPattern = new Regex("^(cos|sin|tan|acos|asin|atan|sqrt|tod|toh|too|tob|[*]{2}|[/]{2}|[%]{1}|[!]{1}|-\\(){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __singleArgFunction = new Regex("^(cos|sin|tan|acos|asin|atan|sqrt|tod|toh|too|tob|[!]{1}|[-]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __twoArgFunction = new Regex("^([*]{2}|[/]{2}|[%]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private ExpressionPrecedence _precedence;
		private int _numArguments;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the TokenType.
		/// </summary>
		public override TokenType Type
		{
			get { return TokenType.Function; }
		}

		/// <summary>
		/// Gets the ExpressionPrecedence.
		/// </summary>
		public override ExpressionPrecedence Precedence
		{
			get { return _precedence; }
		}

		/// <summary>
		/// Gets the number of arguments required for the function.
		/// </summary>
		public int NumArguments
		{
			get { return _numArguments; }
		}

		#endregion

		#region Constructors

		public FunctionToken(string value)
			: base(value)
		{
			_precedence = ExpressionPrecedence.Function;

			DetermineArgumentCount(value);
		}

		public FunctionToken(Token other)
			: base(other)
		{
			_precedence = ExpressionPrecedence.Function;

			DetermineArgumentCount(other.ToString());
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a Token of type FunctionToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token Parse(string line)
		{
			Token token = new NullToken();

			if (FunctionToken.__functionPattern.IsMatch(line))
			{
				string matchText = FunctionToken.__functionPattern.Matches(line)[0].Value;

				if (matchText.Equals("-(", StringComparison.InvariantCultureIgnoreCase))
				{
					matchText = "-";
				}

				token = new FunctionToken(matchText);
			}

			return token;
		}

		/// <summary>
		/// Determine the number of arguments a function needs.
		/// </summary>
		/// <param name="value"></param>
		private void DetermineArgumentCount(string value)
		{
			if (FunctionToken.__singleArgFunction.IsMatch(value))
			{
				_numArguments = 1;
			}
			else if (FunctionToken.__twoArgFunction.IsMatch(value))
			{
				_numArguments = 2;
			}
		}

		#endregion
	}
}

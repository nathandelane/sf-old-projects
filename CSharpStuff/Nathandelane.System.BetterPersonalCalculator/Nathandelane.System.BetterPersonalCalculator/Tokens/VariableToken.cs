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
	public class VariableToken : NumberToken
	{
		#region Fields

		private static readonly Regex __variablePattern = new Regex("^[A-Za-z_]{1}[A-Za-z_\\d]+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		#endregion

		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Variable; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Variable; }
		}

		#endregion

		#region Constructors

		public VariableToken(string value)
			: base(value)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a Token of type NumberToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public new static Token Parse(string line)
		{
			Token token = new NullToken();

			if (VariableToken.__variablePattern.IsMatch(line))
			{
				string matchText = VariableToken.__variablePattern.Matches(line)[0].Value;

				token = new VariableToken(matchText);
			}

			return token;
		}

		#endregion
	}
}

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

namespace Nathandelane.System.Bpc
{
	public class OperatorToken : Token
	{
		#region Properties

		/// <summary>
		/// Gets this OperatorToken's token type.
		/// </summary>
		public override TokenType Type
		{
			get { return TokenType.Operator; }
		}

		/// <summary>
		/// Gets this OperatorToken's precedence.
		/// </summary>
		public override ExpressionPrecedence Precedence
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of OperatorToken.
		/// </summary>
		/// <param name="value"></param>
		protected OperatorToken(string value)
			: base(value)
		{
		}

		/// <summary>
		/// Creates an instance of OperatorToken.
		/// </summary>
		/// <param name="other"></param>
		protected OperatorToken(Token other)
			: base(other)
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
		public static bool TryParse(string line, out Token token)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets a Token of type OperatorToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public new static Token Parse(string line)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

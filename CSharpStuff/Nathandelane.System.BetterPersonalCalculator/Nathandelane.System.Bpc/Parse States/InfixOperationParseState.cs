﻿/*
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
	public class InfixOperationParseState : TokenParseState
	{
		#region Methods

		/// <summary>
		/// Gets the next token while within an infix operation parse state.
		/// </summary>
		/// <param name="internalLine"></param>
		/// <param name="token"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public override bool GetNextToken(string internalLine, out Token token, out TokenParseState state)
		{
			bool result = false;

			if ((result = PrefixFunctionToken.TryParse(internalLine, out token)))
			{
				state = new PrefixFunctionTokenParseState();
			}
			else if((result = BooleanToken.TryParse(internalLine, out token) || ConstantToken.TryParse(internalLine, out token) || NumberToken.TryParse(internalLine, out token) || LastResultToken.TryParse(internalLine, out token)))
			{
				state = new ValueTokenParseState();
			}
			else if((result = VariableToken.TryParse(internalLine, out token)))
			{
				state = new VariableTokenParseState();
			}
			else if((result = (PerenthesisToken.TryParse(internalLine, out token) && ((PerenthesisToken)token).PerenthesisType == PerenthesisTokenType.Open)))
			{
				state = new PerenthesisTokenParseState();
			}
			else if((result = CommentToken.TryParse(internalLine, out token)))
			{
				state = new NullTokenParseState();
			}
			else
			{
				state = new InvalidTokenParseState();
			}

			return result;
		}

		#endregion
	}
}

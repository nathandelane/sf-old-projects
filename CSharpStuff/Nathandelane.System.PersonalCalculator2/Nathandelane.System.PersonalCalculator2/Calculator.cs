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
using System.Linq;
using System.Text;
using System.Collections;

namespace Nathandelane.System.PersonalCalculator2
{
	public static class Calculator
	{
		#region Fields

		#region ReadOnly Fields

		public static readonly TokenPatterns Patterns = new TokenPatterns()
		{
			{ TokenPatterns.HexadecimalNumberKey, TokenType.HexadecimalNumber },
			{ TokenPatterns.DecimalNumberKey, TokenType.DecimalNumber },
			{ TokenPatterns.AdditionKey, TokenType.Add },
			{ TokenPatterns.SubtractionKey, TokenType.Subtract },
			{ TokenPatterns.PowerKey, TokenType.Power },
			{ TokenPatterns.FactorialKey, TokenType.Factorial },
			{ TokenPatterns.MultiplicationKey, TokenType.Multiply },
			{ TokenPatterns.ModulusKey, TokenType.Modulus },
			{ TokenPatterns.DivKey, TokenType.Div },
			{ TokenPatterns.DivisionKey, TokenType.Divide },
			{ TokenPatterns.LeftParenthesisKey, TokenType.OpeningParenthesis },
			{ TokenPatterns.RightParenthesisKey, TokenType.ClosingParenthesis },
			{ TokenPatterns.FunctionKey, TokenType.Function }
		};

		#endregion

		#region ReadWrite Fields

		public static Hashtable Heap = new Hashtable();

		#endregion

		#endregion
	}
}

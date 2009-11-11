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

namespace Nathandelane.System.PersonalCalculator2
{
	public class PrecedenceMap
	{
		#region Fields

		private static Dictionary<TokenType, int> _precedence = new Dictionary<TokenType, int>()
		{
			{ TokenType.Power, 0 },
			{ TokenType.Add, 1 },
			{ TokenType.Subtract, 1},
			{ TokenType.Multiply, 2 },
			{ TokenType.Divide, 2 },
			{ TokenType.Modulus, 2 },
			{ TokenType.Div, 2},
			{ TokenType.Factorial, 3 },
			{ TokenType.OpeningParenthesis, -1 },
			{ TokenType.ClosingParenthesis, -1 },
			{ TokenType.Function, -1 }
		};

		#endregion

		#region Properties

		public int this[TokenType key]
		{
			get { return _precedence[key]; }
		}

		#endregion
	}
}

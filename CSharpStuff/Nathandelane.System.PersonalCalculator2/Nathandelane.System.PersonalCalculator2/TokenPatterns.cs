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
using System.Text.RegularExpressions;

namespace Nathandelane.System.PersonalCalculator2
{
	public class TokenPatterns : Dictionary<string, TokenType>
	{
		#region Fields

		public static readonly string HexadecimalNumberKey = "^[\\dA-Fa-f]+(h){1}";
		public static readonly string NegativeHexadecimalNumberKey = "^[-]{0,1}[\\dA-Fa-f]+(h){1}";
		public static readonly string DecimalNumberKey = "^[\\d]+([.]{1}[\\d]+){0,1}";
		public static readonly string NegativeDecimalNumberKey = "^[-]{0,1}[\\d]+([.]{1}[\\d]+){0,1}";
		public static readonly string AdditionKey = "^[+]{1}";
		public static readonly string SubtractionKey = "^[-]{1}";
		public static readonly string PowerKey = "^(\\*\\*){1}";
		public static readonly string FactorialKey = "^(!){1}";
		public static readonly string MultiplicationKey = "^[*]{1}";
		public static readonly string ModulusKey = "^[%]{1}";
		public static readonly string DivKey = "^(//){1}";
		public static readonly string DivisionKey = "^[/]{1}";
		public static readonly string LeftParenthesisKey = "^[(]{1}";
		public static readonly string RightParenthesisKey = "^[)]{1}";
		public static readonly string FunctionKey = "^(sinh|cosh|tanh|sin|cos|tan|asin|acos|atan|sqrt|tohx|todc){1}";

		#endregion

		#region Constructors

		public TokenPatterns()
			: base()
		{
		}

		#endregion
	}
}

﻿using System;
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

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

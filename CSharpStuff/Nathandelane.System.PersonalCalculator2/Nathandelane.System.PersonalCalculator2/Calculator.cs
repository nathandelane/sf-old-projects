using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator2
{
	public static class Calculator
	{
		public static readonly TokenPatterns Patterns = new TokenPatterns()
		{ 
			{ TokenPatterns.DecimalNumberKey, TokenType.DecimalNumber },
			{ TokenPatterns.AdditionKey, TokenType.Add },
			{ TokenPatterns.SubtractionKey, TokenType.Subtract },
			{ TokenPatterns.PowerKey, TokenType.Power },
			{ TokenPatterns.MultiplicationKey, TokenType.Multiply },
			{ TokenPatterns.DivisionKey, TokenType.Divide },
			{ TokenPatterns.LeftParenthesisKey, TokenType.OpeningParenthesis },
			{ TokenPatterns.RightParenthesisKey, TokenType.ClosingParenthesis }
		};
	}
}

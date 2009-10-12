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
			{ "^[\\d]+([.]{1}[\\d]+){0,1}", TokenType.DecimalNumber },
			{ "^[+]{1}", TokenType.Add },
			{ "^[-]{1}", TokenType.Subtract },
			{ "^(\\*\\*){1}", TokenType.Power },
			{ "^[*]{1}", TokenType.Multiply },
			{ "^[/]{1}", TokenType.Divide }
		};
	}
}

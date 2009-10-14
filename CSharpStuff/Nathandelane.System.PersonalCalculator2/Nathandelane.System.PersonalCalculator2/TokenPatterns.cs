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

		public static readonly string DecimalNumberKey = "^[\\d]+([.]{1}[\\d]+){0,1}";
		public static readonly string NegativeDecimalNumberKey = "^[-]{0,1}[\\d]+([.]{1}[\\d]+){0,1}";
		public static readonly string AdditionKey = "^[+]{1}";
		public static readonly string SubtractionKey = "^[-]{1}";
		public static readonly string PowerKey = "^(\\*\\*){1}";
		public static readonly string MultiplicationKey = "^[*]{1}";
		public static readonly string ModulusKey = "^[%]{1}";
		public static readonly string DivKey = "^(//){1}";
		public static readonly string DivisionKey = "^[/]{1}";
		public static readonly string LeftParenthesisKey = "^[(]{1}";
		public static readonly string RightParenthesisKey = "^[)]{1}";

		#endregion

		#region Constructors

		public TokenPatterns()
			: base()
		{
		}

		#endregion
	}
}

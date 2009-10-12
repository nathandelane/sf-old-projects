using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Nathandelane.System.PersonalCalculator2
{
	public class TokenPatterns : Dictionary<string, TokenType>
	{
		#region Fields

		public static readonly string DecimalNumberKey = "^[-]{0,1}[\\d]+([.]{1}[\\d]+){0,1}";
		public static readonly string AdditionKey = "^[+]{1}";
		public static readonly string SubtractionKey = "^[-]{1}";
		public static readonly string PowerKey = "^(\\*\\*){1}";
		public static readonly string MultiplicationKey = "^[*]{1}";
		public static readonly string DivisionKey = "^[/]{1}";

		#endregion

		#region Constructors

		public TokenPatterns()
			: base()
		{
		}

		#endregion
	}
}

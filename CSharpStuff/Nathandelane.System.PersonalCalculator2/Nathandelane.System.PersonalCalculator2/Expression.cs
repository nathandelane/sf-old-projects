using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator2
{
	public class Expression
	{
		#region Fields

		private IEnumerable<IToken> _tokens;
		private static Expression _instance;

		#endregion

		#region Constructors

		private Expression()
		{
			_tokens = new List<IToken>();
		}

		#endregion

		#region Methods

		public static Expression Evaluate(string expression)
		{
			_instance = new Expression();

			expression = Parse(expression);

			return _instance;
		}

		private static string Parse(string expression)
		{
			if (expression.Contains("("))
			{
				int perenCount = 1;
				expression = Parse(perenCount, expression.Substring(expression.IndexOf("(") + 1));
			}

			return expression;
		}

		private static string Parse(int perenCount, string expression)
		{
			return expression;
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.PersonalCalculator
{
	public class SpecialNumber
	{
		#region Fields

		public static readonly Dictionary<TokenType, Regex> ExtractionTable = new Dictionary<TokenType, Regex>()
		{
			{ TokenType.DollarSign, new Regex("([\\$]{1})") },
			{ TokenType.E, new Regex("([e]{1})") },
			{ TokenType.Pi, new Regex("(pi){1}") }
		};

		#endregion

		#region Methods

		public static bool IsMatch(string expression)
		{
			bool result = false;

			if (ExtractionTable[TokenType.DollarSign].IsMatch(expression) || ExtractionTable[TokenType.E].IsMatch(expression) || ExtractionTable[TokenType.Pi].IsMatch(expression))
			{
				result = true;
			}

			return result;
		}

		public static string Inject(string expression)
		{
			while (ExtractionTable[TokenType.DollarSign].IsMatch(expression) || ExtractionTable[TokenType.E].IsMatch(expression) || ExtractionTable[TokenType.Pi].IsMatch(expression))
			{
				if (ExtractionTable[TokenType.DollarSign].IsMatch(expression))
				{
					expression = expression.Replace("$", Calculator.State["$"]);
				}
				if (ExtractionTable[TokenType.E].IsMatch(expression))
				{
					expression = expression.Replace("e", String.Format("{0}", Math.E));
				}
				else if (ExtractionTable[TokenType.Pi].IsMatch(expression))
				{
					expression = expression.Replace("pi", String.Format("{0}", Math.PI));
				}
			}

			return expression;
		}

		#endregion
	}
}

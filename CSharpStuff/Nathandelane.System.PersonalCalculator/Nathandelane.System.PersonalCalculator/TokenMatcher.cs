using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.PersonalCalculator
{
	static class TokenMatcher
	{
		#region Fields

		private static readonly string __numberFormat = "([\\d]+([.]{0,1}[\\d]+){0,1}){1}";
		private static readonly string __operatorFormat = "[+-/*()]{1}";
		public static readonly Dictionary<TokenType, Regex> ExtractionTable = new Dictionary<TokenType, Regex>()
		{
			{ TokenType.Addition, new Regex(String.Format("{0}(\\+){{1}}{0}", __numberFormat)) },
			{ TokenType.Division, new Regex(String.Format("{0}(/){{1}}{0}", __numberFormat)) },
			{ TokenType.LastResult, new Regex("[\\$]{1}") },
			{ TokenType.Multiplication, new Regex(String.Format("{0}(\\*){{1}}{0}", __numberFormat)) },
			{ TokenType.Negation, new Regex(String.Format("(-){{1}}{0}", __numberFormat)) },
			{ TokenType.Number, new Regex(__numberFormat) },
			{ TokenType.SubExpression, new Regex(String.Format("(\\(){{1}}({1}|{0})+(\\)){{1}}", __numberFormat, __operatorFormat)) },
			{ TokenType.Subtraction, new Regex(String.Format("{0}(-){{1}}{0}", __numberFormat)) }
		};

		#endregion

		#region Methods

		public static SubExpression Match(string expression)
		{
			SubExpression subExpression = new SubExpression(String.Empty, TokenType.Undefined);

			if (ExtractionTable[TokenType.SubExpression].IsMatch(expression))
			{
				subExpression = GetMatch(TokenType.SubExpression, expression);
			}
			else if(ExtractionTable[TokenType.LastResult].IsMatch(expression))
			{
				subExpression = GetMatch(TokenType.LastResult, expression);
			}
			else if (ExtractionTable[TokenType.Division].IsMatch(expression) || ExtractionTable[TokenType.Multiplication].IsMatch(expression))
			{
				if (ExtractionTable[TokenType.Division].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.Division, expression);
				}
				else
				{
					subExpression = GetMatch(TokenType.Multiplication, expression);
				}
			}
			else if (ExtractionTable[TokenType.Negation].IsMatch(expression) || ExtractionTable[TokenType.Subtraction].IsMatch(expression) || ExtractionTable[TokenType.Addition].IsMatch(expression))
			{
				if (ExtractionTable[TokenType.Addition].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.Addition, expression);
				}
				else if (ExtractionTable[TokenType.Subtraction].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.Subtraction, expression);
				}
				else if (ExtractionTable[TokenType.Negation].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.Negation, expression);
				}
			}

			return subExpression;
		}

		public static bool IsExpression(string expression)
		{
			bool result = false;

			Regex regexExpression = new Regex(String.Format("^(\\(|{0}|-){{1}}({1}|{0})+(\\)|{0}){{1}}$", __numberFormat, __operatorFormat));
			if (regexExpression.IsMatch(expression))
			{
				result = true;
			}

			Regex regexNumber = ExtractionTable[TokenType.Number];
			if (regexNumber.IsMatch(expression))
			{
				SubExpression exp = GetMatch(TokenType.Number, expression);
				if (exp.Expression.Length == expression.Length)
				{
					result = false;
				}
			}

			return result;
		}

		public static bool IsNotSimpleNegation(string expression)
		{
			bool result = true;
			Regex regex = ExtractionTable[TokenType.Negation];

			if (regex.IsMatch(expression))
			{
				result = false;
			}

			return result;
		}

		private static SubExpression GetMatch(TokenType type, string expression)
		{
			SubExpression subExpression = new SubExpression(String.Empty, TokenType.Undefined);
			string subExp = ExtractionTable[type].Matches(expression)[0].Value;
			if (type == TokenType.SubExpression)
			{
				int index = subExp.IndexOf(")");
				if (index < subExp.Length - 1)
				{
					subExp = subExp.Substring(0, index + 1);
				}

				subExpression = new SubExpression(subExp, TokenType.SubExpression);
			}
			else if(type == TokenType.LastResult)
			{

			}
			else
			{
				subExpression = new SubExpression(subExp, type);
			}

			return subExpression;
		}

		#endregion
	}
}

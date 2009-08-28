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

		private static readonly string __decimalNumberFormat = "(([-]{0,1}[\\d]+([.]{0,1}[\\d]+){0,1})([d]{0,1}))";
		private static readonly string __binaryNumberFormat = "(([-]{0,1}[01]+)([b]{1}))";
		private static readonly string __hexNumberFormat = "(([-]{0,1}[\\dABCDEF]+)([h]{1}))";
		private static readonly string __octalNumberFormat = "(([-]{0,1}[01234567]+)([o]{1}))";
		private static readonly string __numberFormat = String.Format("(({0}|{1}|{2}|{3})+)", __hexNumberFormat, __decimalNumberFormat, __octalNumberFormat, __binaryNumberFormat);
		private static readonly string __operatorFormat = "((\\*\\*|[+-/*\\(\\)^%&|]){1})";
		private static readonly string __functionFormat = "(([-]{0,1}([bdho]|cos|deg|rad|sin|tan){1})([\\(]{1})((([-]{0,1}[\\dABCDEF]+)([h]{1}))|(([-]{0,1}[\\d]+([.]{0,1}[\\d]+){0,1})([d]{0,1}))|(([-]{0,1}[01234567]+)([o]{1}))|(([-]{0,1}[01]+)([b]{1}))|((\\*\\*|[+-/*\\(\\)^%&|]){1})|([-]{0,1}([bdho]{1}|cos|sin|tan){1})|([-]{0,1}(e|pi|\\$){1}))+([\\)]{1}))";
		private static readonly string __variableFormat = "((([A-Za-z_]+[\\d_]*)|mode)+)";
		private static readonly string __specialNumberFormat = "((e|pi){1})";
		private static readonly string __subExpressionFormat = "([\\(]{1})((([-]{0,1}[\\dABCDEF]+)([h]{1}))|(([-]{0,1}[\\d]+([.]{0,1}[\\d]+){0,1})([d]{0,1}))|(([-]{0,1}[01234567]+)([o]{1}))|(([-]{0,1}[01]+)([b]{1}))|((\\*\\*|[+-/*\\(\\)^%&|]){1})|([-]{0,1}(e|pi|\\$){1})|(([-]{0,1}([bdho]|cos|sin|tan){1})([\\(]{1})((([-]{0,1}[\\dABCDEF]+)([h]{1}))|(([-]{0,1}[\\d]+([.]{0,1}[\\d]+){0,1})([d]{0,1}))|(([-]{0,1}[01234567]+)([o]{1}))|(([-]{0,1}[01]+)([b]{1}))|((\\*\\*|[+-/*\\(\\)^%&|]){1})|([-]{0,1}([bdho]{1}|cos|sin|tan){1})|([-]{0,1}(e|pi|\\$){1}))+([\\)]{1})))+([\\)]{1})";
		public static readonly Dictionary<TokenType, Regex> ExtractionTable = new Dictionary<TokenType, Regex>()
		{
			{ TokenType.Addition, new Regex(String.Format("{0}(\\+){{1}}{0}", __numberFormat)) },
			{ TokenType.And, new Regex(String.Format("{0}(&){{1}}{0}", __numberFormat)) },
			{ TokenType.Assignment, new Regex(String.Format("{0}[=]{{1}}{1}", __variableFormat, __numberFormat)) },
			{ TokenType.BinaryNumber, new Regex(String.Format("{0}", __binaryNumberFormat)) },
			{ TokenType.ConversionFunction, new Regex(String.Format("([bohd]|rad|deg){{1}}{0}", __subExpressionFormat), RegexOptions.Compiled) },
			{ TokenType.DecimalNumber, new Regex(String.Format("{0}", __decimalNumberFormat)) },
			{ TokenType.Division, new Regex(String.Format("{0}(/){{1}}{0}", __numberFormat)) },
			{ TokenType.Function, new Regex(String.Format("{0}", __functionFormat)) },
			{ TokenType.HexNumber, new Regex(String.Format("{0}", __hexNumberFormat)) },
			{ TokenType.LastResult, new Regex("[\\$]{1}") },
			{ TokenType.Multiplication, new Regex(String.Format("{0}(\\*){{1}}{0}", __numberFormat)) },
			{ TokenType.Modulus, new Regex(String.Format("{0}(%){{1}}{0}", __numberFormat)) },
			{ TokenType.Negation, new Regex(String.Format("(-){{1}}{0}", __numberFormat)) },
			{ TokenType.Number, new Regex(__numberFormat, RegexOptions.Compiled) },
			{ TokenType.NumericResult, new Regex(String.Format("^[-]{{0,1}}{0}$", __numberFormat)) },
			{ TokenType.OctalNumber, new Regex(String.Format("{0}", __octalNumberFormat)) },
			{ TokenType.Or, new Regex(String.Format("{0}(\\|){{1}}{0}", __numberFormat)) },
			{ TokenType.Power, new Regex(String.Format("{0}(\\*\\*){{1}}{0}", __numberFormat)) },
			{ TokenType.SpecialNumber, new Regex(__specialNumberFormat) },
			{ TokenType.SubExpression, new Regex(__subExpressionFormat, RegexOptions.Compiled) },
			{ TokenType.Subtraction, new Regex(String.Format("{0}(-){{1}}{0}", __numberFormat)) },
			{ TokenType.TrigFunction, new Regex(String.Format("((sin|cos|tan){{1}}){0}", __subExpressionFormat), RegexOptions.Compiled) },
			{ TokenType.Variable, new Regex(String.Format("{0}", __variableFormat)) },
			{ TokenType.Xor, new Regex(String.Format("{0}(\\^){{1}}{0}", __numberFormat)) },
		};

		#endregion

		#region Methods

		public static SubExpression Match(string expression)
		{
			SubExpression subExpression = new SubExpression(String.Empty, TokenType.Undefined);

			if (ExtractionTable[TokenType.Function].IsMatch(expression))
			{
				if (ExtractionTable[TokenType.ConversionFunction].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.ConversionFunction, expression);
				}
				else if (ExtractionTable[TokenType.TrigFunction].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.TrigFunction, expression);
				}
			}
			else if (ExtractionTable[TokenType.SubExpression].IsMatch(expression))
			{
				subExpression = GetMatch(TokenType.SubExpression, expression);
			}
			else if (ExtractionTable[TokenType.Modulus].IsMatch(expression) || ExtractionTable[TokenType.Power].IsMatch(expression))
			{
				if (ExtractionTable[TokenType.Modulus].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.Modulus, expression);
				}
				else
				{
					subExpression = GetMatch(TokenType.Power, expression);
				}
			}
			else if (ExtractionTable[TokenType.And].IsMatch(expression) || ExtractionTable[TokenType.Or].IsMatch(expression) || ExtractionTable[TokenType.Xor].IsMatch(expression))
			{
				if (ExtractionTable[TokenType.And].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.And, expression);
				}
				else if (ExtractionTable[TokenType.Or].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.Or, expression);
				}
				else if (ExtractionTable[TokenType.Xor].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.Xor, expression);
				}
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
			else if (ExtractionTable[TokenType.SpecialNumber].IsMatch(expression))
			{
				subExpression = GetMatch(TokenType.SpecialNumber, expression);
			}
			else if (ExtractionTable[TokenType.Assignment].IsMatch(expression) || ExtractionTable[TokenType.Variable].IsMatch(expression))
			{
				if (ExtractionTable[TokenType.Assignment].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.Assignment, expression);
				}
				else if (ExtractionTable[TokenType.Variable].IsMatch(expression))
				{
					subExpression = GetMatch(TokenType.Variable, expression);
				}
			}

			return subExpression;
		}

		public static bool IsExpression(string expression)
		{
			bool result = false;

			Regex regexExpression = new Regex(String.Format("^({0}|{1}|{2}|{3}|{4})+$", __subExpressionFormat, __functionFormat, __specialNumberFormat, __numberFormat, __operatorFormat), RegexOptions.Compiled);
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
				SubExpression exp = GetMatch(TokenType.Negation, expression);
				if (exp.Expression.Length == expression.Length)
				{
					result = false;
				}
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
				if (index != -1 && index < subExp.Length - 1)
				{
					subExp = subExp.Substring(0, index + 1);
				}
				else if (index == -1)
				{
					throw new ArgumentException("Your expression was malformed and appeared to be missing a closing parenthesis.");
				}

				subExpression = new SubExpression(subExp, TokenType.SubExpression);
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

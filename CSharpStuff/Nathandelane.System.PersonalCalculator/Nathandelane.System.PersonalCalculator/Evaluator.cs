using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.PersonalCalculator
{
	public class Evaluator
	{
		#region Fields

		private string _result;

		#endregion

		#region Properties

		public string Result
		{
			get { return _result; }
		}

		#endregion

		#region Constructors

		private Evaluator()
		{
			_result = String.Empty;
		}

		#endregion

		#region Methods

		public static Evaluator Evaluate(string expression)
		{
			Evaluator evaluation = new Evaluator();

			if (expression.Length > 0)
			{
				while (TokenMatcher.IsExpression(expression) && TokenMatcher.IsNotSimpleNegation(expression))
				{
					SubExpression exp = TokenMatcher.Match(expression);
					string subExp = exp.Expression;
					TokenType type = exp.Operator;
					if (type == TokenType.Addition || type == TokenType.Division || type == TokenType.Multiplication || type == TokenType.Negation || type == TokenType.Subtraction)
					{
						expression = PerformOperation(expression, type, subExp);
					}
					else if (type == TokenType.SubExpression)
					{
						expression = EvaluateSubExpression(expression, subExp);
					}
				}

				evaluation._result = expression;
			}
			else
			{
				evaluation._result = "Undefined";
			}

			return evaluation;
		}

		private static string EvaluateSubExpression(string expression, string subExp)
		{
			string fixedSubExp = subExp.Substring(1, subExp.Length - 2);
			Evaluator innerEvaluation = Evaluate(fixedSubExp);
			string result = innerEvaluation._result;
			int index = expression.IndexOf(subExp);
			int length = subExp.Length;

			if (index > 0)
			{
				string subStrLeft = expression.Substring(0, index);
				string subStrRight = expression.Substring(index + subExp.Length);
				expression = String.Concat(subStrLeft, result, subStrRight);
			}
			else
			{
				expression = expression.Remove(index, length);
				expression = String.Concat(result, expression);
			}

			return expression;
		}

		private static string PerformOperation(string expression, TokenType type, string subExp)
		{
			Regex regex = TokenMatcher.ExtractionTable[TokenType.Number];
			if (regex.Matches(subExp).Count == 2)
			{
				string left = regex.Matches(subExp)[0].Value;
				string right = regex.Matches(subExp)[1].Value;
				double result = 0.0d;
				switch (type)
				{
					case TokenType.Addition:
						result = double.Parse(left) + double.Parse(right);
						break;
					case TokenType.Subtraction:
						result = double.Parse(left) - double.Parse(right);
						break;
					case TokenType.Division:
						result = double.Parse(left) / double.Parse(right);
						break;
					case TokenType.Multiplication:
						result = double.Parse(left) * double.Parse(right);
						break;
				}
				int index = expression.IndexOf(subExp);
				int length = subExp.Length;

				if (index > 0)
				{
					string subStrLeft = expression.Substring(0, index);
					string subStrRight = expression.Substring(index + subExp.Length);
					expression = String.Concat(subStrLeft, result, subStrRight);
				}
				else
				{
					expression = expression.Remove(index, length);
					expression = String.Concat(result, expression);
				}
			}
			else if (regex.Matches(subExp).Count == 1)
			{
				string left = regex.Matches(subExp)[0].Value;
				double result = double.Parse(left) * (-1);
				int index = expression.IndexOf(subExp);
				int length = subExp.Length;

				if (index > 0)
				{
					string subStrLeft = expression.Substring(0, index);
					string subStrRight = expression.Substring(index + subExp.Length);
					expression = String.Concat(subStrLeft, result, subStrRight);
				}
				else
				{
					expression = expression.Remove(index, length);
					expression = String.Concat(result, expression);
				}
			}

			return expression;
		}

		public override string ToString()
		{
			return _result;
		}

		#endregion
	}
}

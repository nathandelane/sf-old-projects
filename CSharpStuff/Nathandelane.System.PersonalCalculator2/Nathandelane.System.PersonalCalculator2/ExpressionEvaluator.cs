using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.PersonalCalculator2
{
	public class ExpressionEvaluator
	{
		#region Fields

		private static string _value;

		#endregion

		#region Constructors

		private ExpressionEvaluator(string value)
		{
			_value = value;
		}

		#endregion

		#region Methods

		public static ExpressionEvaluator Evaluate(string[] tokens)
		{
			Stack<string> postfixatedExpression = PostfixateExpression(tokens);
			ExpressionEvaluator evaluator = new ExpressionEvaluator(EvaluateExpression(postfixatedExpression));
			
			return evaluator;
		}

		private static string EvaluateExpression(Stack<string> postfixatedExpression)
		{
			string result = "0";
			Stack<string> resultStack = new Stack<string>();

			while (postfixatedExpression.Count > 0)
			{
				string nextToken = postfixatedExpression.Pop();

				if (GetTokenType(nextToken) == TokenType.DecimalNumber)
				{
					resultStack.Push(nextToken);
				}
				else
				{
					string right = resultStack.Pop();
					string left = resultStack.Pop();

					resultStack.Push(DispatchOperation(GetTokenType(nextToken), left, right));
				}
			}

			result = resultStack.Pop();

			return result;
		}

		private static Stack<string> PostfixateExpression(string[] tokens)
		{
			PrecedenceMap precedenceMap = new PrecedenceMap();
			Stack<string> postfixatedExpression = new Stack<string>();
			Stack<string> operatorStack = new Stack<string>();
			TokenType lastTokenType = TokenType.Null;

			foreach (string nextToken in tokens)
			{
				string currentToken = nextToken;

				if (GetTokenType(currentToken) == TokenType.DecimalNumber)
				{
					if (lastTokenType == TokenType.Negation)
					{
						currentToken = currentToken.Prepend("-");
					}

					postfixatedExpression.Push(currentToken);

					lastTokenType = TokenType.DecimalNumber;
				}
				else
				{
					TokenType nextTokenType = GetTokenType(currentToken);
					TokenType nextOperator = (operatorStack.Count > 0) ? GetTokenType(operatorStack.Peek()) : TokenType.Null;

					if (GetTokenType(currentToken) == TokenType.Subtract && lastTokenType != TokenType.DecimalNumber)
					{
						lastTokenType = TokenType.Negation;
					}
					else
					{
						if (operatorStack.Count == 0)
						{
							operatorStack.Push(currentToken);
						}
						else
						{
							if (precedenceMap[nextTokenType].LessThan(precedenceMap[nextOperator]))
							{
								while ((precedenceMap[nextTokenType].LessThan(precedenceMap[nextOperator]) || precedenceMap[nextTokenType].EqualTo(precedenceMap[nextOperator])) && operatorStack.Count > 0)
								{
									postfixatedExpression.Push(operatorStack.Pop());
								}

								operatorStack.Push(currentToken);
							}
							else if (precedenceMap[nextTokenType].GreaterThan(precedenceMap[nextOperator]))
							{
								operatorStack.Push(currentToken);
							}
							else
							{
								postfixatedExpression.Push(operatorStack.Pop());
								operatorStack.Push(currentToken);
							}
						}
					}

					lastTokenType = GetTokenType(currentToken);
				}
			}

			while (operatorStack.Count > 0)
			{
				postfixatedExpression.Push(operatorStack.Pop());
			}

			Stack<string> expression = new Stack<string>();
			while (postfixatedExpression.Count > 0)
			{
				expression.Push(postfixatedExpression.Pop());
			}

			return expression;
		}

		private static string DispatchOperation(TokenType nextOperator, string left, string right)
		{
			string result = left;

			if (nextOperator == TokenType.Add)
			{
				result = (double.Parse(left) + double.Parse(right)).ToString();
			}
			else if (nextOperator == TokenType.Subtract)
			{
				result = (double.Parse(left) - double.Parse(right)).ToString();
			}
			else if (nextOperator == TokenType.Power)
			{
				result = Math.Pow(double.Parse(left), double.Parse(right)).ToString();
			}
			else if (nextOperator == TokenType.Multiply)
			{
				result = (double.Parse(left) * double.Parse(right)).ToString();
			}
			else if (nextOperator == TokenType.Divide)
			{
				result = (double.Parse(left) / double.Parse(right)).ToString();
			}

			return result;
		}

		private static TokenType GetTokenType(string token)
		{
			TokenType type = TokenType.DecimalNumber;

			if ((new Regex(TokenPatterns.AdditionKey)).IsMatch(token))
			{
				type = TokenType.Add;
			}
			else if ((new Regex(TokenPatterns.SubtractionKey)).IsMatch(token))
			{
				type = TokenType.Subtract;
			}
			else if ((new Regex(TokenPatterns.PowerKey)).IsMatch(token))
			{
				type = TokenType.Power;
			}
			else if ((new Regex(TokenPatterns.MultiplicationKey)).IsMatch(token))
			{
				type = TokenType.Multiply;
			}
			else if ((new Regex(TokenPatterns.DivisionKey)).IsMatch(token))
			{
				type = TokenType.Divide;
			}

			return type;
		}

		public override string ToString()
		{
			return _value;
		}

		#endregion
	}
}

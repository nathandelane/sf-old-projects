/*  Copyright (C) 2009, Nathandelane.
	License:
	Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.

	This program is free software; you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation; either version 3, or (at your option)
	any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
	02111-1307, USA.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

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

		/// <summary>
		/// Evaluates a series of tokens.
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns>ExpressionEvaluator</returns>
		public static ExpressionEvaluator Evaluate(string[] tokens)
		{
			Stack<string> postfixatedExpression = ExpressionEvaluator.PostfixateExpression(tokens);
			string result = ExpressionEvaluator.EvaluateExpression(postfixatedExpression);
			ExpressionEvaluator evaluator = new ExpressionEvaluator(result);
			
			return evaluator;
		}

		/// <summary>
		/// Evaluates a postfixated expression.
		/// </summary>
		/// <param name="postfixatedExpression"></param>
		/// <returns>string</returns>
		private static string EvaluateExpression(Stack<string> postfixatedExpression)
		{
			string result = "0";
			Stack<string> resultStack = new Stack<string>();

			while (postfixatedExpression.Count > 0)
			{
				string nextToken = postfixatedExpression.Pop();

				if (GetTokenType(nextToken) != TokenType.DecimalNumber && GetTokenType(nextToken) != TokenType.HexadecimalNumber)
				{
					if (GetTokenType(nextToken) == TokenType.Function)
					{
						string value = resultStack.Pop();

						resultStack.Push(DispatchFunction(nextToken, value));
					}
					else if (GetTokenType(nextToken) == TokenType.Factorial)
					{
						string left = resultStack.Pop();

						resultStack.Push(DispatchFunction(nextToken, left));
					}
					else
					{
						string right = resultStack.Pop();
						string left = resultStack.Pop();

						resultStack.Push(DispatchOperation(GetTokenType(nextToken), left, right));
					}
				}
				else
				{
					resultStack.Push(nextToken);
				}
			}

			result = resultStack.Pop();

			return result;
		}

		/// <summary>
		/// Converts a radian value to degrees.
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		private static string RadiansToDegrees(string angle)
		{
			return (double.Parse(angle) * (180.0 / Math.PI)).ToString();
		}

		/// <summary>
		/// Converts a degree value to radians.
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		private static string DegreesToRadians(string angle)
		{
			return (Math.PI * double.Parse(angle) / 180).ToString();
		}

		/// <summary>
		/// Returns the result of a function.
		/// </summary>
		/// <param name="nextToken">string</param>
		/// <param name="value">string</param>
		/// <returns></returns>
		private static string DispatchFunction(string nextToken, string value)
		{
			string result = value;
			bool resultWillBeHexadecimal = false;

			if ((new Regex(TokenPatterns.NegativeHexadecimalNumberKey)).IsMatch(value))
			{
				resultWillBeHexadecimal = true;
				value = long.Parse(value.Substring(0, value.Length - 1), NumberStyles.HexNumber).ToString();
			}

			if (nextToken.Equals("!"))
			{
				long res = long.Parse(value);
				long index = res;

				while (index > 1)
				{
					index--;

					res *= index;
				}

				result = res.ToString();
			}
			if (nextToken.Equals("sin"))
			{
				if (Calculator.Heap["mode"].Equals("deg"))
				{
					value = DegreesToRadians(value);
				}

				result = Math.Sin(double.Parse(value)).ToString();
			}
			else if (nextToken.Equals("cos"))
			{
				if (Calculator.Heap["mode"].Equals("deg"))
				{
					value = DegreesToRadians(value);
				}

				result = Math.Cos(double.Parse(value)).ToString();
			}
			else if (nextToken.Equals("tan"))
			{
				if (Calculator.Heap["mode"].Equals("deg"))
				{
					value = DegreesToRadians(value);
				}

				result = Math.Tan(double.Parse(value)).ToString();
			}
			else if (nextToken.Equals("asin"))
			{
				result = Math.Asin(double.Parse(value)).ToString();

				if (Calculator.Heap["mode"].Equals("deg"))
				{
					result = RadiansToDegrees(result);
				}
			}
			else if (nextToken.Equals("acos"))
			{
				result = Math.Acos(double.Parse(value)).ToString();

				if (Calculator.Heap["mode"].Equals("deg"))
				{
					result = RadiansToDegrees(result);
				}
			}
			else if (nextToken.Equals("atan"))
			{
				result = Math.Atan(double.Parse(value)).ToString();

				if (Calculator.Heap["mode"].Equals("deg"))
				{
					result = RadiansToDegrees(result);
				}
			}
			else if (nextToken.Equals("sinh"))
			{
				if (Calculator.Heap["mode"].Equals("deg"))
				{
					value = DegreesToRadians(value);
				}

				result = Math.Sinh(double.Parse(value)).ToString();
			}
			else if (nextToken.Equals("cosh"))
			{
				if (Calculator.Heap["mode"].Equals("deg"))
				{
					value = DegreesToRadians(value);
				}

				result = Math.Cosh(double.Parse(value)).ToString();
			}
			else if (nextToken.Equals("tanh"))
			{
				if (Calculator.Heap["mode"].Equals("deg"))
				{
					value = DegreesToRadians(value);
				}

				result = Math.Tanh(double.Parse(value)).ToString();
			}
			else if (nextToken.Equals("sqrt"))
			{
				result = Math.Sqrt(double.Parse(value)).ToString();
			}
			else if (nextToken.Equals("tohx"))
			{
				if (!resultWillBeHexadecimal)
				{
					resultWillBeHexadecimal = true;
				}
			}
			else if (nextToken.Equals("todc"))
			{
				if (resultWillBeHexadecimal)
				{
					result = value;
					resultWillBeHexadecimal = false;
				}
			}

			if (resultWillBeHexadecimal)
			{
				result = String.Concat(long.Parse(result).ToString("x"), "h");
			}

			return result;
		}

		/// <summary>
		/// Transforms the infix formatted expression into a postfix formatted expression.
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns>Stack&lt;string&gt;</returns>
		private static Stack<string> PostfixateExpression(string[] tokens)
		{
			PrecedenceMap precedenceMap = new PrecedenceMap();
			Stack<string> postfixatedExpression = new Stack<string>();
			Stack<string> operatorStack = new Stack<string>();
			TokenType lastTokenType = TokenType.Null;
			bool distributeNegation = false;

			foreach (string nextToken in tokens)
			{
				string currentToken = nextToken;

				if (GetTokenType(currentToken) == TokenType.DecimalNumber || GetTokenType(currentToken) == TokenType.HexadecimalNumber)
				{
					if (lastTokenType == TokenType.Negation)
					{
						if (!distributeNegation)
						{
							currentToken = String.Concat("-", currentToken);
						}
					}
					else if (distributeNegation)
					{
						currentToken = String.Concat("-", currentToken);
					}

					postfixatedExpression.Push(currentToken);

					lastTokenType = GetTokenType(currentToken);
				}
				else
				{
					TokenType nextTokenType = GetTokenType(currentToken);
					TokenType nextOperator = (operatorStack.Count > 0) ? GetTokenType(operatorStack.Peek()) : TokenType.Null;

					if (GetTokenType(currentToken) == TokenType.Subtract && lastTokenType != TokenType.DecimalNumber && lastTokenType != TokenType.HexadecimalNumber && lastTokenType != TokenType.Factorial)
					{
						lastTokenType = TokenType.Negation;
					}
					else
					{
						if (GetTokenType(currentToken) == TokenType.Function)
						{
							operatorStack.Push(currentToken);
						}
						else if (GetTokenType(currentToken) == TokenType.Factorial)
						{
							operatorStack.Push(currentToken);
						}
						else if (GetTokenType(currentToken) == TokenType.OpeningParenthesis)
						{
							if (lastTokenType == TokenType.Negation)
							{
								distributeNegation = true;
							}

							operatorStack.Push(currentToken);
						}
						else if (GetTokenType(currentToken) == TokenType.ClosingParenthesis)
						{
							if (distributeNegation)
							{
								distributeNegation = false;
							}

							while (GetTokenType(operatorStack.Peek()) != TokenType.OpeningParenthesis)
							{
								postfixatedExpression.Push(operatorStack.Pop());
							}

							if (operatorStack.Count > 0 && GetTokenType(operatorStack.Peek()) == TokenType.OpeningParenthesis)
							{
								operatorStack.Pop();
							}

							if (operatorStack.Count > 0 && GetTokenType(operatorStack.Peek()) == TokenType.Function)
							{
								postfixatedExpression.Push(operatorStack.Pop());
							}
						}
						else if (operatorStack.Count == 0)
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

						lastTokenType = GetTokenType(currentToken);
					}
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

		/// <summary>
		/// Dispatches the operation given for the pair of numbers given.
		/// </summary>
		/// <param name="nextOperator"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns>string</returns>
		private static string DispatchOperation(TokenType nextOperator, string left, string right)
		{
			string result = left;
			bool resultWillBeHexadecimal = false;

			if ((new Regex(TokenPatterns.NegativeHexadecimalNumberKey)).IsMatch(left))
			{
				resultWillBeHexadecimal = true;
				left = long.Parse(left.Substring(0, left.Length - 1), NumberStyles.HexNumber).ToString();
			}

			if ((new Regex(TokenPatterns.NegativeHexadecimalNumberKey)).IsMatch(right))
			{
				resultWillBeHexadecimal = true;
				right = long.Parse(right.Substring(0, right.Length - 1), NumberStyles.HexNumber).ToString();
			}

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
			else if (nextOperator == TokenType.Modulus)
			{
				result = (double.Parse(left) % double.Parse(right)).ToString();
			}
			else if (nextOperator == TokenType.Div)
			{
				long rem = 0L;
				long res = Math.DivRem(long.Parse(left), long.Parse(right), out rem);

				result = res.ToString();
			}
			else if (nextOperator == TokenType.Divide)
			{
				result = (double.Parse(left) / double.Parse(right)).ToString();
			}

			if (resultWillBeHexadecimal)
			{
				result = String.Concat(long.Parse(result).ToString("x"), "h");
			}

			return result;
		}

		/// <summary>
		/// Gets the type of a token.
		/// </summary>
		/// <param name="token"></param>
		/// <returns>TokenType</returns>
		private static TokenType GetTokenType(string token)
		{
			TokenType type = TokenType.DecimalNumber;

			if ((new Regex(TokenPatterns.NegativeDecimalNumberKey)).IsMatch(token))
			{
				type = TokenType.DecimalNumber;
			}
			else if ((new Regex(TokenPatterns.HexadecimalNumberKey)).IsMatch(token))
			{
				type = TokenType.HexadecimalNumber;
			}
			else if ((new Regex(TokenPatterns.NegativeHexadecimalNumberKey)).IsMatch(token))
			{
				type = TokenType.HexadecimalNumber;
			}
			else if ((new Regex(TokenPatterns.AdditionKey)).IsMatch(token))
			{
				type = TokenType.Add;
			}
			else if ((new Regex(TokenPatterns.SubtractionKey)).IsMatch(token))
			{
				type = TokenType.Subtract;
			}
			else if ((new Regex(TokenPatterns.FactorialKey)).IsMatch(token))
			{
				type = TokenType.Factorial;
			}
			else if ((new Regex(TokenPatterns.PowerKey)).IsMatch(token))
			{
				type = TokenType.Power;
			}
			else if ((new Regex(TokenPatterns.MultiplicationKey)).IsMatch(token))
			{
				type = TokenType.Multiply;
			}
			else if ((new Regex(TokenPatterns.ModulusKey)).IsMatch(token))
			{
				type = TokenType.Modulus;
			}
			else if ((new Regex(TokenPatterns.DivKey)).IsMatch(token))
			{
				type = TokenType.Div;
			}
			else if ((new Regex(TokenPatterns.DivisionKey)).IsMatch(token))
			{
				type = TokenType.Divide;
			}
			else if ((new Regex(TokenPatterns.LeftParenthesisKey)).IsMatch(token))
			{
				type = TokenType.OpeningParenthesis;
			}
			else if ((new Regex(TokenPatterns.RightParenthesisKey)).IsMatch(token))
			{
				type = TokenType.ClosingParenthesis;
			}
			else if ((new Regex(TokenPatterns.FunctionKey)).IsMatch(token))
			{
				type = TokenType.Function;
			}

			return type;
		}

		/// <summary>
		/// Returns a string representation of ExpressionEvaluator.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return _value;
		}

		#endregion
	}
}

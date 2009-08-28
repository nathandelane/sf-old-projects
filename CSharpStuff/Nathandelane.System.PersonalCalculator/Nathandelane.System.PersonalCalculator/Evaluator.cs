using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

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
					if (type == TokenType.Modulus || type == TokenType.Power || type == TokenType.And || type == TokenType.Or || type == TokenType.Xor || type == TokenType.Addition || type == TokenType.Division || type == TokenType.Multiplication || type == TokenType.Negation || type == TokenType.Subtraction)
					{
						expression = PerformOperation(expression, type, subExp);
					}
					else if (type == TokenType.ConversionFunction || type == TokenType.TrigFunction)
					{
						if (type == TokenType.ConversionFunction)
						{
							expression = PerformConversion(expression, subExp);
						}
						else if (type == TokenType.TrigFunction)
						{
							expression = PerformTrigOperation(expression, subExp);
						}
					}
					else if (type == TokenType.SubExpression)
					{
						expression = EvaluateSubExpression(expression, subExp);
					}
					else if (type == TokenType.Assignment || type == TokenType.Variable)
					{
						if (type == TokenType.Assignment)
						{
							string[] parts = expression.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
							expression = Evaluate(expression)._result;
							Calculator.State[parts[0]] = expression;
						}
						else if (type == TokenType.Variable)
						{
							expression = Calculator.State[expression];
						}
					}
					else if (type == TokenType.Undefined)
					{
						break;
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

		private static OutputType SetOutputType(string expression)
		{
			OutputType outputType = OutputType.Decimal;
			if (TokenMatcher.ExtractionTable[TokenType.BinaryNumber].IsMatch(expression))
			{
				outputType = OutputType.Binary;
			}
			else if (TokenMatcher.ExtractionTable[TokenType.OctalNumber].IsMatch(expression))
			{
				outputType = OutputType.Octal;
			}
			else if (TokenMatcher.ExtractionTable[TokenType.HexNumber].IsMatch(expression))
			{
				outputType = OutputType.Hexadecimal;
			}

			return outputType;
		}

		private static double GetDoubleValue(string expression)
		{
			double value = 0.0d;
			switch (SetOutputType(expression))
			{
				case OutputType.Binary:
					value = Convert.ToInt32(expression, 2);
					break;
				case OutputType.Decimal:
					value = double.Parse(expression);
					break;
				case OutputType.Hexadecimal:
					value = Convert.ToInt32(expression, 16);
					break;
				case OutputType.Octal:
					value = Convert.ToInt32(expression, 8);
					break;
			}

			return value;
		}

		private static int GetIntegerValue(string expression)
		{
			int value = 0;
			switch (SetOutputType(expression))
			{
				case OutputType.Binary:
					value = Convert.ToInt32(expression, 2);
					break;
				case OutputType.Decimal:
					value = int.Parse(expression);
					break;
				case OutputType.Hexadecimal:
					value = Convert.ToInt32(expression, 16);
					break;
				case OutputType.Octal:
					value = Convert.ToInt32(expression, 8);
					break;
			}

			return value;
		}

		private static string GetNumber(string subExp, int index)
		{
			Regex regex = TokenMatcher.ExtractionTable[TokenType.Number];
			MatchCollection mc = null;
			string value = String.Empty;
			if ((mc = regex.Matches(subExp)).Count >= index + 1)
			{
				value = mc[index].Value;
			}

			string dec = regex.Matches(subExp)[index].Value;

			regex = TokenMatcher.ExtractionTable[TokenType.HexNumber];
			if (regex.IsMatch(subExp))
			{
				if ((mc = regex.Matches(subExp)).Count >= index + 1)
				{
					value = mc[index].Value;
					if (value.Substring(0, dec.Length).Equals(dec))
					{
						return value;
					}
				}
			}

			regex = TokenMatcher.ExtractionTable[TokenType.OctalNumber];
			if (regex.IsMatch(subExp))
			{
				if ((mc = regex.Matches(subExp)).Count >= index + 1)
				{
					value = mc[index].Value;
					if (value.Substring(0, dec.Length).Equals(dec))
					{
						return value;
					}
				}
			}

			regex = TokenMatcher.ExtractionTable[TokenType.BinaryNumber];
			if (regex.IsMatch(subExp))
			{
				if ((mc = regex.Matches(subExp)).Count >= index + 1)
				{
					value = mc[index].Value;
					if (value.Substring(0, dec.Length).Equals(dec))
					{
						return value;
					}
				}
			}

			return dec;
		}

		private static string ConvertToDecimal(OutputType outputType, string value)
		{
			switch (outputType)
			{
				case OutputType.Binary:
					value = value.EndsWith("b") ? value.Substring(0, value.Length - 1) : value;
					value = Convert.ToInt32(value, 2).ToString();
					break;
				case OutputType.Hexadecimal:
					value = value.EndsWith("h") ? value.Substring(0, value.Length - 1) : value;
					value = Convert.ToInt32(value, 16).ToString();
					break;
				case OutputType.Octal:
					value = value.EndsWith("o") ? value.Substring(0, value.Length - 1) : value;
					value = Convert.ToInt32(value, 8).ToString();
					break;
				case OutputType.Decimal:
					value = value.EndsWith("d") ? value.Substring(0, value.Length - 1) : value;
					break;
			}

			return value;
		}

		private static string PerformTrigOperation(string expression, string subExp)
		{
			string fixedSubExp = subExp.Substring(4, subExp.Length - 5);
			Evaluator innerEvaluation = Evaluate(fixedSubExp);
			string result = ConvertToDecimal(SetOutputType(innerEvaluation._result), innerEvaluation._result);

			if (subExp.StartsWith("sin"))
			{
				expression = Math.Sin(double.Parse(result)).ToString();
			}
			else if (subExp.StartsWith("cos"))
			{
				expression = Math.Cos(double.Parse(result)).ToString();
			}
			else if (subExp.StartsWith("tan"))
			{
				expression = Math.Tan(double.Parse(result)).ToString();
			}

			return expression;
		}

		private static string PerformConversion(string expression, string subExp)
		{
			string fixedSubExp = subExp.Substring(2, subExp.Length - 3);
			Evaluator innerEvaluation = Evaluate(fixedSubExp);
			string result = ConvertToDecimal(SetOutputType(innerEvaluation._result), innerEvaluation._result);

			if(subExp.StartsWith("deg"))
			{
				expression = (double.Parse(result) * (180 / Math.PI)).ToString();
			}
			else if(subExp.StartsWith("rad"))
			{
				expression = (double.Parse(result) * (Math.PI / 180)).ToString();
			}
			else if (subExp.StartsWith("b"))
			{
				expression = String.Concat(Convert.ToString(int.Parse(result), 2), "b");
			}
			else if (subExp.StartsWith("o"))
			{
				expression = String.Concat(Convert.ToString(int.Parse(result), 8), "o");
			}
			else if (subExp.StartsWith("h"))
			{
				expression = String.Concat(Convert.ToString(int.Parse(result), 16).ToUpper(), "h");
			}
			else if (subExp.StartsWith("d"))
			{
				expression = String.Concat(result, "d");
			}

			return expression;
		}

		private static string PerformOperation(string expression, TokenType type, string subExp)
		{
			OutputType outputType = OutputType.Decimal;
			Regex regex = TokenMatcher.ExtractionTable[TokenType.Number];
			if (regex.Matches(subExp).Count == 2)
			{
				string left = String.IsNullOrEmpty(GetNumber(subExp, 0)) ? "0" : GetNumber(subExp, 0);
				outputType = SetOutputType(left);
				left = ConvertToDecimal(outputType, left);

				string right = String.IsNullOrEmpty(GetNumber(subExp, 1)) ? "0" : GetNumber(subExp, 1);
				outputType = SetOutputType(right);
				right = ConvertToDecimal(outputType, right);

				double result = 0.0d;
				string strResult = String.Format("{0}", result);
				switch (type)
				{
					case TokenType.Addition:
						result = double.Parse(left) + double.Parse(right);
						break;
					case TokenType.Subtraction:
						result = GetDoubleValue(left) - GetDoubleValue(right);
						break;
					case TokenType.Division:
						result = GetDoubleValue(left) / GetDoubleValue(right);
						break;
					case TokenType.Multiplication:
						result = GetDoubleValue(left) * GetDoubleValue(right);
						break;
					case TokenType.And:
						result = GetIntegerValue(left) & GetIntegerValue(right);
						break;
					case TokenType.Or:
						result = GetIntegerValue(left) | GetIntegerValue(right);
						break;
					case TokenType.Xor:
						result = GetIntegerValue(left) ^ GetIntegerValue(right);
						break;
					case TokenType.Modulus:
						result = GetDoubleValue(left) % GetDoubleValue(right);
						break;
					case TokenType.Power:
						result = Math.Pow(GetDoubleValue(left), GetDoubleValue(right));
						break;
				}

				switch (outputType)
				{
					case OutputType.Binary:
						strResult = String.Concat(Convert.ToString((int)result, 2), "b");
						break;
					case OutputType.Decimal:
						strResult = result.ToString();
						break;
					case OutputType.Hexadecimal:
						strResult = String.Concat(Convert.ToString((int)result, 16), "h");
						break;
					case OutputType.Octal:
						strResult = String.Concat(Convert.ToString((int)result, 8), "o");
						break;
				}

				int index = expression.IndexOf(subExp);
				int length = subExp.Length;

				if (index > 0)
				{
					string subStrLeft = expression.Substring(0, index);
					string subStrRight = expression.Substring(index + subExp.Length);
					expression = String.Concat(subStrLeft, strResult, subStrRight);
				}
				else
				{
					expression = expression.Remove(index, length);
					expression = String.Concat(strResult, expression);
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

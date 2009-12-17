﻿/*
Nathan Lane, Nathandelane Copyright (C) 2009, Nathandelane.

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

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class FunctionExpression : Expression
	{
		#region Constructors

		public FunctionExpression(Token function, Expression operand)
			: base(ExpressionPrecedence.Function, function, new List<Expression>() { operand })
		{
		}

		public FunctionExpression(Token function, Expression left, Expression right)
			: base(ExpressionPrecedence.Function, function, new List<Expression>() { left, right })
		{
		}

		public FunctionExpression(Token function, Expression first, Expression second, Expression third)
			: base(ExpressionPrecedence.Function, function, new List<Expression>() { first, second, third })
		{
		}

		public FunctionExpression(Token function, IList<Expression> operands)
			: base(ExpressionPrecedence.Function, function, operands)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Evaluates this expression.
		/// </summary>
		/// <returns></returns>
		public override Token Evaluate()
		{
			Token result = new NullToken();
			string op = Operation.ToString();
			bool modeIsDegrees = CalculatorContext.GetInstance()[CalculatorContext.Mode].ToString().Equals("deg", StringComparison.InvariantCultureIgnoreCase);

			if (op.Equals("cosh", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				double res = modeIsDegrees ? ToRadians(double.Parse(val.ToString())) : double.Parse(val.ToString());
				res = Math.Cosh(res);

				result = new NumberToken(res.ToString());
			}
			else if (op.Equals("sinh", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				double res = modeIsDegrees ? ToRadians(double.Parse(val.ToString())) : double.Parse(val.ToString());
				res = Math.Sinh(res);

				result = new NumberToken(res.ToString());
			}
			else if (op.Equals("tanh", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				double res = modeIsDegrees ? ToRadians(double.Parse(val.ToString())) : double.Parse(val.ToString());
				res = Math.Tanh(res);

				result = new NumberToken(res.ToString());
			}
			else if (op.Equals("cos", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				double res = modeIsDegrees ? ToRadians(double.Parse(val.ToString())) : double.Parse(val.ToString());
				res = Math.Cos(res);

				result = new NumberToken(res.ToString());
			}
			else if (op.Equals("sin", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				double res = modeIsDegrees ? ToRadians(double.Parse(val.ToString())) : double.Parse(val.ToString());
				res = Math.Sin(res);

				result = new NumberToken(res.ToString());
			}
			else if (op.Equals("tan", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				double res = modeIsDegrees ? ToRadians(double.Parse(val.ToString())) : double.Parse(val.ToString());
				res = Math.Tan(res);

				result = new NumberToken(res.ToString());
			}
			else if (op.Equals("acos", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				double res = double.Parse(val.ToString());
				res = modeIsDegrees ? ToDegrees(Math.Acos(res)) : Math.Acos(res);

				result = new NumberToken(res.ToString());
			}
			else if (op.Equals("asin", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				double res = double.Parse(val.ToString());
				res = modeIsDegrees ? ToDegrees(Math.Asin(res)) : Math.Asin(res);

				result = new NumberToken(res.ToString());
			}
			else if (op.Equals("atan", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				double res = double.Parse(val.ToString());
				res = modeIsDegrees ? ToDegrees(Math.Atan(res)) : Math.Atan(res);

				result = new NumberToken(res.ToString());
			}
			else if (op.Equals("log", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);

				result = new NumberToken((Math.Log10(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("ln", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);

				result = new NumberToken((Math.Log(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("lb", StringComparison.InvariantCultureIgnoreCase) || op.Equals("ld", StringComparison.InvariantCultureIgnoreCase) || op.Equals("lg", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);

				result = new NumberToken((Math.Log(double.Parse(val.ToString())) / Math.Log(2.0)).ToString());
			}
			else if (op.Equals("deg", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);

				result = new NumberToken((ToDegrees(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("rad", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);

				result = new NumberToken((ToRadians(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("sqrt", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);

				result = new NumberToken((Math.Sqrt(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("too", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("8");

				result = new NumberToken(val);
			}
			else if (op.Equals("tod", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("10");

				result = new NumberToken(val);
			}
			else if (op.Equals("toh", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("16");

				result = new NumberToken(val);
			}
			else if (op.Equals("tob", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);
				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("2");

				result = new NumberToken(val);
			}
			else if (op.Equals("**", StringComparison.InvariantCultureIgnoreCase))
			{
				Token left = EvaluateOperand(1);
				Token right = EvaluateOperand(0);

				result = new NumberToken((Math.Pow(double.Parse(left.ToString()), double.Parse(right.ToString()))).ToString());
			}
			else if (op.Equals("//", StringComparison.InvariantCultureIgnoreCase))
			{
				Token left = EvaluateOperand(1);
				Token right = EvaluateOperand(0);

				if (left is NumberToken && right is NumberToken)
				{
					long lLeft = 0;
					long lRight = 0;
					long rem = 0;

					if (long.TryParse(((NumberToken)left).WholePart(), out lLeft) && long.TryParse(((NumberToken)right).WholePart(), out lRight))
					{
						long whole = Math.DivRem(lLeft, lRight, out rem);

						result = new NumberToken(whole.ToString());
					}
					else
					{
						throw new FormatException(String.Format("Could not parse long value from {0} for function expression.", left.ToString()));
					}
				}
				else
				{
					throw new MalformedExpressionException("Exception in //.");
				}
			}
			else if (op.Equals("%", StringComparison.InvariantCultureIgnoreCase))
			{
				Token left = EvaluateOperand(1);
				Token right = EvaluateOperand(0);

				result = new NumberToken((double.Parse(left.ToString()) % double.Parse(right.ToString())).ToString());
			}
			else if (op.Equals("!", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);

				if (Operation is PostfixFunctionToken)
				{
					double counter = double.Parse(val.ToString());
					double total = counter;

					while (counter > 1)
					{
						counter--;

						total *= counter;
					}

					result = new NumberToken(total.ToString());
				}
				else if (Operation is PrefixFunctionToken)
				{
					if (val is BooleanToken)
					{
						result = ((BooleanToken)val).Not();
					}
					else if (val is VariableToken)
					{

					}
					else if (val is NumberToken)
					{
						result = ((NumberToken)val).Negate();
					}
				}
			}
			else if (op.Equals("-", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = EvaluateOperand(0);

				result = new NumberToken((double.Parse(val.ToString()) * (-1)).ToString());
			}

			return result;
		}

		/// <summary>
		/// Converts a degree value to radians.
		/// </summary>
		/// <param name="deg"></param>
		/// <returns></returns>
		private double ToRadians(double deg)
		{
			return (Math.PI * deg) / 180.0;
		}

		/// <summary>
		/// Converts a radian value to degrees.
		/// </summary>
		/// <param name="rad"></param>
		/// <returns></returns>
		private double ToDegrees(double rad)
		{
			return (180.0 / Math.PI) * rad;
		}

		#endregion
	}
}

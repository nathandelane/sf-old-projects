/*
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

namespace Nathandelane.System.Bpc
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

			if (Operation is PrefixFunctionToken)
			{
				Token val = EvaluateOperand(0);
				double res = 0.0;

				if (double.TryParse(val.ToString(), out res))
				{
					if (op.Equals("abs", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((Math.Abs(res)).ToString());
					}
					else if (op.Equals("cosh", StringComparison.InvariantCultureIgnoreCase))
					{
						if (modeIsDegrees)
						{
							res = ToRadians(res);
						}

						res = Math.Cosh(res);

						result = new NumberToken(res.ToString());
					}
					else if (op.Equals("sinh", StringComparison.InvariantCultureIgnoreCase))
					{
						if (modeIsDegrees)
						{
							res = ToRadians(res);
						}

						res = Math.Sinh(res);

						result = new NumberToken(res.ToString());
					}
					else if (op.Equals("tanh", StringComparison.InvariantCultureIgnoreCase))
					{
						if (modeIsDegrees)
						{
							res = ToRadians(res);
						}

						res = Math.Tanh(res);

						result = new NumberToken(res.ToString());
					}
					else if (op.Equals("cos", StringComparison.InvariantCultureIgnoreCase))
					{
						if (modeIsDegrees)
						{
							res = ToRadians(res);
						}

						res = Math.Cos(res);

						result = new NumberToken(res.ToString());
					}
					else if (op.Equals("sin", StringComparison.InvariantCultureIgnoreCase))
					{
						if (modeIsDegrees)
						{
							res = ToRadians(res);
						}

						res = Math.Sin(res);

						result = new NumberToken(res.ToString());
					}
					else if (op.Equals("tan", StringComparison.InvariantCultureIgnoreCase))
					{
						if (modeIsDegrees)
						{
							res = ToRadians(res);
						}

						res = Math.Tan(res);

						result = new NumberToken(res.ToString());
					}
					else if (op.Equals("acos", StringComparison.InvariantCultureIgnoreCase))
					{
						res = modeIsDegrees ? ToDegrees(Math.Acos(res)) : Math.Acos(res);

						result = new NumberToken(res.ToString());
					}
					else if (op.Equals("asin", StringComparison.InvariantCultureIgnoreCase))
					{
						res = modeIsDegrees ? ToDegrees(Math.Asin(res)) : Math.Acos(res);

						result = new NumberToken(res.ToString());
					}
					else if (op.Equals("atan", StringComparison.InvariantCultureIgnoreCase))
					{
						res = modeIsDegrees ? ToDegrees(Math.Atan(res)) : Math.Acos(res);

						result = new NumberToken(res.ToString());
					}
					else if (op.Equals("log", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((Math.Log10(res)).ToString());
					}
					else if (op.Equals("ln", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((Math.Log(res)).ToString());
					}
					else if (op.Equals("lb", StringComparison.InvariantCultureIgnoreCase) || op.Equals("ld", StringComparison.InvariantCultureIgnoreCase) || op.Equals("lg", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((Math.Log(res) / Math.Log(2.0)).ToString());
					}
					else if (op.Equals("deg", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((ToDegrees(res)).ToString());
					}
					else if (op.Equals("rad", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((ToRadians(res)).ToString());
					}
					else if (op.Equals("sqrt", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((Math.Sqrt(res)).ToString());
					}
					else if (op.Equals("too", StringComparison.InvariantCultureIgnoreCase))
					{
						CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("8");

						result = new NumberToken(val);
					}
					else if (op.Equals("tod", StringComparison.InvariantCultureIgnoreCase))
					{
						CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("10");

						result = new NumberToken(val);
					}
					else if (op.Equals("toh", StringComparison.InvariantCultureIgnoreCase))
					{
						CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("16");

						result = new NumberToken(val);
					}
					else if (op.Equals("tob", StringComparison.InvariantCultureIgnoreCase))
					{
						CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("2");

						result = new NumberToken(val);
					}
					else if (op.Equals("-", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((res * (-1)).ToString());
					}
					else if (op.Equals("!", StringComparison.InvariantCultureIgnoreCase))
					{
						if (val is BooleanToken)
						{
							result = ((BooleanToken)val).Not();
						}
						else if (val is NumberToken)
						{
							result = ((NumberToken)val).Negate();
						}
					}
				}
				else
				{
					throw new FormatException(String.Format("Could not parse double value from {0}", val));
				}
			}
			else if (Operation is InfixFunctionToken)
			{
				Token left = EvaluateOperand(1);
				Token right = EvaluateOperand(0);
				double dLeft = 0.0;
				double dRight = 0.0;

				if (double.TryParse(left.ToString(), out dLeft) && double.TryParse(right.ToString(), out dRight))
				{
					if (op.Equals("**", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((Math.Pow(dLeft, dRight)).ToString());
					}
					else if (op.Equals("%", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((dLeft % dRight).ToString());
					}
					else if (op.Equals("//", StringComparison.InvariantCultureIgnoreCase))
					{
						if ((left is NumberToken && right is NumberToken))
						{
							long lLeft = 0;
							long lRight = 0;

							if ((long.TryParse(((NumberToken)left).WholePart(), out lLeft) && long.TryParse(((NumberToken)right).WholePart(), out lRight)))
							{
								long rem = 0;
								long whole = Math.DivRem(lLeft, lRight, out rem);

								result = new NumberToken(whole.ToString());
							}
						}
						else
						{
							throw new FormatException(String.Format("Could not parse long values from {0} or {1}.", left, right));
						}
					}
				}
				else
				{
					throw new FormatException(String.Format("Could not parse double values from {0} or {1}.", left, right));
				}
			}
			else if (Operation is PostfixFunctionToken)
			{
				Token val = EvaluateOperand(0);
				double res = 0.0;

				if (double.TryParse(val.ToString(), out res))
				{
					if (op.Equals("!", StringComparison.InvariantCultureIgnoreCase))
					{
						if (Operation is PostfixFunctionToken)
						{
							double counter = res;
							double total = counter;

							while (counter > 1)
							{
								counter--;

								total *= counter;
							}

							result = new NumberToken(total.ToString());
						}
					}
				}
				else
				{
					throw new FormatException(String.Format("Could not parse double value from {0}", val));
				}
			}
			else
			{
				throw new InvalidTokenException(String.Format("Operation {0} not currently supported. Please submit a feature request.", Operation));
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

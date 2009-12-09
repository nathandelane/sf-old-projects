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

			if (op.Equals("cos", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();

				result = new NumberToken((Math.Cos(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("sin", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();

				result = new NumberToken((Math.Sin(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("tan", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();

				result = new NumberToken((Math.Tan(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("acos", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();

				result = new NumberToken((Math.Acos(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("asin", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();

				result = new NumberToken((Math.Asin(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("atan", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();

				result = new NumberToken((Math.Atan(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("sqrt", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();

				result = new NumberToken((Math.Sqrt(double.Parse(val.ToString()))).ToString());
			}
			else if (op.Equals("too", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();
				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("8");

				result = new NumberToken(val);
			}
			else if (op.Equals("tod", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();
				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("10");

				result = new NumberToken(val);
			}
			else if (op.Equals("toh", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();
				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("16");

				result = new NumberToken(val);
			}
			else if (op.Equals("tob", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();
				CalculatorContext.GetInstance()[CalculatorContext.DisplayBase] = new NumberToken("2");

				result = new NumberToken(val);
			}
			else if (op.Equals("**", StringComparison.InvariantCultureIgnoreCase))
			{
				Token left = Operands[1].Evaluate();
				Token right = Operands[0].Evaluate();

				result = new NumberToken((Math.Pow(double.Parse(left.ToString()), double.Parse(right.ToString()))).ToString());
			}
			else if (op.Equals("//", StringComparison.InvariantCultureIgnoreCase))
			{
				Token left = Operands[1].Evaluate();
				Token right = Operands[0].Evaluate();

				if (left is NumberToken && right is NumberToken)
				{
					long lLeft = long.Parse(((NumberToken)left).WholePart());
					long lRight = long.Parse(((NumberToken)right).WholePart()); ;
					long rem = 0;
					long whole = Math.DivRem(lLeft, lRight, out rem);

					result = new NumberToken(whole.ToString());
				}
				else
				{
					throw new MalformedExpressionException("Exception in //.");
				}
			}
			else if (op.Equals("%", StringComparison.InvariantCultureIgnoreCase))
			{
				Token left = Operands[1].Evaluate();
				Token right = Operands[0].Evaluate();

				result = new NumberToken((double.Parse(left.ToString()) % double.Parse(right.ToString())).ToString());
			}
			else if (op.Equals("!", StringComparison.InvariantCultureIgnoreCase))
			{
				Token val = Operands[0].Evaluate();
				double counter = double.Parse(val.ToString());
				double total = counter;

				while (counter > 1)
				{
					counter--;

					total *= counter;
				}

				result = new NumberToken(total.ToString());
			}

			return result;
		}

		#endregion
	}
}

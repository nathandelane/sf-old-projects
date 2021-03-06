﻿/*
Nathan Lane, Nathandelane Copyright (C) 2010, Nathandelane.

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
	public class ArithmeticExpression : Expression
	{
		#region Constructors

		/// <summary>
		/// Creates an instance of ArithmeticExpression.
		/// </summary>
		/// <param name="operation"></param>
		/// <param name="left"></param>
		/// <param name="right"></param>
		public ArithmeticExpression(Token operation, Expression left, Expression right)
			: base(DeterminePrecedence(operation), operation, new List<Expression>() { left, right })
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Determine the actual operation precedence for this operation.
		/// </summary>
		/// <param name="operation"></param>
		/// <returns></returns>
		private static ExpressionPrecedence DeterminePrecedence(Token operation)
		{
			ExpressionPrecedence precedence = ExpressionPrecedence.Null;

			if (operation.ToString().Equals("+", StringComparison.InvariantCultureIgnoreCase))
			{
				precedence = ExpressionPrecedence.Add;
			}
			else if (operation.ToString().Equals("-", StringComparison.InvariantCultureIgnoreCase))
			{
				precedence = ExpressionPrecedence.Subtract;
			}
			else if (operation.ToString().Equals("*", StringComparison.InvariantCultureIgnoreCase))
			{
				precedence = ExpressionPrecedence.MultiplyOrDivide;
			}
			else if (operation.ToString().Equals("/", StringComparison.InvariantCultureIgnoreCase))
			{
				precedence = ExpressionPrecedence.MultiplyOrDivide;
			}

			return precedence;
		}

		/// <summary>
		/// Evaluates this expression.
		/// </summary>
		/// <returns></returns>
		public override Token Evaluate()
		{
			Token result = new NullToken();
			Token left = EvaluateOperand(1);
			Token right = EvaluateOperand(0);
			double dLeft = 0.0;
			double dRight = 0.0;

			if (double.TryParse(left.ToString(), out dLeft) && double.TryParse(right.ToString(), out dRight))
			{
				switch (Precedence)
				{
					case ExpressionPrecedence.Add:
						result = new NumberToken((dLeft + dRight).ToString());
						break;
					case ExpressionPrecedence.Subtract:
						result = new NumberToken((dLeft - dRight).ToString());
						break;
					case ExpressionPrecedence.MultiplyOrDivide:
						if (Operation.ToString().Equals("/", StringComparison.InvariantCultureIgnoreCase))
						{
							if (right.ToString().Equals("0", StringComparison.InvariantCultureIgnoreCase))
							{
								throw new DivideByZeroException();
							}
							else
							{
								result = new NumberToken((dLeft / dRight).ToString());
							}
						}
						else if (Operation.ToString().Equals("*"))
						{
							result = new NumberToken((dLeft * dRight).ToString());
						}
						break;
				}
			}
			else
			{
				throw new FormatException(String.Format("Could not parse double value from {0} for arithmetic expression.", left.ToString()));
			}

			return result;
		}

		#endregion
	}
}

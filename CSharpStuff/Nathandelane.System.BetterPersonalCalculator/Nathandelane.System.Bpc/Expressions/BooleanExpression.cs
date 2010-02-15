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
	public class BooleanExpression : Expression
	{
		#region Constructors

		public BooleanExpression(Token operation, Expression left, Expression right)
			: base(ExpressionPrecedence.Boolean, operation, new List<Expression>() { left, right })
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
			Token left = EvaluateOperand(1);
			Token right = EvaluateOperand(0);

			if (left is NumberToken && right is NumberToken)
			{
				double dLeft = 0.0;
				double dRight = 0.0;

				if (double.TryParse(left.ToString(), out dLeft) && double.TryParse(right.ToString(), out dRight))
				{
					if (Operation.ToString().Equals("==", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((dLeft == dRight).ToString());
					}
					else if (Operation.ToString().Equals("<=", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((dLeft <= dRight).ToString());
					}
					else if (Operation.ToString().Equals(">=", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((dLeft >= dRight).ToString());
					}
					else if (Operation.ToString().Equals("<", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((dLeft < dRight).ToString());
					}
					else if (Operation.ToString().Equals(">", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((dLeft > dRight).ToString());
					}
					else if (Operation.ToString().Equals("!=", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((dLeft != dRight).ToString());
					}
					else
					{
						throw new MalformedExpressionException(String.Format("Unknown boolean operator {0}.", Operation));
					}
				}
				else
				{
					throw new FormatException(String.Format("Could not parse double value from {0} for boolean expression.", left.ToString()));
				}
			}
			else if (left is BooleanToken && right is BooleanToken)
			{
				bool bLeft = false;
				bool bRight = false;

				if (bool.TryParse(left.ToString(), out bLeft) && bool.TryParse(right.ToString(), out bRight))
				{
					if (Operation.ToString().Equals("==", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((bLeft == bRight).ToString());
					}
					else if (Operation.ToString().Equals("!=", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((bLeft != bRight).ToString());
					}
					else
					{
						throw new MalformedExpressionException("Booleans cannot be less than or greater than each other.");
					}
				}
				else
				{
					throw new FormatException(String.Format("Could not parse bool value from {0} for boolean expression.", left.ToString()));
				}
			}
			else
			{
				throw new MalformedExpressionException(String.Concat(ToString(), " is not a valid boolean expression."));
			}

			return result;
		}

		#endregion
	}
}

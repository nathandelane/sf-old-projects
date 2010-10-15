/*
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
	public class BinaryExpression : Expression
	{
		#region Constructors

		public BinaryExpression(Token operation, Expression left, Expression right)
			: base(ExpressionPrecedence.Binary, operation, new List<Expression>() { left, right })
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
				long lLeft = 0; ;
				long lRight = 0; ;

				if (long.TryParse(((NumberToken)left).WholePart(), out lLeft) && long.TryParse(((NumberToken)right).WholePart(), out lRight))
				{
					if (Operation.ToString().Equals("&", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((lLeft & lRight).ToString());
					}
					else if (Operation.ToString().Equals("|", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((lLeft | lRight).ToString());
					}
					else if (Operation.ToString().Equals("^", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new NumberToken((lLeft ^ lRight).ToString());
					}
					else
					{
						throw new FormatException(String.Format("Could not parse long value from {0} for binary expression.", left.ToString()));
					}
				}
			}
			else if (left is BooleanToken && right is BooleanToken)
			{
				bool bLeft = false;
				bool bRight = false;

				if (bool.TryParse(left.ToString(), out bLeft) && bool.TryParse(right.ToString(), out bRight))
				{
					if (Operation.ToString().Equals("&", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((bLeft & bRight).ToString());
					}
					else if (Operation.ToString().Equals("|", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((bLeft | bRight).ToString());
					}
					else if (Operation.ToString().Equals("^", StringComparison.InvariantCultureIgnoreCase))
					{
						result = new BooleanToken((bLeft ^ bRight).ToString());
					}
					else
					{
						throw new FormatException(String.Format("Could not parse bool value from {0} for binary expression.", left.ToString()));
					}
				}
			}
			else
			{
				throw new MalformedExpressionException(String.Concat(ToString(), " is not a valid binary expression."));
			}

			return result;
		}

		#endregion
	}
}

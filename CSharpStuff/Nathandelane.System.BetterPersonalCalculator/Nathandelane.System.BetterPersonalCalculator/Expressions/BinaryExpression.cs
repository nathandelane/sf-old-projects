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
			Token left = Operands[1].Evaluate();
			Token right = Operands[0].Evaluate();

			if (left is NumberToken && right is NumberToken)
			{
				long dLeft = long.Parse(((NumberToken)left).WholePart());
				long dRight = long.Parse(((NumberToken)right).WholePart());

				if (Operation.ToString().Equals("&", StringComparison.InvariantCultureIgnoreCase))
				{
					result = new NumberToken((dLeft & dRight).ToString());
				}
				else if (Operation.ToString().Equals("|", StringComparison.InvariantCultureIgnoreCase))
				{
					result = new NumberToken((dLeft | dRight).ToString());
				}
				else if (Operation.ToString().Equals("^", StringComparison.InvariantCultureIgnoreCase))
				{
					result = new NumberToken((dLeft ^ dRight).ToString());
				}
			}
			else if (left is BooleanToken && right is BooleanToken)
			{
				bool bLeft = Boolean.Parse(left.ToString());
				bool bRight = Boolean.Parse(right.ToString());

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

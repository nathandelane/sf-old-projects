using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class ArithmeticExpression : Expression
	{
		#region Constructors

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

			if (operation.ToString().Equals("+"))
			{
				precedence = ExpressionPrecedence.Add;
			}
			else if (operation.ToString().Equals("-"))
			{
				precedence = ExpressionPrecedence.Subtract;
			}
			else if (operation.ToString().Equals("*"))
			{
				precedence = ExpressionPrecedence.MultiplyOrDivide;
			}
			else if (operation.ToString().Equals("/"))
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
			Token left = Operands[1].Evaluate();
			Token right = Operands[0].Evaluate();

			switch (Precedence)
			{
				case ExpressionPrecedence.Add:
					result = new NumberToken((double.Parse(left.ToString()) + double.Parse(right.ToString())).ToString());
					break;
				case ExpressionPrecedence.Subtract:
					result = new NumberToken((double.Parse(left.ToString()) - double.Parse(right.ToString())).ToString());
					break;
				case ExpressionPrecedence.MultiplyOrDivide:
					if (Operation.ToString().Equals("/"))
					{
						if (right.ToString().Equals("0", StringComparison.InvariantCulture))
						{
							throw new DivideByZeroException();
						}
						else
						{
							result = new NumberToken((double.Parse(left.ToString()) / double.Parse(right.ToString())).ToString());
						}
					}
					else if (Operation.ToString().Equals("*"))
					{
						result = new NumberToken((double.Parse(left.ToString()) * double.Parse(right.ToString())).ToString());
					}
					break;
			}

			return result;
		}

		#endregion
	}
}

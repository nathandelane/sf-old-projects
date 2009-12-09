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

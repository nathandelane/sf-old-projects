using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	static class ExpressionYard
	{
		/// <summary>
		/// Forumulates the tokens contained in the tokenizer
		/// </summary>
		/// <param name="tokenizer"></param>
		/// <returns></returns>
		public static Expression Formulate(ITokenizer tokenizer)
		{
			Expression expression = default(Expression);

			if (tokenizer.HasTokens)
			{
				Queue<Expression> output = new Queue<Expression>();
				Token operation = new NullToken();

				foreach (Token token in tokenizer.Tokens)
				{
					if (token is NumberToken || token is ConstantToken)
					{
						output.Enqueue(new NumericExpression(token));

						if (!(operation is NullToken))
						{
							if (operation is OperatorToken && output.Count >= 2)
							{
								Expression subExp = new ArithmeticExpression(operation, output.Dequeue(), output.Dequeue());
							}
							else if (operation is FunctionToken && output.Count >= 1)
							{
								
							}

							operation = new NullToken();
						}
					}
					else if (token is OperatorToken)
					{
						operation = token;
					}
				}
			}

			return expression;
		}
	}
}

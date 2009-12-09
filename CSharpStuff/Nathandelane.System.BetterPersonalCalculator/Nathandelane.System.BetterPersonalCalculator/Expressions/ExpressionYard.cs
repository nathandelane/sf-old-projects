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
			Expression expression = null;

			if (tokenizer.HasTokens)
			{
				Queue<Token> output = Postfixate(tokenizer);
				Stack<Expression> expressionStack = new Stack<Expression>();

				while (output.Count > 0)
				{
					Token nextToken = output.Dequeue();

					if (nextToken is NumberToken)
					{
						expressionStack.Push(new NumericExpression(nextToken));
					}
					else
					{
						//if(nextToken is 
					}
				}
			}

			return expression;
		}

		/// <summary>
		/// Gets postfix-ordered Queue of Token objects.
		/// </summary>
		/// <param name="tokenizer"></param>
		/// <returns></returns>
		private static Queue<Token> Postfixate(ITokenizer tokenizer)
		{
			Queue<Token> output = new Queue<Token>();
			Stack<Token> operations = new Stack<Token>();
			bool openPerenthesisSet = false;

			foreach (Token token in tokenizer.Tokens)
			{
				if (token is NumberToken || token is ConstantToken)
				{
					output.Enqueue(token);
				}
				else if (token is OperatorToken)
				{
					if (operations.Count == 0 || operations.Peek().Precedence > token.Precedence)
					{
						operations.Push(token);
					}
					else if (operations.Peek().Precedence <= token.Precedence && !(token is PerenthesisToken))
					{
						output.Enqueue(operations.Pop());
					}
					else if (token is PerenthesisToken)
					{
						if (openPerenthesisSet)
						{
							while (!(operations.Peek() is PerenthesisToken))
							{
								Token nextOperator = operations.Pop();

								output.Enqueue(nextOperator);
							}

							operations.Pop();
							openPerenthesisSet = false;
						}
						else
						{
							openPerenthesisSet = true;
						}
					}
				}

				if (operations.Count > 0)
				{
					while (operations.Count > 0)
					{
						output.Enqueue(operations.Pop());
					}
				}
			}

			return output;
		}
	}
}

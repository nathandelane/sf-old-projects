using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.Math.Processor.Tokens;

namespace Nathandelane.Math.Processor.Evaluation
{
	public class PostfixExpression : Queue<IToken>
	{
		#region Constructors

		public PostfixExpression(Expression expression)
		{
			Postfixate(expression);
		}

		#endregion

		#region Private Methods

		private void Postfixate(Expression expression)
		{
			Stack<IToken> operatorStack = new Stack<IToken>();
			int index = 0;

			while (index < expression.Count)
			{
				IToken nextToken = expression[index];

				if (nextToken.Type == TokenType.Number)
				{
					Enqueue(nextToken);
				}
				else if (nextToken.Type == TokenType.Operator)
				{
					if (operatorStack.Count > 0)
					{
						if (operatorStack.Peek().Precedence.IsGreaterThan(nextToken.Precedence))
						{
							Enqueue(operatorStack.Pop());
							operatorStack.Push(nextToken);
						}
						else
						{
							Enqueue(nextToken);
						}
					}
					else
					{
						operatorStack.Push(nextToken);
					}
				}
				else if (nextToken.Type == TokenType.Structure)
				{
					if (nextToken is OpenPerenthesis)
					{
						operatorStack.Push(nextToken);
					}
					else
					{
						while (!(operatorStack.Peek() is OpenPerenthesis))
						{
							Enqueue(operatorStack.Pop());
						}

						operatorStack.Pop(); // Get rid of the open perenthesis
					}
				}

				index++;
			}

			DumpRemainingTokens(operatorStack);
		}

		private void DumpRemainingTokens(Stack<IToken> operatorStack)
		{
			while (operatorStack.Count > 0)
			{
				Enqueue(operatorStack.Pop());
			}
		}

		#endregion
	}
}

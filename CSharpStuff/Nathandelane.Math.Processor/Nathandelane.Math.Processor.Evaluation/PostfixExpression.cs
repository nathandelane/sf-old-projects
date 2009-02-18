using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.Math.Processor.Tokens;

namespace Nathandelane.Math.Processor.Evaluation
{
	public class PostfixExpression : List<IToken>
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
					Add(nextToken);
				}
				else if (nextToken.Type == TokenType.Operator)
				{
					if (operatorStack.Count > 0)
					{
						if (operatorStack.Peek().Precedence.IsGreaterThan(nextToken.Precedence))
						{
							Add(operatorStack.Pop());
							operatorStack.Push(nextToken);
						}
						else
						{
							Add(nextToken);
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
							Add(operatorStack.Pop());
						}

						operatorStack.Pop(); // Get rid of the open perenthesis
					}
				}
			}
		}

		#endregion
	}
}

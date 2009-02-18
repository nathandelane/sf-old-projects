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

		#region Public Methods

		public override string ToString()
		{
			string str = String.Empty;

			foreach (IToken token in this)
			{
				str = String.Concat(str, token.Value, ", ");
			}

			str = str.Substring(0, str.Length - 2);

			return str;
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
						if (nextToken.Precedence.Equals(operatorStack.Peek().Precedence))
						{
							Add(operatorStack.Pop());
							operatorStack.Push(nextToken);
						}
						else if (nextToken.Precedence.IsGreaterThan(operatorStack.Peek().Precedence))
						{
							operatorStack.Push(nextToken);
						}
						else if (nextToken.Precedence.IsLessThan(operatorStack.Peek().Precedence))
						{
							if (operatorStack.Peek() is OpenPerenthesis)
							{
								operatorStack.Push(nextToken);
							}
							else
							{
								Add(nextToken);
							}
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

				index++;
			}

			DumpRemainingTokens(operatorStack);
		}

		private void DumpRemainingTokens(Stack<IToken> operatorStack)
		{
			while (operatorStack.Count > 0)
			{
				Add(operatorStack.Pop());
			}
		}

		#endregion
	}
}

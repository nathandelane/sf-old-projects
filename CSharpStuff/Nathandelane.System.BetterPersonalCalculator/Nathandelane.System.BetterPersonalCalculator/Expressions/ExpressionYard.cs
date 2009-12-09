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

				expression = CreateExpression(output);
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
			Stack<Token> output = new Stack<Token>();
			Stack<Token> operations = new Stack<Token>();
			int openPerenthesisSet = 0;

			foreach (Token token in tokenizer.Tokens)
			{
				if (token is NumberToken || token is ConstantToken)
				{
					output.Push(token);
				}
				else if (token is OperatorToken || token is FunctionToken)
				{
					if (operations.Count == 0 || operations.Peek().Precedence < token.Precedence)
					{
						operations.Push(token);
					}
					else if (operations.Peek().Precedence >= token.Precedence && !(token is PerenthesisToken))
					{
						if (operations.Peek().Precedence == token.Precedence && operations.Peek().Precedence == ExpressionPrecedence.Function)
						{
							operations.Push(token);
						}
						else
						{
							output.Push(operations.Pop());
							operations.Push(token);
						}
					}
				}
				else if (token is PerenthesisToken)
				{
					if (openPerenthesisSet > 0)
					{
						while (!(operations.Peek() is PerenthesisToken))
						{
							Token nextOperator = operations.Pop();

							output.Push(nextOperator);
						}

						operations.Pop();
						openPerenthesisSet--;
					}
					else
					{
						operations.Push(token);
						openPerenthesisSet++;
					}
				}
			}

			if (operations.Count > 0)
			{
				while (operations.Count > 0)
				{
					output.Push(operations.Pop());
				}
			}

			Stack<Token> reversed = new Stack<Token>();
			while (output.Count > 0)
			{
				reversed.Push(output.Pop());
			}

			Queue<Token> tokenQueue = new Queue<Token>();
			while (reversed.Count > 0)
			{
				tokenQueue.Enqueue(reversed.Pop());
			}

			return tokenQueue;
		}

		/// <summary>
		/// Generates an expression.
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		private static Expression CreateExpression(Queue<Token> output)
		{
			Stack<Expression> expressionStack = new Stack<Expression>();

			while (output.Count > 0)
			{
				Token nextToken = output.Dequeue();

				if (nextToken is NumberToken || nextToken is ConstantToken)
				{
					expressionStack.Push(new NumericExpression(nextToken));
				}
				else if (nextToken is VariableToken)
				{
					if (CalculatorContext.GetInstance().ContainsKey(nextToken.ToString()))
					{
						expressionStack.Push(new NumericExpression(CalculatorContext.GetInstance()[nextToken.ToString()]));
					}
				}
				else if (nextToken is OperatorToken)
				{
					expressionStack.Push(new ArithmeticExpression(nextToken, expressionStack.Pop(), expressionStack.Pop()));
				}
				else if (nextToken is FunctionToken)
				{
					int argCount = ((FunctionToken)nextToken).NumArguments;
					Expression[] operands = new Expression[argCount];

					for (int counter = 0; counter < operands.Length; counter++)
					{
						operands[counter] = expressionStack.Pop();
					}

					expressionStack.Push(new FunctionExpression(nextToken, operands));
				}
			}

			return expressionStack.Pop();
		}
	}
}


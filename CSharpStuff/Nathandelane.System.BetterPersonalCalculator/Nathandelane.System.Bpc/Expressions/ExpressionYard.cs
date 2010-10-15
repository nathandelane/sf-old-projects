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
	static class ExpressionYard
	{
		/// <summary>
		/// Forumulates the tokens contained in the tokenizer
		/// </summary>
		/// <param name="postfixTokenizer"></param>
		/// <returns></returns>
		public static Expression Formulate(ITokenizer postfixTokenizer)
		{
			Expression expression = null;

			if (postfixTokenizer.HasTokens)
			{
				//Stack<Token> output = Postfixate(tokenizer);
				expression = CreateExpression(postfixTokenizer.Tokens);
			}
			else
			{
				Console.WriteLine("Tokenizer has no tokens.");
			}

			return expression;
		}

		#region Old Postfixator Code
		///// <summary>
		///// Gets postfix-ordered Queue of Token objects.
		///// </summary>
		///// <param name="tokenizer"></param>
		///// <returns></returns>
		//private static Stack<Token> Postfixate(ITokenizer tokenizer)
		//{
		//    Stack<Token> output = new Stack<Token>();
		//    Stack<Token> operations = new Stack<Token>();
		//    int openPerenthesisSet = 0;

		//    foreach (Token token in tokenizer.Tokens)
		//    {
		//        if (token is NumberToken || token is BooleanToken || token is VariableToken)
		//        {
		//            output.Push(token);
		//        }
		//        else if (token is OperatorToken || token is FunctionToken)
		//        {
		//            if (operations.Count == 0 || operations.Peek().Precedence < token.Precedence)
		//            {
		//                operations.Push(token);
		//            }
		//            else if (operations.Peek().Precedence >= token.Precedence && !(operations.Peek() is PerenthesisToken))
		//            {
		//                if (operations.Peek().Precedence == token.Precedence && operations.Peek().Precedence == ExpressionPrecedence.Function)
		//                {
		//                    operations.Push(token);
		//                }
		//                else
		//                {
		//                    output.Push(operations.Pop());
		//                    operations.Push(token);
		//                }
		//            }
		//            else if(operations.Peek() is PerenthesisToken)
		//            {
		//                operations.Push(token);
		//            }
		//        }
		//        else if (token is PerenthesisToken)
		//        {
		//            if (openPerenthesisSet > 0 && token.ToString().Equals(")", StringComparison.InvariantCultureIgnoreCase))
		//            {
		//                while (!(operations.Peek() is PerenthesisToken))
		//                {
		//                    Token nextOperator = operations.Pop();

		//                    output.Push(nextOperator);
		//                }

		//                operations.Pop();
		//                openPerenthesisSet--;
		//            }
		//            else
		//            {
		//                operations.Push(token);
		//                openPerenthesisSet++;
		//            }
		//        }
		//    }

		//    if (operations.Count > 0)
		//    {
		//        while (operations.Count > 0)
		//        {
		//            output.Push(operations.Pop());
		//        }
		//    }

		//    Stack<Token> reversed = new Stack<Token>();
		//    while (output.Count > 0)
		//    {
		//        reversed.Push(output.Pop());
		//    }

		//    return reversed;
		//}
		#endregion

		/// <summary>
		/// Generates an expression.
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		private static Expression CreateExpression(IList<Token> outputList)
		{
			TokenListStackAdapter output = new TokenListStackAdapter(outputList);
			Stack<Expression> expressionStack = new Stack<Expression>();

			while (output.Count > 0)
			{
				Token nextToken = output.Pop();

				if (nextToken is BooleanToken)
				{
					expressionStack.Push(new BooleanValueExpression(nextToken));
				}
				else if (nextToken is VariableToken)
				{
					expressionStack.Push(new VariableExpression(nextToken));
				}
				else if (nextToken is ConstantToken || nextToken is NumberToken)
				{
					expressionStack.Push(new NumericExpression(nextToken));
				}
				else if (nextToken is ArithmeticOperatorToken)
				{
					expressionStack.Push(new ArithmeticExpression(nextToken, expressionStack.Pop(), expressionStack.Pop()));
				}
				else if (nextToken is BinaryOperatorToken)
				{
					expressionStack.Push(new BinaryExpression(nextToken, expressionStack.Pop(), expressionStack.Pop()));
				}
				else if (nextToken is BooleanOperatorToken)
				{
					expressionStack.Push(new BooleanExpression(nextToken, expressionStack.Pop(), expressionStack.Pop()));
				}
				else if (nextToken is AssignmentOperatorToken)
				{
					expressionStack.Push(new AssignmentExpression(nextToken, expressionStack.Pop(), expressionStack.Pop()));
				}
				else if (nextToken is FunctionToken)
				{
					int argCount = 0;

					if (nextToken is PrefixFunctionToken || nextToken is PostfixFunctionToken)
					{
						argCount = 1;
					}
					else if (nextToken is InfixFunctionToken)
					{
						argCount = 2;
					}

					Expression[] operands = new Expression[argCount];

					for (int counter = 0; counter < operands.Length; counter++)
					{
						operands[counter] = expressionStack.Pop();
					}

					expressionStack.Push(new FunctionExpression(nextToken, operands));
				}
			}

			if (expressionStack.Count > 1)
			{
				throw new MalformedExpressionException(String.Format("There were too many expressions still on the stack: {0}", expressionStack.Count));
			}

			return expressionStack.Pop();
		}
	}
}


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
	public class PostfixTokenizer : ITokenizer
	{
		#region Fields

		private IList<Token> _tokens;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a list of Tokens.
		/// </summary>
		public IList<Token> Tokens
		{
			get { return _tokens; }
		}

		/// <summary>
		/// Gets whether this tokenizer has tokens.
		/// </summary>
		public bool HasTokens
		{
			get { return _tokens.Count > 0; }
		}

		#endregion

		#region Constructors

		public PostfixTokenizer(ITokenizer stringTokenizer)
		{
			_tokens = new List<Token>();

			Postfixate(stringTokenizer);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Transforms the tokens in the tokenizer into postfix order for use by the ExpressionYard.
		/// </summary>
		/// <param name="tokenizer"></param>
		private void Postfixate(ITokenizer tokenizer)
		{
			Stack<Token> output = new Stack<Token>();
			Stack<Token> operations = new Stack<Token>();
			int openPerenthesisSet = 0;

			foreach (Token token in tokenizer.Tokens)
			{
				if (token is NumberToken || token is BooleanToken || token is VariableToken)
				{
					output.Push(token);
				}
				else if (token is OperatorToken || token is FunctionToken)
				{
					if (operations.Count == 0 || operations.Peek().Precedence < token.Precedence)
					{
						operations.Push(token);
					}
					else if (operations.Peek().Precedence >= token.Precedence && !(operations.Peek() is PerenthesisToken))
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
					else if (operations.Peek() is PerenthesisToken)
					{
						operations.Push(token);
					}
				}
				else if (token is PerenthesisToken)
				{
					if (openPerenthesisSet > 0 && token.ToString().Equals(")", StringComparison.InvariantCultureIgnoreCase))
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

			while (output.Count > 0)
			{
				_tokens.Add(output.Pop());
			}
		}

		#endregion
	}
}

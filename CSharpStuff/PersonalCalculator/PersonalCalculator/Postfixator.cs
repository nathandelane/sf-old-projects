using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
	public class Postfixator : IDisposable
	{
		private static Stack<Token> _postfixEquation;
		private static Stack<Token> _operatorStack;

		private Postfixator()
		{
			_postfixEquation = new Stack<Token>();
			_operatorStack = new Stack<Token>();
		}

		public static Postfixator CreateInstance(string equation)
		{
			Postfixator result = null;

			if (Utility.IsInfix(equation))
			{
				result = new Postfixator();

				result.Postfixate(equation);
			}
			else
			{
				result = CreateNullPostfixator();
			}

			return result;
		}

		private static Postfixator CreateNullPostfixator()
		{
			Postfixator result = new Postfixator();

			result.PostfixEquation.Push(new Token(TokenType.Number, "0"));

			return result;
		}

		public Stack<Token> PostfixEquation
		{
			get { return _postfixEquation; }
		}

		private void Postfixate(string equation)
		{
			Parser parser = new Parser(equation);

			foreach (Token t in parser.Tokens)
			{
				switch (t.Type)
				{
					case TokenType.Number:
						_postfixEquation.Push(t);
						break;
					case TokenType.ArithmeticOperator:
						if (t.Value.Equals("+") || t.Value.Equals("-"))
						{
							if (_operatorStack.Count > 0)
							{
								if ((_operatorStack.Peek() as Token).Equals(Token.AdditionOperator))
								{
									_operatorStack.Push(t);
								}
								else if ((_operatorStack.Peek() as Token).Equals(Token.SubtractionOperator))
								{
									_operatorStack.Push(t);
								}
								else if ((_operatorStack.Peek() as Token).Equals(Token.MultiplicationOperator))
								{
									_postfixEquation.Push(_operatorStack.Pop());
									_operatorStack.Push(t);
								}
								else if ((_operatorStack.Peek() as Token).Equals(Token.DivisionOperator))
								{
									_postfixEquation.Push(_operatorStack.Pop());
									_operatorStack.Push(t);
								}
								else if ((_operatorStack.Peek() as Token).Equals(Token.OpenPerenthesis))
								{
									_operatorStack.Push(t);
								}
							}
							else
							{
								_operatorStack.Push(t);
							}
						}
						else if (t.Value.Equals("*") || t.Value.Equals("/"))
						{
							_operatorStack.Push(t);
						}
						break;
					case TokenType.Perentheses:
						if (t.Value.Equals("("))
						{
							_operatorStack.Push(t);
						}
						else if (t.Value.Equals(")"))
						{
							while (!(_operatorStack.Peek() as Token).Equals(Token.OpenPerenthesis))
							{
								_postfixEquation.Push(_operatorStack.Pop());
							}

							// Pop the open perenthesis off last
							_operatorStack.Pop();
						}
						break;
				}
			}

			while (_operatorStack.Count > 0)
			{
				_postfixEquation.Push(_operatorStack.Pop());
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			// Dispose
		}

		#endregion
	}
}
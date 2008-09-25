using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
	public class ArithmeticPostfixEvaluator
	{
		private ArithmeticPostfixator _postfixator;
		private string _result;

		private ArithmeticPostfixEvaluator(ArithmeticPostfixator postfixator)
		{
			_postfixator = postfixator;
			_result = String.Empty;
		}

		public string Result
		{
			get { return _result; }
		}

		public static ArithmeticPostfixEvaluator CreateInstance(ArithmeticPostfixator postfixator)
		{
			ArithmeticPostfixEvaluator result = null;

			if (postfixator.PostfixEquation.Count > 0)
			{
				result = new ArithmeticPostfixEvaluator(postfixator);

				result.Evaluate();
			}

			return result;
		}

		private void Evaluate()
		{
			string left = String.Empty;
			string right = String.Empty;
			Stack<Token> evalStack = new Stack<Token>();
			Token nextToken = new Token(TokenType.Void, String.Empty);
			int totalTokens = _postfixator.PostfixEquation.Count;

			do
			{
				if (_postfixator.PostfixEquation.Count == 0)
				{
					while (evalStack.Count > 0)
					{
						_postfixator.PostfixEquation.Push(evalStack.Pop());
					}

					nextToken = _postfixator.PostfixEquation.Pop();
				}
				else
				{
					nextToken = _postfixator.PostfixEquation.Pop();
				}

				switch (nextToken.Type)
				{
					case TokenType.Number:
						if (left.Equals(String.Empty))
						{
							evalStack.Push(nextToken);
						}
						break;
					case TokenType.ArithmeticOperator:
						if (_postfixator.PostfixEquation.Peek().Type == TokenType.Number)
						{
							right = _postfixator.PostfixEquation.Pop().Value;

							if (_postfixator.PostfixEquation.Count >= 1)
							{
								if (_postfixator.PostfixEquation.Peek().Type == TokenType.Number)
								{
									left = _postfixator.PostfixEquation.Pop().Value;

									if (nextToken.Value.Equals("+"))
									{
										if (!String.IsNullOrEmpty(left))
										{
											_result = Utility.Add(left, right);
										}
										else
										{
											_result = String.Format("{0}", right);
										}
									}
									else if (nextToken.Value.Equals("-"))
									{
										if (!String.IsNullOrEmpty(left))
										{
											_result = Utility.Sub(left, right);
										}
										else
										{
											_result = String.Format("{0}", ((-1) * double.Parse(right)));
										}
									}
									else if (nextToken.Value.Equals("*"))
									{
										_result = Utility.Mul(left, right);
									}
									else if (nextToken.Value.Equals("/"))
									{
										_result = Utility.Div(left, right);
									}
									else
									{
										evalStack.Push(nextToken);
									}

									evalStack.Push(new Token(TokenType.Number, _result));
								}
								else
								{
									evalStack.Push(nextToken);
									evalStack.Push(new Token(TokenType.Number, right));
								}
							}
							else if(nextToken.Value.Equals("-"))
							{
								_result = String.Format("{0}", ((-1) * double.Parse(right)));
								evalStack.Push(new Token(TokenType.Number, _result));
							}
						}
						else
						{
							evalStack.Push(nextToken);
						}
						break;
				}

				totalTokens = _postfixator.PostfixEquation.Count + evalStack.Count;
				right = String.Empty;
				left = String.Empty;
			}
			while (totalTokens > 1);

			_result = evalStack.Pop().Value;
		}
	}
}

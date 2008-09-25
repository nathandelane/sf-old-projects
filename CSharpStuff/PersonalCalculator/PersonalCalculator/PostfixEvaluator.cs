using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
	public class PostfixEvaluator
	{
		private Postfixator _postfixator;
		private string _result;

		private PostfixEvaluator(Postfixator postfixator)
		{
			_postfixator = postfixator;
			_result = String.Empty;
		}

		public string Result
		{
			get { return _result; }
		}

		public static PostfixEvaluator CreateInstance(Postfixator postfixator)
		{
			PostfixEvaluator result = null;

			if (postfixator.PostfixEquation.Count > 0)
			{
				result = new PostfixEvaluator(postfixator);

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
							if (_postfixator.PostfixEquation.Count > 1)
							{
								right = _postfixator.PostfixEquation.Pop().Value;
								left = _postfixator.PostfixEquation.Pop().Value;
							}
							else
							{
								right = _postfixator.PostfixEquation.Pop().Value;
								left = String.Empty;
							}
							//***
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
						}
						break;
				}

				totalTokens = _postfixator.PostfixEquation.Count + evalStack.Count;
				right = String.Empty;
			}
			while (totalTokens > 1);

			_result = evalStack.Pop().Value;
		}
	}
}

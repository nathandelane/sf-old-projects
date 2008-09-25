using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
	public class ArithmeticParser
	{
		private List<Token> _tokens;

		public ArithmeticParser(string equation)
		{
			_tokens = new List<Token>();

			ParseEquation(equation);
		}

		public List<Token> Tokens
		{
			get { return _tokens; }
		}

		private void ParseEquation(string equation)
		{
			bool negationFlag = false;
			char[] elementParts = equation.ToCharArray();
			Token lastElement = new Token(TokenType.Void, String.Empty);

			for (int i = 0; i < elementParts.Length; i++)
			{
				string el = String.Format("{0}", elementParts[i]);

				if (Utility.IsNumeric(el))
				{
					string element = el;
					int j = 1;

					try
					{
						do
						{
							el = String.Format("{0}", elementParts[i + j]);

							if (Utility.IsNumeric(el))
							{
								element = String.Concat(new string[] { element, el });
								j++;
							}
						}
						while (Utility.IsNumeric(el));
					}
					catch (Exception)
					{
					}

					i += (j - 1);

					if (negationFlag)
					{
						double d = double.Parse(element);
						d *= (-1);
						element = String.Format("{0}", d);
						negationFlag = false;
					}

					lastElement = new Token(TokenType.Number, element);
					_tokens.Add(lastElement);
				}
				else if (Utility.IsOperator(el))
				{
					switch (el)
					{
						case "+":
							lastElement = new Token(TokenType.ArithmeticOperator, el);
							_tokens.Add(lastElement);
							break;
						case "-":
							if (lastElement.Type != TokenType.Number && Utility.IsNumeric(String.Format("{0}", elementParts[i + 1])))
							{
								negationFlag = true;
							}
							else
							{
								lastElement = new Token(TokenType.ArithmeticOperator, el);
								_tokens.Add(lastElement);
							}
							break;
						case "*":
							lastElement = new Token(TokenType.ArithmeticOperator, el);
							_tokens.Add(lastElement);
							break;
						case "/":
							lastElement = new Token(TokenType.ArithmeticOperator, el);
							_tokens.Add(lastElement);
							break;
						case "(":
							lastElement = new Token(TokenType.Perentheses, el);
							_tokens.Add(lastElement);
							break;
						case ")":
							lastElement = new Token(TokenType.Perentheses, el);
							_tokens.Add(lastElement);
							break;
					}
				}
				else if (Utility.IsFunction(el))
				{
					string element = String.Empty;
					int j = 1;

					do
					{
						el = String.Format("{0}", elementParts[i + j]);

						if (Utility.IsFunction(el))
						{
							element = String.Concat(new string[] { element, el });
							j++;
						}
					}
					while (Utility.IsFunction(el));

					i += (j - 1);
					_tokens.Add(new Token(TokenType.Number, element));
				}
			}
		}
	}
}

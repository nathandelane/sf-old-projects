using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
	public class Token
	{
		private TokenType _type;
		private string _value;

		public Token(TokenType type, string value)
		{
			_type = type;
			_value = value;
		}

		public TokenType Type
		{
			get { return _type; }
		}

		public string Value
		{
			get { return _value; }
		}

		public bool Equals(Token token)
		{
			bool result = true;

			if (this.Type != token.Type)
			{
				result = false;
			}
			else if (!this.Value.Equals(token.Value))
			{
				result = false;
			}

			return result;
		}

		public static Token AdditionOperator
		{
			get { return new Token(TokenType.ArithmeticOperator, "+"); }
		}

		public static Token SubtractionOperator
		{
			get { return new Token(TokenType.ArithmeticOperator, "-"); }
		}

		public static Token MultiplicationOperator
		{
			get { return new Token(TokenType.ArithmeticOperator, "*"); }
		}

		public static Token DivisionOperator
		{
			get { return new Token(TokenType.ArithmeticOperator, "/"); }
		}

		public static Token OpenPerenthesis
		{
			get { return new Token(TokenType.Perentheses, "("); }
		}

		public static Token ClosePerenthesis
		{
			get { return new Token(TokenType.Perentheses, ")"); }
		}
	}
}

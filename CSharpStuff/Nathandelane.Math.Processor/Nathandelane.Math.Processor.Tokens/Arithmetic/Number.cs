﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.Processor.Tokens
{
    public class Number : IToken
	{
		#region Fields

		private TokenType _type;
		private TokenPrecedence _precedence;
		private string _value;

		#endregion

		#region Constructors

		public Number(long value)
		{
			_value = value.ToString();
			_type = TokenType.Number;
			_precedence = new TokenPrecedence(-1);
		}

		public Number(int value)
		{
			_value = value.ToString();
			_type = TokenType.Number;
			_precedence = new TokenPrecedence(-1);
		}

		public Number(double value)
		{
			_value = value.ToString();
			_type = TokenType.Number;
			_precedence = new TokenPrecedence(-1);
		}

		#endregion

		#region IToken Members

		public TokenType Type
		{
			get
			{
				return _type;
			}
		}

		public TokenPrecedence Precedence
		{
			get
			{
				return _precedence;
			}
			set
			{
				_precedence = value;
			}
		}

		public string Value
		{
			get
			{
				return _value;
			}
		}

		public bool Matches(string str)
		{
			Regex regex = new Regex("[\\d]{1}");

			return regex.IsMatch(str);
		}

		#endregion
	}
}
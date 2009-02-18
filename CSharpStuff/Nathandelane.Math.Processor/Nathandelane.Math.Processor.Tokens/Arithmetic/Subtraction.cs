﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.Processor.Tokens
{
	public class Subtraction : IToken
	{
		#region Fields

		private readonly Regex regex = new Regex("^[-]{1}");
		private TokenType _type;
		private TokenPrecedence _precedence;
		private string _value;

		#endregion

		#region Constructors

		public Subtraction()
		{
			_type = TokenType.Operator;
			_precedence = new TokenPrecedence(5);
			_value = "-";
		}

		#endregion

		#region IToken Members

		public TokenType Type
		{
			get { return _type; }
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
			get { return _value; }
		}

		public bool Matches(string str)
		{
			return regex.IsMatch(str);
		}

		public new string FirstMatch(string str)
		{
			return regex.Matches(str)[0].Value;
		}

		public override string ToString()
		{
			return String.Format("{Type={0};Value={1};Precedence={2}}", Type, Value, Precedence);
		}

		#endregion
	}
}

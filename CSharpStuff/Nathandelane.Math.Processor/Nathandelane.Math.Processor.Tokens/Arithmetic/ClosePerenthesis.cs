﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.Processor.Tokens
{
	public class ClosePerenthesis : IToken
	{
		#region Fields

		private TokenType _type;
		private TokenPrecedence _precedence;
		private string _value;

		#endregion

		#region Constructors

		public ClosePerenthesis()
		{
			_type = TokenType.Structure;
			_precedence = new TokenPrecedence(1);
			_value = ")";
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
			Regex regex = new Regex("[)]{1}");

			return regex.IsMatch(str);
		}

		#endregion
	}
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Math.Processor.Tokens
{
	public class OpenPerenthesis : IToken
	{
		#region Fields

		private TokenType _type;
		private TokenPrecedence _precedence;
		private string _value;

		#endregion

		#region Constructors

		public OpenPerenthesis()
		{
			_type = TokenType.Structure;
			_precedence = new TokenPrecedence(0);
			_value = "(";
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

		#endregion
	}
}

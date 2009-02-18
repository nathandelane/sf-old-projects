using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.Processor.Tokens
{
	public class Addition : IToken
	{
		#region Fields

		private readonly Regex regex = new Regex("^[+]{1}");
		private TokenType _type;
		private TokenPrecedence _precedence;
		private string _value;

		#endregion

		#region Constructors

		public Addition()
		{
			_type = TokenType.Operator;
			_precedence = new TokenPrecedence(6);
			_value = "+";
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

		public string FirstMatch(string str)
		{
			return regex.Matches(str)[0].Value;
		}

		#endregion
	}
}

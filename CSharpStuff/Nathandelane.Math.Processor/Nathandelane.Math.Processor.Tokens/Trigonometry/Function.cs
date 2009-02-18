using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Math.Processor.Tokens
{
	public abstract class Function : IToken
	{
		#region Fields

		private TokenType _type;
		private TokenPrecedence _precedence;
		private string _value;

		#endregion

		#region Constructors

		public Function(string name)
		{
			_type = TokenType.Function;
			_precedence = new TokenPrecedence(5);
			_value = name;
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
			throw new NotImplementedException();
		}

		public string FirstMatch(string str)
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return String.Format("{Type={0};Value={1};Precedence={2}}", Type, Value, Precedence);
		}

		#endregion
	}
}

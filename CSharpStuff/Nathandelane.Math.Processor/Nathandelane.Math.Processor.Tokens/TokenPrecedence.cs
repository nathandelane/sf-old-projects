using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Math.Processor.Tokens
{
	public class TokenPrecedence
	{
		#region Fields

		private int _value;

		#endregion

		#region Properties

		internal int Value
		{
			get { return _value; }
		}

		#endregion

		#region Constructors

		public TokenPrecedence(int value)
		{
			_value = value;
		}

		#endregion

		#region Public Methods

		public bool IsGreaterThan(TokenPrecedence otherTokenPrecedence)
		{
			return (Value > otherTokenPrecedence.Value);
		}

		public bool IsLessThan(TokenPrecedence otherTokenPrecedence)
		{
			return (Value < otherTokenPrecedence.Value);
		}

		public bool Equals(TokenPrecedence otherTokenPrecedence)
		{
			return (Value == otherTokenPrecedence.Value);
		}

		#endregion
	}
}

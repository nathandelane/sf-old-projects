using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public abstract class Token
	{
		#region Fields

		private string _value;

		#endregion

		#region Properties

		public abstract TokenType Type { get; }

		public abstract ExpressionPrecedence Precedence { get; }

		/// <summary>
		/// Gets the length of the token value.
		/// </summary>
		public int Length
		{
			get { return _value.Length; }
		}

		#endregion

		#region Constructors

		protected Token(string value)
		{
			_value = value;
		}

		protected Token(Token other)
		{
			_value = other._value;
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			return _value;
		}

		#endregion
	}
}

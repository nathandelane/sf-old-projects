using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.Bpc
{
	public class OperatorToken : Token
	{
		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Operator; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region Constructors

		protected OperatorToken(string value)
			: base(value)
		{
		}

		protected OperatorToken(Token other)
			: base(other)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Attempts to parse a token and returns success or failure.
		/// </summary>
		/// <param name="line">String from which to take the next token.</param>
		/// <param name="token">Out parameter to send token to if successful.</param>
		/// <returns></returns>
		public static bool TryParse(string line, out Token token)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets a Token of type OperatorToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public new static Token Parse(string line)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

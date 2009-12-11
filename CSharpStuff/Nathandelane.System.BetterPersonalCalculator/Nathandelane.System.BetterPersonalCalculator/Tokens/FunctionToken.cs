using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public abstract class FunctionToken : Token
	{
		#region Fields

		private ExpressionPrecedence _precedence;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the TokenType.
		/// </summary>
		public override TokenType Type
		{
			get { return TokenType.Function; }
		}

		/// <summary>
		/// Gets the ExpressionPrecedence.
		/// </summary>
		public override ExpressionPrecedence Precedence
		{
			get { return _precedence; }
		}

		#endregion

		#region Constructors

		protected FunctionToken(string value)
			: base(value)
		{
			_precedence = ExpressionPrecedence.Function;
		}

		protected FunctionToken(Token other)
			: base(other)
		{
			_precedence = ExpressionPrecedence.Function;
		}

		#endregion
	}
}

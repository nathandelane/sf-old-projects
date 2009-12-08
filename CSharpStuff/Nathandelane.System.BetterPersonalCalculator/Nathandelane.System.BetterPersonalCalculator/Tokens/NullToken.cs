using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NullToken : Token
	{
		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Null; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Number; }
		}

		#endregion

		#region Constructors

		public NullToken()
			: base(String.Empty)
		{
		}

		#endregion
}
}

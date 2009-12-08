using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class ConstantExpression : Expression
	{
		#region Fields

		private Token _value;

		#endregion

		#region Constructors

		public ConstantExpression(Token value)
			: base(ExpressionPrecedence.Constant, new NullToken(), new List<Token>())
		{
			_value = value;
		}

		#endregion

		#region Methods

		public override Token Evaluate()
		{
			return _value;
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NumericExpression : Expression
	{
		#region Fields

		private Token _value;

		#endregion

		#region Constructors

		public NumericExpression(Token value)
			: base(ExpressionPrecedence.Number, new NullToken(), new List<Expression>())
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

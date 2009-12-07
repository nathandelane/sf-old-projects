using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public abstract class AbstractExpression
	{
		#region Fields

		private Queue<IToken> _operands;
		private IToken _operation;

		#endregion

		#region Constructors

		protected AbstractExpression(IToken functionToken, IToken value)
		{
			_operation = functionToken;
			_operands = new Queue<IToken>();
			_operands.Enqueue(value);
		}

		protected AbstractExpression(IToken operatorToken, IToken left, IToken right)
		{
			_operation = operatorToken;
			_operands = new Queue<IToken>();
			_operands.Enqueue(left);
			_operands.Enqueue(right);
		}

		#endregion

		#region Methods

		public abstract IToken Calculate();

		#endregion
	}
}

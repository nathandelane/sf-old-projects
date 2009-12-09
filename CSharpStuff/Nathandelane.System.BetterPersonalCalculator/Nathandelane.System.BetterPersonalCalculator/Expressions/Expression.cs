using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public abstract class Expression
	{
		#region Fields

		private ExpressionPrecedence _precedence;
		private Token _operation;
		private IList<Expression> _operands;

		#endregion

		#region Properties

		public ExpressionPrecedence Precedence
		{
			get { return _precedence; }
		}

		public Token Operation
		{
			get { return _operation; }
		}

		public IList<Expression> Operands
		{
			get { return _operands; }
		}

		#endregion

		#region Constructors

		protected Expression(ExpressionPrecedence precedence, Token operation, IList<Expression> operands)
		{
			_precedence = precedence;
			_operation = operation;
			_operands = operands;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Evaluates this expression.
		/// </summary>
		/// <returns></returns>
		public abstract Token Evaluate();

		public override string ToString()
		{
			string[] strOperands = new string[_operands.Count];

			for(int counter = 0; counter < strOperands.Length; counter++)
			{
				strOperands[counter] = _operands[counter].ToString();
			}

			return String.Concat(Operation.ToString(), " ", String.Join(", ", strOperands));
		}

		#endregion
	}
}

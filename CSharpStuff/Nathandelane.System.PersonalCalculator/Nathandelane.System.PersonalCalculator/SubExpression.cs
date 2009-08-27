using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator
{
	public class SubExpression
	{
		#region Fields

		private string _expression;
		private TokenType _operator;

		#endregion

		#region Properties

		public string Expression
		{
			get { return _expression; }
		}

		public TokenType Operator
		{
			get { return _operator; }
		}

		#endregion

		#region Constructor

		public SubExpression(string expression, TokenType op)
		{
			_expression = expression;
			_operator = op;
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			return _expression;
		}

		#endregion
	}
}

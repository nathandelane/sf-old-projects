using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Division : AbstractToken, IArithmeticOperator
	{
		#region fields

		private AbstractToken _left;
		private AbstractToken _right;

		#endregion

		#region Constructors

		public Division(AbstractToken left, AbstractToken right)
			: base("/")
		{
			_left = left;
			_right = right;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Calculates the result of left divided by the right.
		/// </summary>
		/// <returns></returns>
		public AbstractToken Calculate()
		{
			AbstractToken result = new NotANumber();

			if (_right.Value.Equals("0", StringComparison.InvariantCultureIgnoreCase) || _right.Value.Equals("0.0", StringComparison.InvariantCultureIgnoreCase))
			{
				throw new DivideByZeroException();
			}

			result = new Number((double.Parse(_left.Value, CultureInformation) / double.Parse(_right.Value, CultureInformation)).ToString());

			return result;
		}

		#endregion
}
}

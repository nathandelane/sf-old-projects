using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public interface IArithmeticOperator
	{
		/// <summary>
		/// Calculates the arithmetic result of the operation.
		/// </summary>
		/// <param name="left">Left operand of the equation.</param>
		/// <param name="right">Right operand of the equation.</param>
		/// <returns></returns>
		AbstractToken Calculate(AbstractToken left, AbstractToken right);
	}
}

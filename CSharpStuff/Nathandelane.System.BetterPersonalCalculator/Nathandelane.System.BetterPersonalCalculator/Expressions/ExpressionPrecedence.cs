using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	/// <summary>
	/// Represents an order of operations where Number is the lowest evaluation and Perenthesis is the highest order.
	/// </summary>
	public enum ExpressionPrecedence
	{
		Null,
		Constant,
		Number,
		Variable,
		Add,
		Subtract,
		MultiplyOrDivide,
		Function,
		Perenthesis
	}
}

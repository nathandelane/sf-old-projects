using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public enum ExpressionPrecedence
	{
		Variable,
		Number,
		Addition,
		Subtraction,
		Multiplication,
		Division,
		ClosePerenthesis,
		OpenPerenthesis
	}
}

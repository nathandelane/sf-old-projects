using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	/// <summary>
	/// Defines the type of token.
	/// </summary>
	public enum TokenType
	{
		Null,
		NotANumber,
		Constant,
		Number,
		Variable,
		Operator,
		Function
	}
}

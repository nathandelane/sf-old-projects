using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator2
{
	public enum TokenType
	{
		Null,
		DecimalNumber,
		HexadecimalNumber,
		Add,
		Subtract,
		Negation,
		Factorial,
		Power,
		Multiply,
		Divide,
		OpeningParenthesis,
		ClosingParenthesis,
		Modulus,
		Div,
		Function
	}
}

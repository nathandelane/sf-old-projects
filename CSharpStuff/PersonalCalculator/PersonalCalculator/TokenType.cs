using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
	public enum TokenType
	{
		TraceCommand = -1,
		Void = 0,
		Number = 1,
		ArithmeticOperator = 2,
		Perentheses = 3,
		Function = 4,
		Variable = 5
	}
}

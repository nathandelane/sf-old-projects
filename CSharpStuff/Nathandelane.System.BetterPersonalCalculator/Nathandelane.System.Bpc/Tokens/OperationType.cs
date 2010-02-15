using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	/// <summary>
	/// Defines the different types of operations that can occur.
	/// Infix means that it is a basic operator that requires two operands, a left and a right.
	/// InfixFunction means that it is considered a function, but works like an infix operator.
	/// PostfixFunction means that it is a function that is placed after its single operand.
	/// PrefixFunction means that it is a function that is placed before its singleoperand.
	/// </summary>
	public enum OperationType
	{
		Infix,
		InfixFunction,
		PostfixFunction,
		PrefixFunction
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public interface IToken
	{
		/// <summary>
		/// Gets the value of this token.
		/// </summary>
		string Value { get; }

		/// <summary>
		/// Gets the Regex object used to match tokens of this type.
		/// </summary>
		Regex MatchExpression { get; }
	}
}

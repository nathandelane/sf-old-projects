using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public interface IToken
	{
		/// <summary>
		/// Gets the value of this token.
		/// </summary>
		string Value { get; }
	}
}

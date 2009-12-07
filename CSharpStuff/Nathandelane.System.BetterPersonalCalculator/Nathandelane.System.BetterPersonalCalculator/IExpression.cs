using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public interface IExpression
	{
		#region Properties

		Regex MatchExpression { get; }

		#endregion

		#region Methods

		string Calculate();

		#endregion
	}
}

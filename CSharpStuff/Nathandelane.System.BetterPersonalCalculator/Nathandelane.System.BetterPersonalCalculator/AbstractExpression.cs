using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public abstract class AbstractExpression
	{
		#region Methods

		public abstract string Calculate();

		public override string ToString()
		{
			return Calculate();
		}

		#endregion
	}
}

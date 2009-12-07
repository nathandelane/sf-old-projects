using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NonNumeric : IExpression
	{
		#region Properties

		public Regex MatchExpression
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region Methods

		public string Calculate()
		{
			return "i";
		}

		#endregion
	}
}

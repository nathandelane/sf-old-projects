using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NonNumeric : AbstractExpression
	{
		#region Properties

		public static Regex MatchExpression
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region Methods

		public override string Calculate()
		{
			return "i";
		}

		#endregion
	}
}

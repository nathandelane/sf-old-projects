﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NonNumeric : IExpression
	{
		#region Properties

		public static Regex MatchExpression
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region Methods

		public IExpression Calculate(IDictionary<string, IExpression> context)
		{
			return null;
		}

		public override string ToString()
		{
			return "i";
		}

		#endregion
	}
}

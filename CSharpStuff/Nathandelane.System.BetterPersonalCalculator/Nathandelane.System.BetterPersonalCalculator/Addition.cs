﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Addition : IExpression
	{
		#region Fields

		private static readonly string __matchExpression = "^[+]{1}";

		private IExpression _left;
		private IExpression _right;

		#endregion

		#region Properties

		public static string MatchExpression
		{
			get { return Addition.__matchExpression; }
		}

		#endregion

		#region Constructors

		public Addition(IExpression left, IExpression right)
		{
			_left = left;
			_right = right;
		}

		#endregion

		#region Methods

		public IExpression Calculate(IEnumerable<IExpression> operands)
		{
			IExpression result = new Numeric("0");

			return result;
		}

		public override string ToString()
		{
			return String.Concat(_left.ToString(), " + ", _right.ToString());
		}

		#endregion
	}
}

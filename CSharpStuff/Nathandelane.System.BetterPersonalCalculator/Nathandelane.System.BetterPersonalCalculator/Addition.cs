using System;
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

		private static readonly Regex __matchExpression = new Regex("^[+]{1}");

		private IExpression _left;
		private IExpression _right;

		#endregion

		#region Properties

		public Regex MatchExpression
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

		public string Calculate()
		{
			return (double.Parse(_left.Calculate(), CultureInfo.CurrentCulture) + double.Parse(_right.Calculate(), CultureInfo.CurrentCulture)).ToString();
		}

		public override string ToString()
		{
			return Calculate();
		}

		#endregion
	}
}

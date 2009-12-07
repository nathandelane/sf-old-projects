using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Subtraction : IExpression
	{
		#region Fields

		private static readonly Regex __matchExpression = new Regex("^[-]{1}");

		private IExpression _left;
		private IExpression _right;

		#endregion

		#region Properties

		public Regex MatchExpression
		{
			get { return Subtraction.__matchExpression; }
		}

		#endregion

		#region Constructors

		public Subtraction(IExpression left, IExpression right)
		{
			_left = left;
			_right = right;
		}

		#endregion

		#region Methods

		public string Calculate()
		{
			return (double.Parse(_left.Calculate(), CultureInfo.CurrentCulture) - double.Parse(_right.Calculate(), CultureInfo.CurrentCulture)).ToString();
		}

		public override string ToString()
		{
			return Calculate();
		}

		#endregion
	}
}

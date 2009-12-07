using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Subtraction : AbstractExpression
	{
		#region Fields

		private static readonly string __matchExpression ="^[-]{1}";

		private AbstractExpression _left;
		private AbstractExpression _right;

		#endregion

		#region Properties

		public static string MatchExpression
		{
			get { return Subtraction.__matchExpression; }
		}

		#endregion

		#region Constructors

		public Subtraction(AbstractExpression left, AbstractExpression right)
		{
			_left = left;
			_right = right;
		}

		#endregion

		#region Methods

		public override string Calculate()
		{
			return (double.Parse(_left.Calculate(), CultureInfo.CurrentCulture) - double.Parse(_right.Calculate(), CultureInfo.CurrentCulture)).ToString();
		}

		#endregion
	}
}

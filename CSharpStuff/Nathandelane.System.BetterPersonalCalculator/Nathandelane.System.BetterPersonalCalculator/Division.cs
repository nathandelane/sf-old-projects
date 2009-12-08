using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Division : IExpression
	{
		#region Fields

		private static readonly string __matchExpression = "^[-]{1}";

		private IExpression _left;
		private IExpression _right;

		#endregion

		#region Properties

		public static string MatchExpression
		{
			get { return Division.__matchExpression; }
		}

		#endregion

		#region Constructors

		public Division(IExpression left, IExpression right)
		{
			_left = left;
			_right = right;
		}

		#endregion

		#region Methods

		public IExpression Calculate(IDictionary<string, IExpression> context)
		{
			IExpression result = new Numeric("0");
			double left = double.Parse(_left.Calculate(context).ToString());
			double right = double.Parse(_right.Calculate(context).ToString());

			result = new Numeric((left / right).ToString());

			return result;
		}

		public override string ToString()
		{
			return String.Concat(_left.ToString(), " / ", _right.ToString());
		}

		#endregion
	}
}

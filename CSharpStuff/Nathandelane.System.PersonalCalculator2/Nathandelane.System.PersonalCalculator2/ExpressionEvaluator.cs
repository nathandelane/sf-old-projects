using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator2
{
	public class ExpressionEvaluator
	{
		#region Fields

		private string _value;

		#endregion

		#region Properties

		public string Value
		{
			get { return _value; }
		}

		#endregion

		#region Constructors

		private ExpressionEvaluator()
		{
			_value = "0";
		}

		#endregion

		#region Methods

		public static ExpressionEvaluator Evaluate(string[] tokens)
		{
			ExpressionEvaluator evaluator = new ExpressionEvaluator();

			return evaluator;
		}

		#endregion
	}
}

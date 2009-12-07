﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Multiplcation : AbstractToken, IArithmeticOperator
	{
		#region fields

		private AbstractToken _left;
		private AbstractToken _right;

		#endregion

		#region Constructors

		public Multiplcation(AbstractToken left, AbstractToken right)
			: base("*")
		{
			_left = left;
			_right = right;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Calculates the result of left multiplied by the right.
		/// </summary>
		/// <returns></returns>
		public AbstractToken Calculate()
		{
			double result = double.Parse(_left.Value, CultureInformation) * double.Parse(_right.Value, CultureInformation);

			return new Number(result.ToString());
		}

		#endregion
	}
}

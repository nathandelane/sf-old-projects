﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public abstract class Token
	{
		#region Fields

		private string _value;

		#endregion

		#region Constructors

		protected Token(string value)
		{
			_value = value;
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			return _value;
		}

		#endregion
	}
}
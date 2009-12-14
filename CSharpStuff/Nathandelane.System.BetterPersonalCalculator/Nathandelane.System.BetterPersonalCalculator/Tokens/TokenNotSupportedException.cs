﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class TokenNotSupportedException : Exception
	{
		#region Constructors

		public TokenNotSupportedException()
			: base()
		{
		}

		public TokenNotSupportedException(string message)
			: base(message)
		{
		}

		#endregion
	}
}

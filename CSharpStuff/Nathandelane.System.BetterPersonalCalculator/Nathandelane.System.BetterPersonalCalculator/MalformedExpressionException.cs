using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class MalformedExpressionException : Exception
	{
		#region Constructors

		public MalformedExpressionException(string message)
			: base(message)
		{
		}

		#endregion
	}
}

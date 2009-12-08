using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class UnrecognizedTokenException : Exception
	{
		#region Constructors

		public UnrecognizedTokenException(string message)
			: base(message)
		{
		}

		#endregion
	}
}

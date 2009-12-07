using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NotANumber : IToken
	{
		#region Properties

		/// <summary>
		/// Gets the value of NotANumber which is String.Empty.
		/// </summary>
		public string Value
		{
			get { return String.Empty; }
		}

		#endregion

		#region Constructors

		public NotANumber()
		{
		}

		#endregion
	}
}

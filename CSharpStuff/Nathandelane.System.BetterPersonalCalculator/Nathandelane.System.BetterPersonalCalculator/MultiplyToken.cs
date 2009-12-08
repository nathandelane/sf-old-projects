using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class MultiplyToken : IToken
	{
		#region Properties

		public string Value
		{
			get { return "*"; }
		}

		#endregion

		#region Constructors

		public MultiplyToken()
		{
		}

		#endregion
	}
}

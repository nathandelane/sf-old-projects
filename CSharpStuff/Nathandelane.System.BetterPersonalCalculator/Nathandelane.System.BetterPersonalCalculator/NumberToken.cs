using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NumberToken : IToken
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

		public NumberToken()
		{
			_value = "0";
		}

		public NumberToken(string value)
		{
			_value = value;
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Nathandelane.System.BetterPersonalCalculator
{
	/// <summary>
	///AbstractToken is the base class for all token classes.
	/// </summary>
	public abstract class AbstractToken
	{
		#region Fields

		private string _value;
		private IFormatProvider _cultureInformation;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the value of this token.
		/// </summary>
		public string Value
		{
			get { return _value; }
		}

		/// <summary>
		/// Gets the current system's culture information.
		/// </summary>
		public IFormatProvider CultureInformation
		{
			get { return _cultureInformation; }
		}

		#endregion

		#region Constructors

		protected AbstractToken(string value)
		{
			_value = value;
			_cultureInformation = CultureInfo.CurrentCulture;
		}

		protected AbstractToken(AbstractToken token)
		{
			_value = token._value;
			_cultureInformation = CultureInfo.CurrentCulture;
		}

		#endregion
	}
}

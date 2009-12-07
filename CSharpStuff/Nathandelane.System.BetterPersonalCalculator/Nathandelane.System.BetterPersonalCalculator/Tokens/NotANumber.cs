using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

		/// <summary>
		/// Gets the match expression of NotANumber which is null.
		/// </summary>
		public Regex MatchExpression
		{
			get { return null; }
		}

		#endregion

		#region Constructors

		public NotANumber()
		{
		}

		#endregion
	}
}

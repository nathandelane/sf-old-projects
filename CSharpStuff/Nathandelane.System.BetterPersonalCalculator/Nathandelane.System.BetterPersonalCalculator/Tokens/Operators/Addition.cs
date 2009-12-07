using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Addition : IToken
	{
		#region Fields

		private static readonly Regex __matchExpression = new Regex("/^+/", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		#endregion

		#region Properties

		public string Value
		{
			get { return "+"; }
		}

		public Regex MatchExpression
		{
			get { return Addition.__matchExpression; }
		}

		#endregion

		#region Constructors

		public Addition()
		{
		}

		#endregion
	}
}

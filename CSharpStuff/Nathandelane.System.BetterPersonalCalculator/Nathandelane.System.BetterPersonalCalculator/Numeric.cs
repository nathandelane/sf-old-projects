using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator.Expressions
{
	public class Numeric : IExpression
	{
		#region Fields

		private static readonly Regex __matchExpression = new Regex("/^(-){0,1}([\\d]+((.){0,1}[\\d]+){0,1}|[\\dA-Fa-f]+(h|H){1}|[0-7]+(o|O){1}|[01]+(b|B){1})/", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private string _value;

		#endregion

		#region Properties

		public Regex MatchExpression
		{
			get { return Numeric.__matchExpression; }
		}

		#endregion

		#region Constructors

		public Numeric(string value)
		{
			_value = value;
		}

		#endregion

		#region Methods

		public string Calculate()
		{
			return _value;
		}

		#endregion
	}
}

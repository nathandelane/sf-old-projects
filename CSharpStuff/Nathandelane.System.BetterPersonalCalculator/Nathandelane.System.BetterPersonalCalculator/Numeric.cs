using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Numeric : AbstractExpression
	{
		#region Fields

		private static readonly string __matchExpression =  "^(-){0,1}([\\d]+((.){0,1}[\\d]+){0,1}|[\\dA-Fa-f]+(h|H){1}|[0-7]+(o|O){1}|[01]+(b|B){1}){1}";

		private string _value;

		#endregion

		#region Properties

		public static string MatchExpression
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

		public override string Calculate()
		{
			return _value;
		}

		#endregion
	}
}

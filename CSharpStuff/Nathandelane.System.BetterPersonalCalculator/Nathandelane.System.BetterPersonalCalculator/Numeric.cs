using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

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

		public string WholePart()
		{
			string value = _value;
			int decimalIndex = -1;

			if ((decimalIndex = value.IndexOf(".", StringComparison.InvariantCultureIgnoreCase)) > -1)
			{
				value = _value.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0];
			}

			return value;
		}

		public string FractionalPart()
		{
			string value = "0";
			int decimalIndex = -1;

			if ((decimalIndex = value.IndexOf(".", StringComparison.InvariantCultureIgnoreCase)) > -1)
			{
				value = _value.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[1];
			}

			return value;
		}

		public string AsHexadecimal()
		{
			return String.Concat(Convert.ToString(long.Parse(WholePart()), 16), "h");
		}

		public string AsOctal()
		{
			return String.Concat(Convert.ToString(long.Parse(WholePart()), 8), "o");
		}

		public string AsBinary()
		{
			return String.Concat(Convert.ToString(long.Parse(WholePart()), 2), "b");
		}

		public override string Calculate()
		{
			return _value;
		}

		#endregion
	}
}

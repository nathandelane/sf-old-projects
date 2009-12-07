using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Number : IToken
	{
		#region Fields

		private static readonly int __binaryBase = 2;
		private static readonly int __octalBase = 8;
		private static readonly int __hexadecimalBase = 16;
		private static readonly Regex __matchExpression = new Regex("/^(-){0,1}([\\d]+((.){0,1}[\\d]+){0,1}|[\\dA-Fa-f]+(h|H){1}|[0-7]+(o|O){1}|[01]+(b|B){1})/", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private string _value;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the value of Number.
		/// </summary>
		public string Value
		{
			get { return _value; }
		}

		/// <summary>
		/// Gets the match expression of Number.
		/// </summary>
		public Regex MatchExpression
		{
			get { return Number.__matchExpression; }
		}

		#endregion

		#region Constructors

		public Number(string value)
		{
			_value = value;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the whole part of a number.
		/// </summary>
		/// <returns></returns>
		public string GetWholePart()
		{
			string wholePart = _value;
			int decimalSeparatorIndex = -1;

			if ((decimalSeparatorIndex = wholePart.IndexOf(".", StringComparison.InvariantCultureIgnoreCase) + 1) > 0)
			{
				wholePart = wholePart.Substring(0, decimalSeparatorIndex);
			}

			return wholePart;
		}

		/// <summary>
		/// Gets the decimal part of a number.
		/// </summary>
		/// <returns></returns>
		public string GetDecimalPart()
		{
			string decimalPart = "0";
			int decimalSeparatorIndex = -1;

			if ((decimalSeparatorIndex = decimalPart.IndexOf(".", StringComparison.InvariantCultureIgnoreCase) + 1) > 0)
			{
				decimalPart = decimalPart.Substring(decimalSeparatorIndex);
			}

			return decimalPart;
		}

		/// <summary>
		/// Gets the number as a hexadecimal value.
		/// </summary>
		/// <returns></returns>
		public string AsHexadecimal()
		{
			return Convert.ToString(long.Parse(GetWholePart()), Number.__hexadecimalBase);
		}

		/// <summary>
		/// Gets the number as a octal value.
		/// </summary>
		/// <returns></returns>
		public string AsOctal()
		{
			return Convert.ToString(long.Parse(GetWholePart()), Number.__octalBase);
		}

		/// <summary>
		/// Gets the number as a binary value.
		/// </summary>
		/// <returns></returns>
		public string AsBinary()
		{
			return Convert.ToString(long.Parse(GetWholePart()), Number.__binaryBase);
		}

		/// <summary>
		/// Gets the number as a binary value.
		/// </summary>
		/// <param name="fieldWidth">The width of the field in which to hold the binary representation.</param>
		/// <returns></returns>
		public string AsBinary(int fieldWidth)
		{
			string formatString = String.Concat(new String('0', fieldWidth), "{{0}}");

			return String.Format(formatString, Convert.ToString(long.Parse(GetWholePart()), Number.__binaryBase));
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.PersonalCalculator
{
	public static class Utility
	{
		public static bool IsInfix(string equation)
		{
			Regex regex = new Regex("^[()+-]*[\\d]+");

			return regex.IsMatch(equation);
		}

		public static bool IsNumeric(string element)
		{
			Regex regex = new Regex("[\\d]+\\.{0,1}[\\d]*");

			return regex.IsMatch(element);
		}

		public static bool IsOperator(string element)
		{
			Regex regex = new Regex("[+-/*()]+");

			return regex.IsMatch(element);
		}

		public static bool IsFunction(string element)
		{
			Regex regex = new Regex("[A-Za-z\\d]");

			return regex.IsMatch(element);
		}

		public static string Add(string left, string right)
		{
			return String.Format("{0}", (double.Parse(left) + double.Parse(right)));
		}

		public static string Sub(string left, string right)
		{
			string result = String.Empty;

			if (left.Equals(String.Empty))
			{
				result = String.Format("{0}", ((-1) * double.Parse(right)));
			}
			else if (right.Equals(String.Empty))
			{
				result = String.Format("{0}", ((-1) * double.Parse(left)));
			}
			else
			{
				result = String.Format("{0}", (double.Parse(left) - double.Parse(right)));
			}

			return result;
		}

		public static string Mul(string left, string right)
		{
			return String.Format("{0}", (double.Parse(left) * double.Parse(right)));
		}

		public static string Div(string left, string right)
		{
			if (right.Equals("0"))
			{
				throw new Exception("Math: Divide by zero exception.");
			}
			else
			{
				return String.Format("{0}", (double.Parse(left) / double.Parse(right)));
			}
		}
	}
}

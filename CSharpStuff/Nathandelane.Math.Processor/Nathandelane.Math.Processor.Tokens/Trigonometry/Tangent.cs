using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.Processor.Tokens
{
	public class Tangent : Function
	{
		private readonly Regex regex = new Regex("^(tan){1}");

		public Tangent()
			: base("tangent")
		{
		}

		public new bool Matches(string str)
		{
			return regex.IsMatch(str);
		}

		public new string FirstMatch(string str)
		{
			return regex.Matches(str)[0].Value;
		}
	}
}

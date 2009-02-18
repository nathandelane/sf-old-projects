using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.Processor.Tokens
{
	public class Tangent : Function
	{
		public Tangent()
			: base("tangent")
		{
		}

		public new bool Matches(string str)
		{
			Regex regex = new Regex("(tan){1}");

			return regex.IsMatch(str);
		}
	}
}

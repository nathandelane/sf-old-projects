using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.Processor.Tokens
{
	public class Cosine : Function
	{
		public Cosine()
			: base("cosine")
		{
		}

		public new bool Matches(string str)
		{
			Regex regex = new Regex("(cos){1}");

			return regex.IsMatch(str);
		}
	}
}

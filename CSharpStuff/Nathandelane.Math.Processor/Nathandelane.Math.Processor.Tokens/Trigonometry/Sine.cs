using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.Processor.Tokens
{
	public class Sine : Function
	{
		public Sine()
			: base("sine")
		{
		}

		public new bool Matches(string str)
		{
			Regex regex = new Regex("(sin){1}");

			return regex.IsMatch(str);
		}
	}
}

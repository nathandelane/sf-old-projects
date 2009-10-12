using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System
{
	public static class Int32Extensions
	{
		public static bool LessThan(this int i, int right)
		{
			return i < right;
		}

		public static bool GreaterThan(this int i, int right)
		{
			return i > right;
		}

		public static bool EqualTo(this int i, int right)
		{
			return i == right;
		}
	}
}

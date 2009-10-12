using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System
{
	public static class CharExtensions
	{
		public static bool IsDigit(this char c)
		{
			return Char.IsDigit(c);
		}

		public static bool IsUpper(this char c)
		{
			return Char.IsUpper(c);
		}

		public static bool IsLower(this char c)
		{
			return Char.IsLower(c);
		}

		public static bool IsLetter(this char c)
		{
		    return Char.IsLetter(c);
		}

		public static char ToUpper(this char c)
		{
			return Char.ToUpper(c);
		}

		public static char ToLower(this char c)
		{
			return Char.ToLower(c);
		}
	}
}

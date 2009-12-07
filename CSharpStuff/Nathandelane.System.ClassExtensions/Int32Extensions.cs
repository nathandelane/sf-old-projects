using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.ClassExtensions
{
	public static class Int32Extensions
	{
		public static bool LessThan(this Int32 i, Int32 right)
		{
			return i < right;
		}

		public static bool LessThan(this Int32 i, Int16 right)
		{
			return i < right;
		}

		public static bool LessThan(this Int32 i, Int64 right)
		{
			return i < right;
		}

		public static bool LessThan(this Int32 i, decimal right)
		{
			return i < right;
		}

		public static bool LessThan(this Int32 i, float right)
		{
			return i < right;
		}

		public static bool GreaterThan(this Int32 i, Int32 right)
		{
			return i > right;
		}

		public static bool GreaterThan(this Int32 i, Int16 right)
		{
			return i > right;
		}

		public static bool GreaterThan(this Int32 i, Int64 right)
		{
			return i > right;
		}

		public static bool GreaterThan(this Int32 i, decimal right)
		{
			return i > right;
		}

		public static bool GreaterThan(this Int32 i, float right)
		{
			return i > right;
		}

		public static bool EqualTo(this Int32 i, Int32 right)
		{
			return i == right;
		}

		public static bool EqualTo(this Int32 i, Int16 right)
		{
			return i == right;
		}

		public static bool EqualTo(this Int32 i, Int64 right)
		{
			return i == right;
		}

		public static bool EqualTo(this Int32 i, decimal right)
		{
			return i == right;
		}

		public static bool EqualTo(this Int32 i, float right)
		{
			return i == right;
		}

		public static short ToInt16(this Int32 i)
		{
			return Convert.ToInt16(i);
		}

		public static short ToShort(this Int32 i)
		{
			return Convert.ToInt16(i);
		}

		public static double ToInt64(this Int32 i)
		{
			return Convert.ToInt64(i);
		}

		public static double ToDouble(this Int32 i)
		{
			return Convert.ToInt64(i);
		}

		public static string ToHexString(this Int32 i)
		{
			return i.ToString("X");
		}
	}
}

using System;

namespace Nathandelane.System.ClassExtensions
{
	public static class CharExtensions
	{
		/// <summary>
		/// Returns whether char is a digit.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static bool IsDigit(this char c)
		{
			return Char.IsDigit(c);
		}

		/// <summary>
		/// Returns whether char is uppercase.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static bool IsUpper(this char c)
		{
			return Char.IsUpper(c);
		}

		/// <summary>
		/// Returns whether char is lowercase.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static bool IsLower(this char c)
		{
			return Char.IsLower(c);
		}

		/// <summary>
		/// Returns whether char is a letter.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static bool IsLetter(this char c)
		{
		    return Char.IsLetter(c);
		}

		/// <summary>
		/// Makes char uppercase.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static char ToUpper(this char c)
		{
			return Char.ToUpper(c);
		}

		/// <summary>
		/// Makes char lowercase.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static char ToLower(this char c)
		{
			return Char.ToLower(c);
		}
	}
}

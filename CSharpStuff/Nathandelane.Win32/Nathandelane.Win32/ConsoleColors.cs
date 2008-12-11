using System;
using System.Runtime.InteropServices;

namespace Nathandelane.Win32
{
	public class ConsoleColors
	{
		[DllImport("kernel32.dll")]
		public static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, int wAttributes);

		[DllImport("kernel32.dll")]
		public static extern IntPtr GetStdHandle(uint nStdHandle);

		private static readonly uint __stdOutputHandle = 0xfffffff5;
		private static readonly IntPtr __consoleHandle = GetStdHandle(__stdOutputHandle);

		public static void SetConsoleColor(byte colorValue)
		{
			int val = (int)colorValue;

			SetConsoleTextAttribute(__consoleHandle, val);
		}
	}
}

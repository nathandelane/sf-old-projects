using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Nathandelane.Net.HttpAnalyzer.Utility
{
	public class ClearConsole
	{
		#region Interop Definitions

		[DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		private static extern int GetStdHandle(int nStdHandle);

		[DllImport("kernel32.dll", EntryPoint = "FillConsoleOutputCharacter", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		private static extern int FillConsoleOutputCharacter(int hConsoleOutput, byte cCharacter, int nLength, Coord dwWriteCoord, ref int lpNumberOfCharsWritten);

		[DllImport("kernel32.dll", EntryPoint = "GetConsoleScreenBufferInfo", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		private static extern int GetConsoleScreenBufferInfo(int hConsoleOutput, ref ConsoleScreenBufferInfo lpConsoleScreenBufferInfo);

		[DllImport("kernel32.dll", EntryPoint = "SetConsoleCursorPosition", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		private static extern int SetConsoleCursorPosition(int hConsoleOutput, Coord dwCursorPosition);

		#endregion

		#region Fields

		private const int StdOutputHandle = -11;
		private const byte Empty = 32;

		private int _consoleHandle;

		#endregion

		#region Structs

		[StructLayout(LayoutKind.Sequential)]
		private struct Coord
		{
			public short X;
			public short Y;
		}

		[StructLayout(LayoutKind.Sequential)]
		struct SmallRect
		{
			public short Left;
			public short Top;
			public short Right;
			public short Bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		struct ConsoleScreenBufferInfo
		{
			public Coord Size;
			public Coord CursorPosition;
			public int Attributes;
			public SmallRect Window;
			public Coord MaximumWindowSize;
		}

		#endregion

		#region Constuctors

		public ClearConsole()
		{
			_consoleHandle = GetStdHandle(StdOutputHandle);
		}

		#endregion

		#region Methods

		public void Clear()
		{
			int writtenChars = 0;
			ConsoleScreenBufferInfo consoleInfo = new ConsoleScreenBufferInfo();
			Coord home;

			home.X = 0;
			home.Y = 0;

			GetConsoleScreenBufferInfo(_consoleHandle, ref consoleInfo);
			FillConsoleOutputCharacter(_consoleHandle, Empty, consoleInfo.Size.X * consoleInfo.Size.Y, home, ref writtenChars);
			SetConsoleCursorPosition(_consoleHandle, home);
		}

		#endregion
	}
}

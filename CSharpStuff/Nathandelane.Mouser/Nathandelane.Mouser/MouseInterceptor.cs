using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;

namespace Nathandelane.Mouser
{
	public class MouseInterceptor
	{
		#region Dll Imports

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook,
			LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
			IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		#endregion

		#region Static Fields

		/// <summary>
		/// Delegate
		/// </summary>
		private static LowLevelMouseProc _proc = HookCallback;

		/// <summary>
		/// Mouse Hook Id
		/// </summary>
		private static IntPtr _hookId;

		/// <summary>
		/// Mouse Hook
		/// </summary>
		private static MSLLHOOKSTRUCT _hookStruct;

		#endregion

		#region Delegates

		public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

		#endregion

		#region Properties

		public IntPtr HookId
		{
			get { return _hookId; }
			set { _hookId = value; }
		}

		public int MouseX
		{
			get { return _hookStruct.pt.x; }
		}

		public int MouseY
		{
			get { return _hookStruct.pt.y; }
		}

		public Point MouseLocation
		{
			get { return new Point(MouseX, MouseY); }
		}

		public LowLevelMouseProc Callback
		{
			get { return _proc; }
			set { _proc = value; }
		}

		#endregion

		#region Private Constants

		private const int WH_MOUSE_LL = 14;

		#endregion

		#region Enums and Structs

		public enum MouseMessages
		{
			WM_LBUTTONDOWN = 0x0201,
			WM_LBUTTONUP = 0x0202,
			WM_MOUSEMOVE = 0x0200,
			WM_MOUSEWHEEL = 0x020A,
			WM_RBUTTONDOWN = 0x0204,
			WM_RBUTTONUP = 0x0205
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int x;
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MSLLHOOKSTRUCT
		{
			public POINT pt;
			public uint mouseData;
			public uint flags;
			public uint time;
			public IntPtr dwExtraInfo;
		}

		#endregion

		#region Private Static Methods

		private static IntPtr SetHook(LowLevelMouseProc proc)
		{
			using (Process curProcess = Process.GetCurrentProcess())
			{
				using (ProcessModule curModule = curProcess.MainModule)
				{
					return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
				}
			}
		}

		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0 &&
				MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
			{
				_hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

				Console.WriteLine(_hookStruct.pt.x + ", " + _hookStruct.pt.y);
			}
			return CallNextHookEx(_hookId, nCode, wParam, lParam);
		}

		#endregion

		#region Initialization and Deletion Methods

		/// <summary>
		/// Use to create an instance of the Mouse Hook
		/// </summary>
		/// <returns></returns>
		public static IntPtr GetMouseInterceptor()
		{
			_hookId = MouseInterceptor.SetHook(_proc);

			return _hookId;
		}

		/// <summary>
		/// use to release a particular instance of a mouse hook
		/// </summary>
		/// <param name="hookId"></param>
		public static void ReleaseMouseInterceptor(IntPtr hookId)
		{
			UnhookWindowsHookEx(hookId);
		}

		#endregion
	}
}

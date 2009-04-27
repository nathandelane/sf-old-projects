using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Nathandelane.IO.CopyFiles
{
	internal class NativeMethods
	{
		#region DLL Imports

		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern int LogonUser(string userName, string domain, string password, int logonType, int logonProvider, ref IntPtr token);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DuplicateToken(IntPtr token, int impersonationLevel, ref IntPtr newToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool RevertToSelf();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool CloseHandle(IntPtr handle);

		#endregion
	}
}

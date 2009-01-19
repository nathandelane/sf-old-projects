using System;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Microsoft.VisualBasic;

namespace Nathandelane.NCommand
{
	public class Impersonator : IDisposable
	{
		#region Public Methods

		public Impersonator(string domainName, string userName, string password)
		{
			try
			{
				ImpersonateValidUser(userName, domainName, password);
			}
			catch (Exception ex)
			{
				Console.WriteLine(String.Format("Exception Caught! {0}; {1}; Inner Exception Caught: {2}", ex.Message, ex.StackTrace, ex.InnerException));
				Console.WriteLine("If you included the domain in your user name in the XML settings file, then please remove it.");
				throw ex;
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			UndoImpersonation();
		}

		#endregion

		#region P/Invoke.

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool RevertToSelf();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool CloseHandle(IntPtr handle);

		private const int LOGON32_LOGON_INTERACTIVE = 2;
		private const int LOGON32_PROVIDER_DEFAULT = 0;

		#endregion

		#region Private Methods

		private void ImpersonateValidUser(string userName, string domain, string password)
		{
			WindowsIdentity tempWindowsIdentity = null;
			IntPtr token = IntPtr.Zero;
			IntPtr tokenDuplicate = IntPtr.Zero;

			try
			{
				if (RevertToSelf())
				{
					if (LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
					{
						if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
						{
							tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
							impersonationContext = tempWindowsIdentity.Impersonate();
						}
						else
						{
							throw new Win32Exception(Marshal.GetLastWin32Error());
						}
					}
					else
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}
				}
				else
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				if (token != IntPtr.Zero)
				{
					CloseHandle(token);
				}
				if (tokenDuplicate != IntPtr.Zero)
				{
					CloseHandle(tokenDuplicate);
				}
			}
		}

		/// <summary>
		/// Reverts the impersonation.
		/// </summary>
		private void UndoImpersonation()
		{
			if (impersonationContext != null)
			{
				impersonationContext.Undo();
			}
		}

		private WindowsImpersonationContext impersonationContext = null;

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Nathandelane.IO.CopyFiles
{
	internal class Impersonator : IDisposable
	{
		#region Fields

		private const int LOGON32_LOGON_INTERACTIVE = 2;
		private const int LOGON32_PROVIDER_DEFAULT = 0;

		private bool _isDisposed;
		private WindowsImpersonationContext _impersonationContext;
		private WindowsIdentity _identity;
		private IntPtr _token;
		private IntPtr _tokenDuplicate;

		#endregion

		#region Properties

		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		#endregion

		#region Constructors

		public Impersonator(string userName, string domainName, string password)
		{
			_token = IntPtr.Zero;
			_tokenDuplicate = IntPtr.Zero;

			ImpersonateValidUser(userName, domainName, password);
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (!IsDisposed)
			{
				Disposing(true);
			}
		}

		#endregion

		#region Private Methods

		private void ImpersonateValidUser(string userName, string domain, string password)
		{
			if (NativeMethods.RevertToSelf())
			{
				if (NativeMethods.LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref _token) != 0)
				{
					if (NativeMethods.DuplicateToken(_token, LOGON32_LOGON_INTERACTIVE, ref _tokenDuplicate) != 0)
					{
						_identity = new WindowsIdentity(_tokenDuplicate);
						_impersonationContext = _identity.Impersonate();
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

		private void UndoImpersonation()
		{
			if (_impersonationContext != null)
			{
				_impersonationContext.Undo();
			}
		}

		private void Disposing(bool dispose)
		{
			if (_token != IntPtr.Zero)
			{
				NativeMethods.CloseHandle(_token);
			}

			if (_tokenDuplicate != IntPtr.Zero)
			{
				NativeMethods.CloseHandle(_tokenDuplicate);
			}

			UndoImpersonation();

			_isDisposed = true;
		}

		#endregion
	}
}

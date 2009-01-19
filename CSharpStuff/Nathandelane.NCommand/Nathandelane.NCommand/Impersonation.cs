using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Security.Permissions;
using System.Text;

namespace Nathandelane.NCommand
{
	internal class Impersonation : IDisposable
	{
		private static bool _isDisposed;

		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		private Impersonation()
		{
			_isDisposed = false;
		}

		public static Impersonation ImpersonateAs(string userName, string domainName, string userPassword)
		{
			Impersonation impersonation = new Impersonation();



			return impersonation;
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (!IsDisposed)
			{
				_isDisposed = true;
			}
		}

		#endregion
	}
}

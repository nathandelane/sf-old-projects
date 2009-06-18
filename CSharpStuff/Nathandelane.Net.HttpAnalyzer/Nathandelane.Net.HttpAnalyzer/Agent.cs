using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Net.HttpAnalyzer
{
	public class Agent : IDisposable
	{
		#region Fields

		private bool _isDisposed;

		#endregion

		#region Properties

		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		#endregion

		#region Constructors

		public Agent(Uri uri)
		{
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (!_isDisposed)
			{
				_isDisposed = true;
			}
		}

		#endregion
	}
}

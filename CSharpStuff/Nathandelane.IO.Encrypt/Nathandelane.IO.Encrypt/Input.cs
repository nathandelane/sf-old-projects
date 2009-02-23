using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Nathandelane.IO.Encrypt
{
	internal class Input : IDisposable
	{
		#region Fields

		private static readonly int __requiredNumberOfArguments = 3;
		private static readonly int __maxNumberOfArguments = 3;

		private static bool _isDisposed;
		private static bool _isValid;
		private static int _numberOfArguments;

		#endregion

		#region Properties

		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		public int Length
		{
			get { return _numberOfArguments; }
		}

		public bool IsValid
		{
			get { return _isValid; }
		}

		#endregion

		#region Constructors

		private Input()
		{
			_isDisposed = false;
			_isValid = false;
		}

		#endregion

		#region Static Methods

		internal static Input CheckAndSanitizeInput(string[] args)
		{
			if (args.Length >= Input.__requiredNumberOfArguments && args.Length <= Input.__maxNumberOfArguments)
			{
				_isValid = true;
			}

			return new Input();
		}

		internal static void Help()
		{
			Console.WriteLine("Usage: {0}", Assembly.GetEntryAssembly().GetName().Name);
		}

		#endregion

		#region Private Methods

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

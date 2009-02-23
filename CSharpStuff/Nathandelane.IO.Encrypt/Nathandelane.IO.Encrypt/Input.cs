using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nathandelane.IO.Encrypt
{
	internal class Input : IDisposable
	{
		#region Fields

		private static readonly int __requiredNumberOfArguments = 1;
		private static readonly int __maxNumberOfArguments = 3;

		private static bool _isDisposed;
		private static bool _isValid;
		private static int _numberOfArguments;
		private static string _filePath;
		private static string _key;
		private static EncryptionAlgorithm _algorithm;

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

		#region Explicit Properties

		public string FilePath
		{
			get { return _filePath; }
		}

		public string Key
		{
			get { return _key; }
		}

		public EncryptionAlgorithm Algorithm
		{
			get { return _algorithm; }
		}

		#endregion

		#endregion

		#region Constructors

		private Input()
		{
			_isDisposed = false;
		}

		#endregion

		#region Static Methods

		internal static Input CheckAndSanitizeInput(string[] args)
		{
			_isValid = false;

			if (args.Length >= Input.__requiredNumberOfArguments && args.Length <= Input.__maxNumberOfArguments)
			{
				_isValid = true;
				_numberOfArguments = args.Length;

				if (File.Exists(args[0]))
				{
					_isValid = true;
					_filePath = args[0];

					if (args.Length > Input.__requiredNumberOfArguments)
					{
						_isValid = ParseOtherArguments(args);
					}
				}
				else
				{
					_isValid = false;
				}
			}

			return new Input();
		}

		internal static void Help()
		{
			Console.WriteLine("Usage: {0} File-Path [Encryption-Algorithm [Key]]", Assembly.GetEntryAssembly().GetName().Name);
		}

		private static bool ParseOtherArguments(string[] args)
		{
			bool resultOfParse = false;

			if (Enum.IsDefined(typeof(EncryptionAlgorithm), args[1]))
			{
				_algorithm = (EncryptionAlgorithm)Enum.Parse(typeof(EncryptionAlgorithm), args[1], true);
				resultOfParse = true;

				if (args.Length >= 3)
				{
					_key = args[2];
				}
				else
				{
					using (StreamReader reader = new StreamReader(_filePath))
					{
						_key = reader.ReadToEnd();
					}
				}
			}
			else
			{
				throw new ArgumentException("Encryption algorithm");
			}

			return resultOfParse;
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

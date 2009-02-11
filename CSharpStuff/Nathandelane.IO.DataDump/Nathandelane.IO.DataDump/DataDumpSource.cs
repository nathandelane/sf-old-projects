using System;
using System.IO;
using System.Text;

namespace Nathandelane.IO.DataDump
{
	public enum DataDumpSourceType
	{
		File,
		Random,
		Zero
	}

	public class DataDumpSource : IDisposable
	{
		#region Fields

		private static int __defaultNumberOfBytes = 4096;
		private DataDumpSourceType _sourceType;
		private byte[] _bytes;
		private int _numberOfBytes;
		private string _filePath;
		private bool _isDisposed;

		#endregion

		#region Properties

		public DataDumpSourceType SourceType
		{
			get { return _sourceType; }
		}

		public byte[] Bytes
		{
			get { return _bytes; }
		}

		public int NumberOfBytes
		{
			get
			{
				int numBytes = 0;

				numBytes = ((_numberOfBytes <= 0) ? __defaultNumberOfBytes : _numberOfBytes);

				return numBytes;
			}
		}

		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		#endregion

		#region Constructors

		public DataDumpSource(DataDumpSourceType sourceType, int numberOfBytes)
		{
			if (sourceType == DataDumpSourceType.File)
			{
				throw new ArgumentException("sourceType may not be file when no filePath is specified.");
			}

			_sourceType = sourceType;
			_numberOfBytes = numberOfBytes;
			_isDisposed = false;

			GetBytes();
		}

		public DataDumpSource(string filePath, int numberOfBytes)
		{
			_numberOfBytes = numberOfBytes;
			_sourceType = DataDumpSourceType.File;
			_filePath = filePath;
			_isDisposed = false;

			GetBytes();
		}

		#endregion

		#region Private Methods

		private void GetBytes()
		{
			if (SourceType == DataDumpSourceType.Random)
			{
				_bytes = GenerateRandomBytes();
			}
			else if (SourceType == DataDumpSourceType.Zero)
			{
				_bytes = new byte[NumberOfBytes];
			}
			else if (SourceType == DataDumpSourceType.File)
			{
				using (StreamReader reader = new StreamReader(_filePath))
				{
					if (_numberOfBytes <= 0)
					{
						_bytes = ASCIIEncoding.ASCII.GetBytes(reader.ReadToEnd());
					}
					else
					{
						char[] buffer = new char[NumberOfBytes];
						reader.Read(buffer, 0, NumberOfBytes);
						_bytes = ASCIIEncoding.ASCII.GetBytes(buffer);
					}
				}
			}
		}

		private byte[] GenerateRandomBytes()
		{
			byte[] bytes = new byte[NumberOfBytes];

			Random random = new Random((int)DateTime.Now.Ticks);
			random.NextBytes(bytes);

			return bytes;
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

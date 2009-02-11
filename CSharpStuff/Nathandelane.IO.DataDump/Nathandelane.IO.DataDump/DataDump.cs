using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.DataDump
{
	public class DataDump : IDisposable
	{
		#region Fields

		private DataDumpSource _source;
		private string _destination;
		private MemoryStream _data;
		private bool _isDisposed;

		#endregion

		#region Properties

		public MemoryStream Data
		{
			get
			{
				if (_data == null)
				{
					_data = new MemoryStream(_source.Bytes);
				}

				return _data;
			}
		}

		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		#endregion

		#region Constructor

		public DataDump(DataDumpSource source, string destination)
		{
			_source = source;
			_destination = destination;
			_isDisposed = false;
		}

		#endregion

		#region Public Methods

		public void Dump()
		{
			using (StreamWriter writer = new StreamWriter(_destination))
			{
				writer.Write(ASCIIEncoding.ASCII.GetChars(_source.Bytes));
			}
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

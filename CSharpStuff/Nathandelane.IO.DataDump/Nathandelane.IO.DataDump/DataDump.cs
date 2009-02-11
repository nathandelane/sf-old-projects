using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.DataDump
{
	public class DataDump
	{
		#region Fields

		private DataDumpSource _source;
		private string _destination;
		private MemoryStream _data;

		#endregion

		#region Constructor

		public DataDump(DataDumpSource source, string destination)
		{
			_source = source;
			_destination = destination;
		}

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
	}
}

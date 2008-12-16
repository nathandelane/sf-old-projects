using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.HexEditor
{
	public class HexFile
	{
		private List<byte> _fileBytes;
		private FileInfo _info;
		private Exception _hexFileException;

		public byte this[int index]
		{
			get { return _fileBytes[index]; }
		}

		public FileInfo Info
		{
			get { return _info; }
		}

		public Exception HexFileException
		{
			get { return _hexFileException; }
		}

		public HexFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				_hexFileException = new FileNotFoundException("filePath");
				throw _hexFileException;
			}
			else
			{
				_info = new FileInfo(filePath);

				using (StreamReader reader = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
				{
					string data = reader.ReadToEnd();
					byte[] bytes = ASCIIEncoding.ASCII.GetBytes(data.ToCharArray());
					_fileBytes = new List<byte>();

					foreach (byte b in bytes)
					{
						_fileBytes.Add(b);
					}
				}
			}
		}
	}
}

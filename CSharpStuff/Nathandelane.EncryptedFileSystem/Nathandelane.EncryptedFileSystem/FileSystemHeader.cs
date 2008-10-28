using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Nathandelane.EncryptedFileSystem
{
	public sealed class FileSystemHeader
	{
		private int _fileSystemMap;
		private string _blockName;

		public FileSystemHeader(string blockName)
		{
			_blockName = blockName;
			_fileSystemMap = -1;
		}

		/// <summary>
		/// Use SHA-1 to get name of map file from local directory.
		/// 0.0.1-Nathandelane.EncryptedFileSystem.Resources.Map.txt
		/// </summary>
		public int FileSystemMap
		{
			get
			{
				if (_fileSystemMap == -1)
				{
					HashAlgorithm hash = new SHA1Managed();
					_fileSystemMap = Convert.ToInt32(hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes("0.0.1-Nathandelane.EncryptedFileSystem.Resources.Map.txt")));
				}

				return _fileSystemMap;
			}
		}
	}
}

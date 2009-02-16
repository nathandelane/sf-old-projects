using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.FileTool
{
	public class Inode
	{
		#region Fields

		private FileSystemInfo _fsObject;

		#endregion

		#region Properties

		public FileAttributes Attributes
		{
			get { return _fsObject.Attributes; }
			set { _fsObject.Attributes = value; }
		}

		public DateTime CreationTime
		{
			get { return _fsObject.CreationTime; }
			set { _fsObject.CreationTime = value; }
		}

		public DateTime LastAccessTime
		{
			get { return _fsObject.LastAccessTime; }
			set { _fsObject.LastAccessTime = value; }
		}

		public DateTime LastWriteTime
		{
			get { return _fsObject.LastWriteTime; }
			set { _fsObject.LastWriteTime = value; }
		}

		public Type ObjectType
		{
			get { return _fsObject.GetType(); }
		}

		#endregion

		#region Constructors

		public Inode(FileInfo fileObject)
		{
			_fsObject = fileObject;
		}

		public Inode(DirectoryInfo directoryObject)
		{
			_fsObject = directoryObject;
		}

		public Inode(FileSystemInfo fileSystemObject)
		{
			_fsObject = fileSystemObject;
		}

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return String.Concat(
				String.Format("Attributes: {0}\n", Attributes),
				String.Format("Creation Time: {0}\n", CreationTime),
				String.Format("Last Access Time: {0}\n", LastAccessTime),
				String.Format("Last Write Time: {0}\n", LastWriteTime)
				);
		}

		#endregion
	}
}

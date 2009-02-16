using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.FileTool
{
	public enum InodeType
	{
		File,
		Directory
	}

	public class Inode : IDisposable
	{
		#region Fields

		private FileSystemInfo _fsObject;
		private bool _isDisposed;
		private InodeType _type;

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

		public InodeType ObjectType
		{
			get { return _type; }
		}

		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		#endregion

		#region Constructors

		public Inode(FileInfo fileObject)
		{
			_isDisposed = false;
			_fsObject = fileObject;
			_type = InodeType.File;
		}

		public Inode(DirectoryInfo directoryObject)
		{
			_isDisposed = false;
			_fsObject = directoryObject;
			_type = InodeType.Directory;
		}

		public Inode(FileSystemInfo fileSystemObject)
		{
			_isDisposed = false;
			_fsObject = fileSystemObject;
			_type = fileSystemObject is FileInfo ? InodeType.File : InodeType.Directory;
		}

		#endregion

		#region Public Methods

		public void Query(List<InodeQuery> query)
		{
			if (query.Count > 0)
			{
				foreach (InodeQuery qItem in query)
				{
					if (qItem.Type == QueryType.GetAttributes)
					{
						Console.WriteLine("Attributes: {0}", Attributes);
					}
					else if (qItem.Type == QueryType.SetAttributes)
					{
						Attributes = (FileAttributes)qItem.SetValue;
						Console.WriteLine("Attributes: {0}", Attributes);
					}
					else if (qItem.Type == QueryType.GetCreationTime)
					{
						Console.WriteLine("Creation Time: {0}", CreationTime);
					}
					else if (qItem.Type == QueryType.SetCreationTime)
					{
						CreationTime = DateTime.Parse((string)qItem.SetValue);
						Console.WriteLine("Creation Time: {0}", CreationTime);
					}
					else if (qItem.Type == QueryType.GetLastAccessTime)
					{
						Console.WriteLine("Creation Time: {0}", LastAccessTime);
					}
					else if (qItem.Type == QueryType.SetLastAccessTime)
					{
						LastAccessTime = DateTime.Parse((string)qItem.SetValue);
						Console.WriteLine("Creation Time: {0}", LastAccessTime);
					}
					else if (qItem.Type == QueryType.GetLastWriteTime)
					{
						Console.WriteLine("Creation Time: {0}", LastWriteTime);
					}
					else if (qItem.Type == QueryType.SetLastWriteTime)
					{
						LastWriteTime = DateTime.Parse((string)qItem.SetValue);
						Console.WriteLine("Creation Time: {0}", LastWriteTime);
					}
				}
			}
			else
			{
				Console.WriteLine(this);
			}
		}

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

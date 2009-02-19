using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.ListSegments
{
	public class ListSegments : IDisposable
	{
		#region Fields

		private string _directory;
		private string[] _filters;
		private bool _isDisposed;

		#endregion

		#region Properties

		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		#endregion

		#region Constructors

		public ListSegments()
		{
			_directory = @".\";
			_filters = new string[0];
			_isDisposed = false;
		}

		public ListSegments(string directory)
		{
			_directory = directory;
			_filters = new string[0];
			_isDisposed = false;
		}

		public ListSegments(string[] filters)
		{
			_directory = @".\";
			_filters = filters;
			_isDisposed = false;
		}

		public ListSegments(string directory, string[] filters)
		{
			_directory = directory;
			_filters = filters;
			_isDisposed = false;
		}

		#endregion

		#region Public Methods

		public void Run()
		{
			List<FileInfo> files = new List<FileInfo>();
			DirectoryInfo directoryInfo = new DirectoryInfo(_directory);

			if (_filters.Length > 0)
			{
				foreach (string filter in _filters)
				{
					FileInfo[] filteredFiles = directoryInfo.GetFiles(filter);
					files.AddRange(filteredFiles.AsEnumerable<FileInfo>());
				}
			}
			else
			{
				FileInfo[] allFiles = directoryInfo.GetFiles();
				files.AddRange(allFiles.AsEnumerable<FileInfo>());
			}
		}

		#endregion

		#region Private Methods

		private void DisplayFiles(IList<FileInfo> files)
		{
			foreach (FileInfo nextFile in files)
			{
				string output = String.Format("{0}", nextFile.Name);
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

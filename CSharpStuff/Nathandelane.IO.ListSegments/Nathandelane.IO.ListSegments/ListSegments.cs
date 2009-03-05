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
		private IList<string> _filters;
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
			_filters = new List<string>();
			_isDisposed = false;
		}

		public ListSegments(string directory)
		{
			_directory = directory;
			_filters = new List<string>();
			_isDisposed = false;
		}

		public ListSegments(IList<string> filters)
		{
			_directory = @".\";
			_filters = filters;
			_isDisposed = false;
		}

		public ListSegments(string directory, IList<string> filters)
		{
			_directory = directory;
			_filters = filters;
			_isDisposed = false;
		}

		#endregion

		#region Public Methods

		public void Run()
		{
			List<FileSystemInfo> segments = new List<FileSystemInfo>();
			DirectoryInfo directoryInfo = new DirectoryInfo(_directory);

			if (_filters.Count > 0)
			{
				foreach (string filter in _filters)
				{
					FileInfo[] filteredFiles = directoryInfo.GetFiles(filter);
					segments.AddRange(filteredFiles.AsEnumerable<FileSystemInfo>());

					DirectoryInfo[] filteredDirectories = directoryInfo.GetDirectories(filter);
					segments.AddRange(filteredDirectories.AsEnumerable<FileSystemInfo>());
				}
			}
			else
			{
				FileSystemInfo[] allFiles = directoryInfo.GetFiles();
				segments.AddRange(allFiles.AsEnumerable<FileSystemInfo>());

				FileSystemInfo[] allDirectories = directoryInfo.GetDirectories();
				segments.AddRange(allDirectories.AsEnumerable<FileSystemInfo>());
			}

			DisplayFiles(segments);
		}

		#endregion

		#region Private Methods

		private void DisplayFiles(IList<FileSystemInfo> segments)
		{
			string output = String.Empty;

			DisplayDefaultDirectories();

			foreach (FileSystemInfo nextSegment in segments)
			{
				Console.WriteLine("{0,-13} {1} {2} {3,-22}", FormatAttributes(nextSegment.Attributes, (nextSegment is DirectoryInfo)), FormatDateTime(nextSegment.LastWriteTime), FormatFileSize(nextSegment), nextSegment.Name);
			}
		}

		private void DisplayDefaultDirectories()
		{
			DirectoryInfo cwd = new DirectoryInfo(".");
			DirectoryInfo parent = new DirectoryInfo("..");
			
			Console.WriteLine("{0,-13} {1} {2} {3}", FormatAttributes(cwd.Attributes, cwd is DirectoryInfo), FormatDateTime(cwd.LastWriteTime), FormatFileSize(cwd), ".");
			Console.WriteLine("{0,-13} {1} {2} {3}", FormatAttributes(parent.Attributes, parent is DirectoryInfo), FormatDateTime(parent.LastWriteTime), FormatFileSize(parent), "..");
		}
		
		private string FormatFileSize(FileSystemInfo segment)
		{
			string size = String.Format("{0,20", String.Empty);
			
			if(segment is FileInfo)
			{
				size = String.Format("{0,20:0,0}  ", ((FileInfo)segment).Length);
			}
			
			return size;
		}

		private string FormatDateTime(DateTime dateTime)
		{
			string result = String.Format("{0} {1} {2,2}:{3,2}", GetMonthName(dateTime.Month), PadNumber(dateTime.Day, 2), PadNumber(dateTime.TimeOfDay.Hours, 2), PadNumber(dateTime.Minute, 2));

			return result;
		}

		private string GetMonthName(int month)
		{
			month--;

			return (new List<string>()
			{
				"Jan", "Feb", "Mar", "Apr", "May", "Jun",
				"Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
			})[month];
		}

		private string PadNumber(int number, int padding)
		{
			string paddedNumber = String.Format("{0}", number);

			for (int counter = paddedNumber.Length; counter < padding; counter++)
			{
				paddedNumber = String.Concat("0", paddedNumber);
			}

			return paddedNumber;
		}

		private string FormatAttributes(FileAttributes attributes, bool isDirectory)
		{
			string result = String.Empty;

			if (isDirectory)
			{
				result = String.Concat(result, " d");
			}
			else
			{
				result = String.Concat(result, " -");
			}

			return result;
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

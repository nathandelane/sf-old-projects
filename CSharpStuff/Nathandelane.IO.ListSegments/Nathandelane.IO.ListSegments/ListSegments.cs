﻿using System;
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
					FileSystemInfo[] filteredFiles = directoryInfo.GetFiles(filter);
					segments.AddRange(filteredFiles.AsEnumerable<FileSystemInfo>());

					FileSystemInfo[] filteredDirectories = directoryInfo.GetDirectories(filter);
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
				Console.WriteLine("{0,-13} {1} {2}", FormatAttributes(nextSegment.Attributes, (nextSegment is DirectoryInfo)), FormatDateTime(nextSegment.LastWriteTime), nextSegment.Name);
			}
		}

		private void DisplayDefaultDirectories()
		{
			Console.WriteLine("{0,-13} {1} {2}", FormatAttributes((new DirectoryInfo(_directory)).Attributes, ((new DirectoryInfo(_directory)) is DirectoryInfo)), FormatDateTime((new DirectoryInfo(_directory)).LastWriteTime), ".");
			Console.WriteLine("{0,-13} {1} {2}", FormatAttributes((new DirectoryInfo("..")).Attributes, ((new DirectoryInfo("..")) is DirectoryInfo)), FormatDateTime((new DirectoryInfo("..")).LastWriteTime), "..");
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
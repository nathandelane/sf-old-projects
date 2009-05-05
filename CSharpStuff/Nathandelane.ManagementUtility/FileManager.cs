using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;

namespace Nathandelane.ManagementUtility
{
	internal class FileManager : Manager
	{
		#region Fields

		private static long __id = 0;

		private FileInfo _file;
		private long _fileSize;
		private DateTime _modifiedDate;
		private bool _wasModified;
		private Timer _timer;

		#endregion

		#region Properties

		public bool FileExists
		{
			get
			{
				bool result = false;

				if (_file != null)
				{
					if (File.Exists(_file.FullName))
					{
						result = true;
					}
				}

				return result;
			}
		}

		public TimeSpan MonitorInterval { get; set; }

		public bool CheckModifiedDate { get; set; }

		public bool WasModified
		{
			get { return _wasModified; }
		}

		#endregion

		#region Constructors

		public FileManager(string name, string filePath)
		{
			FileManager.__id++;

			_file = new FileInfo(filePath);
			_wasModified = false;

			if (this.FileExists)
			{
				_fileSize = _file.Length;
				_modifiedDate = _file.LastWriteTime;
			}

			this.Name = name;
			this.Type = ManagerType.File;
			this.MonitorInterval = new TimeSpan(0, 0, 30);
			this.CheckModifiedDate = true;
		}

		#endregion

		#region Override Methods

		public override void Run()
		{
			_timer = new Timer(MonitorInterval.Milliseconds);
			_timer.Elapsed += new ElapsedEventHandler(CheckState);
			_timer.Start();
		}

		public override void Stop()
		{
			_timer.Stop();
		}

		#endregion

		#region Private Methods

		private void CheckState(object source, ElapsedEventArgs e)
		{
			if (this.CheckModifiedDate && _modifiedDate != null)
			{
				DateTime currentModifiedDate = _file.LastWriteTime;

				if (currentModifiedDate.CompareTo(_modifiedDate) != 0)
				{
					_wasModified = true;
					_modifiedDate = currentModifiedDate;
				}
			}
		}

		#endregion
	}
}

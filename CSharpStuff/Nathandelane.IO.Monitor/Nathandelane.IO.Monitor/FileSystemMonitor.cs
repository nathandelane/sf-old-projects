using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Monitor
{
	public class FileSystemMonitor : MonitorBase
	{
		#region Fields

		private FileSystemMonitorType _type;
		private NotifyFilters _filters;
		private FileSystemWatcher _watcher;

		#endregion

		#region Properties

		public FileSystemMonitorType Type
		{
			get { return _type; }
			set { _type = value; }
		}

		public NotifyFilters Filters
		{
			get { return _filters; }
			set { _filters = value; }
		}

		public FileSystemWatcher Watcher
		{
			get { return _watcher; }
			set { _watcher = value; }
		}

		#endregion

		#region Constructors

		public FileSystemMonitor(string path)
			: base(path)
		{
			_watcher = new FileSystemWatcher(path);
		}

		public FileSystemMonitor(string name, string path)
			: base(name)
		{
			_watcher = new FileSystemWatcher(path);
		}

		public FileSystemMonitor(string name, string path, FileSystemMonitorType type)
			: base(name)
		{
			_type = type;
			_watcher = new FileSystemWatcher(path);
		}

		public FileSystemMonitor(string name, string path, FileSystemMonitorType type, string filters)
			: base(name)
		{
			_type = type;
			_watcher = new FileSystemWatcher(path, filters);
		}

		public FileSystemMonitor(string name, string path, FileSystemMonitorType type, NotifyFilters filters)
			: base(name)
		{
			_type = type;
			_filters = filters;
			_watcher = new FileSystemWatcher(path);
			_watcher.NotifyFilter = filters;
		}

		#endregion

		#region Public Methods

		public override string ToString()
		{
			return base.Name;
		}

		#endregion
	}
}

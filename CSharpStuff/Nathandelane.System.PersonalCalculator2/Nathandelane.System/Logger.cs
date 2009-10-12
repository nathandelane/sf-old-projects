using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace Nathandelane.System
{
	public static class Logger
	{
		#region Fields

		private static readonly EventLog _eventLog = new EventLog();

		#endregion

		#region Methods

		public static void Error(string message)
		{
			_eventLog.Source = Assembly.GetEntryAssembly().GetName().Name;
			_eventLog.WriteEntry(message, EventLogEntryType.Error);
		}

		public static void Information(string message)
		{
			_eventLog.Source = Assembly.GetEntryAssembly().GetName().Name;
			_eventLog.WriteEntry(message, EventLogEntryType.Information);
		}

		public static void Warning(string message)
		{
			_eventLog.Source = Assembly.GetEntryAssembly().GetName().Name;
			_eventLog.WriteEntry(message, EventLogEntryType.Warning);
		}

		#endregion
	}
}

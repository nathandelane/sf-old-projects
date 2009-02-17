using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Monitor
{
	public class MonitorCollection : List<MonitorBase>
	{
		#region Public Methods

		public void Replace(MonitorBase oldMonitor, MonitorBase newMonitor)
		{
			int index = this.IndexOf(oldMonitor);
			this[index] = newMonitor;
		}

		#endregion
	}
}

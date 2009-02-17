using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nathandelane.IO.Monitor
{
	public abstract class MonitorBase
	{
		#region Fields

		private static int __id = 0;
		private string _name;

		#endregion

		#region Properties

		public string Name
		{
			get { return _name; }
		}

		public int Id
		{
			get { return MonitorBase.__id; }
		}

		#endregion

		#region Constructors

		public MonitorBase(string name)
		{
			MonitorBase.__id++;
			_name = name;
		}

		#endregion
	}
}

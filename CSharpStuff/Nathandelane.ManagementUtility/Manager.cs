using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ManagementUtility
{
	public abstract class Manager
	{
		#region Properties

		public string Name { get; set; }

		public ManagerType Type { get; set; }

		#endregion

		#region Abstract Methods

		public abstract void Run();

		public abstract void Stop();

		#endregion
	}
}

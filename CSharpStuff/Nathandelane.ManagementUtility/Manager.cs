using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ManagementUtility
{
	internal abstract class Manager
	{
		#region Fields

		private static long __id = 0;

		#endregion

		#region Properties

		public string Name { get; set; }

		public ManagerType Type { get; set; }

		#endregion

		#region Abstract Methods

		public abstract void Run();

		#endregion
	}
}

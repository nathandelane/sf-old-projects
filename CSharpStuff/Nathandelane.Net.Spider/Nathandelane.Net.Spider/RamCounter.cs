using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	internal class RamCounter
	{
		#region Fields

		private static PerformanceCounter __ramCounter = new PerformanceCounter("Memory", "Available MBytes");

		#endregion

		#region Properties

		public static float MegabytesAvailable
		{
			get { return __ramCounter.NextValue(); }
		}

		#endregion
	}
}

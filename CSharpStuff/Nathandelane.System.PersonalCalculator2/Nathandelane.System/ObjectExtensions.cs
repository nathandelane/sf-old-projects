using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// Gets whether object is null.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool IsNull(this object o)
		{
			return o == null;
		}
	}
}

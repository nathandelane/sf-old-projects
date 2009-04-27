using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.CopyFiles
{
	public class CopyFiles
	{
		public static CopyResult Copy(string source, string destination)
		{
			CopyResult result = CopyResult.Default;

			if (source.Contains(',') && destination.Contains(','))
			{
				result = CopyMultiple(source.Split(new char[] { ',' }), destination.Split(new char[] { ',' }));
			}

			return result;
		}

		private static CopyResult CopyMultiple(string[] sources, string[] destinations)
		{
			CopyResult result = CopyResult.Default;

			return result;
		}
	}
}

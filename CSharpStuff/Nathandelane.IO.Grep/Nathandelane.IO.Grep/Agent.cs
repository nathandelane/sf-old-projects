using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nathandelane.IO.Grep
{
	public class Agent
	{
		#region Fields

		private static List<string> __filesNotExisting = new List<string>();

		#endregion

		#region Properties

		public static string[] FilesNotExisting
		{
			get { return __filesNotExisting.ToArray(); }
		}

		#endregion

		#region Methods

		public static bool FilesExist(string fileName)
		{
			bool result = true;

			if (!File.Exists(fileName))
			{
				result = false;
				__filesNotExisting.Add(fileName);
			}

			return result;
		}

		public static bool FilesExist(string[] fileNames)
		{
			bool result = true;

			foreach (string fileName in fileNames)
			{
				if (!File.Exists(fileName))
				{
					result = false;
					__filesNotExisting.Add(fileName);
				}
			}

			return result;
		}

		#endregion
	}
}

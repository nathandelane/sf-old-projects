using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.WebGet
{
	public class Logger
	{
		private static object __lockObject = new object();

		public static void LogMessage(string message)
		{
			lock (__lockObject)
			{
				using (StreamWriter writer = new StreamWriter(new FileStream("WGet.log", FileMode.Append)))
				{
					writer.WriteLine(message);
				}
			}
		}
	}
}

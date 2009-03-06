using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.WebGet
{
	static class Logger
	{
		public static void LogMessage(string message)
		{
			using (StreamWriter writer = new StreamWriter(new FileStream("WGet.log", FileMode.Append)))
			{
				writer.WriteLine(message);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nathandelane.Net.Spider.WebCrawler
{
	internal class Logger
	{
		#region Fields

		private static string __logFileName = "SpiderLog.csv";

		#endregion

		#region Static Methods

		public static void InitializeLogFile(string header)
		{
			if (File.Exists(__logFileName))
			{
				File.Delete(__logFileName);
			}

			LogToFile(header);
		}

		public static void LogMessage(string message, LoggingType loggingType)
		{
			switch (loggingType)
			{
				case LoggingType.Both:
					LogToBoth(message);
					break;
				case LoggingType.File:
					LogToFile(message);
					break;
				case LoggingType.Screen:
					LogToScreen(message);
					break;
			}
		}

		public static void LogToFile(string message)
		{
			using (StreamWriter writer = new StreamWriter(new FileStream(__logFileName, FileMode.Append)))
			{
				writer.WriteLine(message);
			}
		}

		public static void LogToScreen(string message)
		{
			Console.WriteLine(message);
		}

		public static void LogToBoth(string message)
		{
			LogToFile(message);
			LogToScreen(message);
		}

		#endregion
	}
}

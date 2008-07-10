using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vehix.QA.SetHostFile
{
	public class Logger
	{
		StreamWriter writer;

		public Logger(string path)
		{
			writer = new StreamWriter(path);
		}

		public void Log(string message)
		{
			DateTime currentTime = DateTime.Now;

			try
			{
				writer.WriteLine("{0}: {1}", String.Format("{0}.{1}.{2}", currentTime.Hour, currentTime.Minute, currentTime.Second), message);
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: {0}", e.Message);
				Console.WriteLine("Stack trace: {0}", e.StackTrace);
			}
		}

		public void Close()
		{
			try
			{
				writer.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: {0}", e.Message);
				Console.WriteLine("Stack trace: {0}", e.StackTrace);
			}
		}
	}
}

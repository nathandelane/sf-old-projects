using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Nathandelane.Net.Irc
{
	public class IrcLogger
	{
		private IrcSettings _settings;
		private StreamWriter _logFileWriter;

		public IrcLogger()
		{
			_settings = new IrcSettings();

			string path = String.Format("{0}/{1}", _settings["LogFilePath"], _settings["LogFileName"]);
			_logFileWriter = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate | FileMode.Append));
		}

		public void WriteLine(string text)
		{
			_logFileWriter.WriteLine(text);
			_logFileWriter.Flush();
		}

		public void Close()
		{
			_logFileWriter.Close();
		}
	}
}

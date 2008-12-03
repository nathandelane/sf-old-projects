using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Console
{
	public class ConsoleState
	{
		private Dictionary<string, FileInfo> _files;

		public ConsoleState()
		{
			_files = new Dictionary<string, FileInfo>();
		}

		public void UpdateState(Settings settings)
		{
			string[] filePaths = Directory.GetFileSystemEntries(settings["currentDirectory"]);
			foreach (string path in filePaths)
			{
				_files.Add(path, new FileInfo(path));
			}
		}
	}
}

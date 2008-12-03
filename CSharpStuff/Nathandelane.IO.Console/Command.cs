using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.IO.Console
{
	internal class Command
	{
		private static int _fileNameFieldLength = 40;

		private static string FormatDate(DateTime dateTime)
		{
			string retVal = String.Empty;

			retVal = String.Format("{0:dd/MM/yyyy} {1:hh:mm tt}", dateTime.Date, dateTime);

			return retVal;
		}

		private static string FormatSize(long fileSize)
		{
			string retVal = String.Empty;
			/*
			if (fileSize > (1024 * 1024))
			{
				retVal = String.Format("{0:0.0} Mb", (fileSize / (1024 * 1024)));
			}
			else if (fileSize > 1024)
			{
				retVal = String.Format("{0:0.0} kb", (fileSize / 1024));
			}
			else
			{*/
			retVal = String.Format("{0:0,0} ", fileSize);
			//}

			return retVal;
		}

		internal static void Cls(TextBox commandTextBox)
		{
			commandTextBox.Clear();
		}

		internal static void Dir(string currentDir, TextBox commandTextBox, List<string> parts)
		{
			if (parts.Count > 1)
			{
				throw new NotImplementedException("Arguments not implemented for Dir");
			}
			else
			{
				string[] files = Directory.GetFileSystemEntries(currentDir);
				foreach (string file in files)
				{
					FileInfo info = new FileInfo(file);
					FileAttributes attributes = info.Attributes;
					string entry = String.Format("{0}", info.Name);

					try
					{
						commandTextBox.AppendText(String.Format("{0}{1,-20}     {2,15} {3,-" + _fileNameFieldLength + "} ", Environment.NewLine, FormatDate(info.LastWriteTime), FormatSize(info.Length), info.Name.Substring(0, ((info.Name.Length >= _fileNameFieldLength) ? _fileNameFieldLength : info.Name.Length))));
					}
					catch (Exception)
					{
						commandTextBox.AppendText(String.Format("{0}{1,-20}     {2,-15} {3,-" + _fileNameFieldLength + "} ", Environment.NewLine, FormatDate(info.LastWriteTime), "<DIR>", info.Name.Substring(0, ((info.Name.Length >= _fileNameFieldLength) ? _fileNameFieldLength : info.Name.Length))));
					}
				}
			}
		}

		internal static string ChDir(string currentDirectory, TextBox commandTextBox, List<string> parts)
		{
			string newDirectory = currentDirectory;

			if (parts[1].Equals("."))
			{
				// Do nothing then exit
			}
			else if (parts[1].Equals(".."))
			{
				DirectoryInfo newDirInfo = Directory.GetParent(currentDirectory);
				newDirectory = newDirInfo.Name;
			}
			else if (Directory.Exists(String.Format("{0}{1}", currentDirectory, parts[1])))
			{
				newDirectory = String.Format("{0}{1}", currentDirectory, parts[1]);
			}

			return newDirectory;
		}
	}
}

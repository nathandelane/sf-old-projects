using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.FileTool
{
	class Program
	{
		#region Fields

		private Inode _inode;

		#endregion

		#region Constructors

		private Program(string[] args)
		{
		}

		#endregion

		#region Private Methods

		private void ParseArguments(string[] args)
		{
			int index = 0;
			while (index < args.Length)
			{
				string nextArg = args[index];
				switch (nextArg.ToLower())
				{
					case "-f":
					case "--file":
						index++;
						FileInfo fileInfo = new FileInfo(args[index]);
						_inode = new Inode(fileInfo);
						break;
					case "-d":
					case "--directory":
						index++;
						DirectoryInfo directoryInfo = new DirectoryInfo(args[index]);
						_inode = new Inode(directoryInfo);
						break;
					case "-q":
					case "--query":
						index++;
						string query = args[index];
						arguments.AddOrReplace("query", query);
						break;
				}

				index++;
			}

			return arguments;
		}

		#endregion

		static void Main(string[] args)
		{
		}
	}
}

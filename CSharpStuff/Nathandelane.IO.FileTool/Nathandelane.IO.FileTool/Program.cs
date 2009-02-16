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

		private List<InodeQuery> _query;

		#endregion

		#region Constructors

		private Program(string[] args)
		{
			if (args.Length >= 2)
			{
				_query = new List<InodeQuery>();

				using (Inode inode = ParseArguments(args))
				{
					inode.Query(_query);
				}
			}
			else
			{
				DisplayHelp();
			}
		}

		#endregion

		#region Private Methods

		private Inode ParseArguments(string[] args)
		{
			Inode inode = null;
			int index = 0;
			while (index < args.Length)
			{
				string nextArg = args[index];
				switch (nextArg.ToLower())
				{
					case "-f":
					case "--file":
						index++;
						inode  = new Inode(new FileInfo(args[index]));
						break;
					case "-d":
					case "--directory":
						index++;
						inode = new Inode(new DirectoryInfo(args[index]));
						break;
					default:
						_query.Add(new InodeQuery(args[index]));
						break;
				}

				index++;
			}

			return inode;
		}


		private void DisplayHelp()
		{
			Console.WriteLine("Usage: FileTool (-f filePath|-d directory) [-a[=attribute1[&attributen]]] [-c[=[MM/DD/YYYY]HH:MM:SS]]] [-t[=[MM/DD/YYYY]HH:MM:SS]]] [-w[=[MM/DD/YYYY]HH:MM:SS]]]");
		}
		#endregion

		static void Main(string[] args)
		{
			new Program(args);
		}
	}
}

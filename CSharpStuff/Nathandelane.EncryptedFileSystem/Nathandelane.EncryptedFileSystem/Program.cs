using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Nathandelane.EncryptedFileSystem
{
	sealed class Program
	{
		private Program(string command, string file)
		{
			
		}

		static void Main(string[] args)
		{
			switch(args.Length)
			{
				case 0: Console.WriteLine("Command and file are required command-line arguments.\n\nUsage: file-system <command> <file>\n");
					break;
				case 1: Console.WriteLine("File is a required command-line argument.\n\nUsage: file-system <command> <file>\n");
					break;
				case 2: new Program(args[0], args[1]);
					break;
				default: Console.WriteLine("Command and file are required command-line arguments.\n\nUsage: file-system <command> <file>\n");
					break;
			}
		}
	}
}

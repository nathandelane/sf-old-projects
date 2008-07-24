using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Nathandelane.NCommand.CommandLibrary
{
	public class LsCommand : Command
	{
		private static Dictionary<string, bool> _parameters;
		private static bool _isFile;
		private static bool _isDirectory;

		public LsCommand()
		{
			_parameters = new Dictionary<string, bool>();
			_parameters.Add("a", false); // All, including hidden (.) files
			_parameters.Add("A", false); // All, including hidden (.) files, excluding working and parent directories
			_parameters.Add("d", false); // If arg is directory, only list dir not contents
			_parameters.Add("g", false); // Long listing without owner
			_parameters.Add("l", false); // List permissions, owners, size, and last modified datetime
			_parameters.Add("m", false); // Stream output format; files listed across page, comma-separated
			_parameters.Add("o", false); // Long listing without group
			_parameters.Add("p", false); // Display / in front of directories
			_parameters.Add("q", false); // Force non-printing chars to be displayed as ?
			_parameters.Add("r", false); // Reverse display order of files
			_parameters.Add("R", false); // Includes contents of directories
			_parameters.Add("s", false); // Display size in blocks
			_parameters.Add("u", false); // Show last accessed datetime
			_parameters.Add("x", false); // Display in columns
			_parameters.Add("1", false); // Display one item per line

			_isFile = false;
			_isDirectory = false;
		}

		public static void Execute(string parameters)
		{
			List<string> results = new List<string>();
			string[] ps = parameters.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

			if (ps.Length > 2)
			{
				Console.WriteLine("Usage: ls [ [[-]parameters] [file|directory] ]");
			}
			else if (ps.Length == 2)
			{

			}
		}
	}
}

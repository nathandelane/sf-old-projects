using System;
using System.Collections.Generic;
using System.Text;
using Nathandelane.NCommand.CommandLibrary;

namespace Nathandelane.NCommand
{
	class Program
	{
		private static Dictionary<string, Command> _commands;

		public Program()
		{
		}

		public Program(params string[] scriptParameters)
		{
		}

		private static void LoadCommands()
		{
			_commands = new Dictionary<string, Command>();
		}

		static void Main(string[] args)
		{
			LoadCommands();

			if (args.Length > 0)
			{
				new Program(args);
			}
			else
			{
				new Program();
			}
		}
	}
}

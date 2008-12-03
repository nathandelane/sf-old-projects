using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.IO.Console
{
	public class Interpreter
	{
		private static List<string> _parts;
		private static TextBox _commandTextBox;

		private Interpreter(TextBox commandTextBox)
		{
			_parts = new List<string>();
			_commandTextBox = commandTextBox;
		}

		public static Interpreter GetInstanceFor(TextBox commandTextBox)
		{
			Interpreter interpreter = null;
			interpreter = new Interpreter(commandTextBox);

			return interpreter;
		}

		public bool Interpret(Settings settings, string commandLine)
		{
			bool refreshPrompt = false;

			if (_commandTextBox != null)
			{
				_parts = new List<string>();

				string[] parts = commandLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string part in parts)
				{
					_parts.Add(part);
				}

				try
				{
					string cmd = _parts[0];
					switch (cmd)
					{
						case "cls":
							Command.Cls(_commandTextBox);
							refreshPrompt = false;
							break;
						case "clear":
							Command.Cls(_commandTextBox);
							refreshPrompt = false;
							break;
						case "dir":
							Command.Dir(settings.CurrentDirectory, _commandTextBox, _parts);
							refreshPrompt = false;
							break;
						case "ls":
							Command.Dir(settings.CurrentDirectory, _commandTextBox, _parts);
							refreshPrompt = false;
							break;
						case "chdir":
							settings.CurrentDirectory = Command.ChDir(settings.CurrentDirectory, _commandTextBox, _parts);
							settings["prompt"] = "%currentDirectory%> ";
							settings["currentDirectory"] = settings.CurrentDirectory;
							break;
						case "cd":
							settings.CurrentDirectory = Command.ChDir(settings.CurrentDirectory, _commandTextBox, _parts);
							settings["prompt"] = "%currentDirectory%> ";
							settings["currentDirectory"] = settings.CurrentDirectory;
							break;
						default:
							throw new Exception("Command unknown");
					}
				}
				catch (Exception e)
				{
					_commandTextBox.AppendText(String.Format("{0}{1}{2}", Environment.NewLine, e.Message, Environment.NewLine));
				}
			}
			else
			{
				throw new Exception("Could not find Console");
			}

			return refreshPrompt;
		}
	}
}

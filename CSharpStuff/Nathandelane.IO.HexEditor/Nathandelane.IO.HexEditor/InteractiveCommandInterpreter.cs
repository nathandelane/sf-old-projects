using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.HexEditor
{
	public class InteractiveCommandInterpreter
	{
		private HexFile _hexFile;
		private string _prompt;

		public InteractiveCommandInterpreter()
		{
			_prompt = ">";
			RunInterpreter();
		}

		public InteractiveCommandInterpreter(HexFile hexFile)
		{
			_prompt = ">";
			_hexFile = hexFile;
			RunInterpreter();
		}

		private void RunInterpreter()
		{
			string userInput = String.Empty;
			List<string> inputBuffer = new List<string>();

			do
			{
				Console.Write("{0} ", _prompt);
				inputBuffer.Add(DispatchCommand(userInput = Console.ReadLine()));

				Console.WriteLine();
			}
			while (!userInput.Equals("q"));
		}

		private string DispatchCommand(string userInput)
		{
			try
			{
				string[] tokens = userInput.Split(new char[] { ' ' });

				switch (tokens[0])
				{
					case "l":
						LoadFile((tokens.Length > 1) ? tokens[1] : String.Empty);
						break;
					case "help":
						break;
					case "h":
						break;
					case "?":
						break;
					case "q":
						break;
					default:
						Console.WriteLine("I do not understand the command {0}.", tokens[0]);
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Message: {0}", ex.Message);
			}

			return userInput;
		}

		private void LoadFile(string fileName)
		{
			if (!String.IsNullOrEmpty(fileName))
			{
			}
			else
			{
				throw new NullReferenceException("fileName");
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nathandelane.IO.Analyze
{
	class Program
	{
		public Program(string[] args)
		{
			string fileName = "";
			string characterClass = "";
			string characterSet = "";
			char token = '\n';

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].Equals("-f") || args[i].Equals("--file"))
				{
					i++;
					fileName = args[i];
				}
				else if (args[i].Equals("-c") || args[i].Equals("--class"))
				{
					i++;
					characterClass = args[i];
				}
				if (args[i].Equals("-s") || args[i].Equals("--set"))
				{
					i++;
					characterSet = args[i];
				}
				if (args[i].Equals("-t") || args[i].Equals("--stream"))
				{
					token = ';';
				}
			}

			if ((String.IsNullOrEmpty(characterClass) && String.IsNullOrEmpty(characterSet)) || String.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException();
			}
			else
			{
				if (File.Exists(fileName))
				{
					using (StreamReader reader = new StreamReader(fileName))
					{
						char[] characters = null;

						if (!String.IsNullOrEmpty(characterClass))
						{
							switch (characterClass.ToLower())
							{
								case "alpha":
									characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
									break;
								case "numeric":
									characters = "0123456789".ToCharArray();
									break;
								case "alphanumeric":
									characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
									break;
								case "alphalower":
									characters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
									break;
								case "alphaupper":
									characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
									break;
								case "alphanumericlower":
									characters = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
									break;
								case "alphanumericuppoer":
									characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
									break;
							}
						}
						else if (!String.IsNullOrEmpty(characterSet))
						{
							characters = characterSet.ToCharArray();
						}

						char[] fileContents = reader.ReadToEnd().ToCharArray();

						foreach (char c in characters)
						{
							int count = 0;

							for (int i = 0; i < fileContents.Length; i++)
							{
								if (fileContents[i] == c)
								{
									count++;
								}
							}

							Console.Write("{0}:{1}{2}", c, count, token);
						}
					}
				}
				else
				{
					Console.WriteLine("Cannot find file named '{0}'", fileName);
				}
			}
		}

		static void Main(string[] args)
		{
			string argument = String.Join(" ", args);

			if (argument.Contains("-h") || argument.Contains("--help"))
			{
				Help();
			}
			else if (args.Length < 2)
			{
				Usage();
			}
			else if (args.Length >= 2)
			{
				if ((argument.Contains("-f") && (argument.Contains("-s") || argument.Contains("-c"))) || (argument.Contains("--file") && (argument.Contains("--set") || argument.Contains("--class"))))
				{
					try
					{
						new Program(args);
					}
					catch (Exception)
					{
						Console.WriteLine("Missing arguments");
					}
				}
				else
				{
					Console.WriteLine("Missing arguments");
				}
			}
		}

		private static void Help()
		{
			Usage();

			Console.WriteLine("\n-c, --class		characterclass	Class of characters to analyze against.");
			Console.WriteLine("-f, --file		filename		Name of file to analyze.");
			Console.WriteLine("-h, --help		help			Show help file.");
			Console.WriteLine("-s, --set		characterset	Set of character to analyze against.");
			Console.WriteLine("-t, --stream		streamformat	Print results in stream format.");
		}

		private static void Usage()
		{
			Console.WriteLine("\nUsage: Analyze -f fileName (-s characterSet|-c characterClass) [-h] [-t]");
		}
	}
}

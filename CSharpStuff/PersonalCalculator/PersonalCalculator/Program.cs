using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
	class Program
	{
		internal static Dictionary<string, string> _settings;

		static void Version()
		{
			Console.WriteLine("This version of pcdotnet is: {0}", this.GetType().Assembly.ToString());
		}

		static void Help()
		{
			Console.WriteLine("Valid operations are ? (help), v (version), q (quit), +, -, *, /, and usage of ( and ) is permitted.");
		}

		static void Main(string[] args)
		{
			try
			{
				LoadConfig();
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Cannot load configuration. Using default settings.");
			}

			if (args.Length > 0)
			{
				string equation = String.Join(String.Empty, args);
				using (ArithmeticPostfixator postfixator = ArithmeticPostfixator.CreateInstance(equation))
				{
					ArithmeticPostfixEvaluator evaluator = ArithmeticPostfixEvaluator.CreateInstance(postfixator);
					Console.WriteLine(evaluator.Result);
				}
			}
			else
			{
				string userInput = String.Empty;

				while (!userInput.Equals("q"))
				{
					Console.Write("> ");

					userInput = Console.ReadLine();

					switch(userInput)
					{
						case "q":
							break;
						case "?":
							Help();
							break;
						case "h":
							Help();
							break;
						case "v":
							Version();
							break;
						default:
							string equation = String.Join(String.Empty, userInput.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries));
							using (ArithmeticPostfixator postfixator = ArithmeticPostfixator.CreateInstance(equation))
							{
								ArithmeticPostfixEvaluator evaluator = ArithmeticPostfixEvaluator.CreateInstance(postfixator);
								Console.WriteLine("{0}", evaluator.Result);
							}
							break;
					}
				}
			}
		}

		static void LoadConfig()
		{
			_settings = new Dictionary<string, string>();
			string[] keys = ConfigurationManager.AppSettings.AllKeys;

			foreach (string key in keys)
			{
				_settings.Add(key, ConfigurationManager.AppSettings[key]);
			}
		}
	}
}

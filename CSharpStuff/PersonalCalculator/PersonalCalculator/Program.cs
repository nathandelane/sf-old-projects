using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
	class Program
	{
		static void Version()
		{
			Console.WriteLine("This version of pcdotnet is: ");
		}

		static void Help()
		{
			Console.WriteLine("The help you asked for: ");
		}

		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				string equation = String.Join(String.Empty, args);
				using (Postfixator postfixator = Postfixator.CreateInstance(equation))
				{
					PostfixEvaluator evaluator = PostfixEvaluator.CreateInstance(postfixator);
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
							using (Postfixator postfixator = Postfixator.CreateInstance(equation))
							{
								PostfixEvaluator evaluator = PostfixEvaluator.CreateInstance(postfixator);
								Console.WriteLine("{0}", evaluator.Result);
							}
							break;
					}
				}
			}
		}
	}
}

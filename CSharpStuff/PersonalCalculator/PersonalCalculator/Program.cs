using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
	class Program
	{
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

					string equation = String.Join(String.Empty, userInput.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries));
					using (Postfixator postfixator = Postfixator.CreateInstance(equation))
					{
						PostfixEvaluator evaluator = PostfixEvaluator.CreateInstance(postfixator);
						Console.WriteLine("{0}", evaluator.Result);
					}
				}
			}
		}
	}
}

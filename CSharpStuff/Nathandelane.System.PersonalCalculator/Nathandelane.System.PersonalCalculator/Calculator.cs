using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator
{
	public class Calculator
	{
		#region Fields

		private static readonly string __quitOperation = "q";
		private static State __state;

		private Evaluator _evaluator;

		#endregion

		#region Properties

		public static State State
		{
			get { return __state; }
		}

		#endregion

		#region Constructors

		private Calculator()
		{
			__state = new State();
		}

		private Calculator(string[] args)
		{
			_evaluator = Evaluator.Evaluate(String.Join("", args));
		}

		#endregion

		#region Methods

		public static void Run(string[] args)
		{
			Calculator calc = null;

			if (args.Length > 0)
			{
				calc = new Calculator(args);

				Console.WriteLine("{0}", calc._evaluator.Result);
			}
			else
			{
				calc = new Calculator();

				string userInput = String.Empty;
				while (!userInput.Equals(__quitOperation))
				{
					Console.Write("> ");
					userInput = Console.ReadLine();
					userInput = String.Join("", userInput.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

					if (!TokenMatcher.IsExpression(userInput) && !TokenMatcher.ExtractionTable[TokenType.NumericResult].IsMatch(userInput))
					{
						Console.WriteLine("I do not understand the expression {0}. Perhaps you have malformatted something in it.", userInput);
						userInput = "0";
					}

					if (SpecialNumber.IsMatch(userInput))
					{
						userInput = SpecialNumber.Inject(userInput);
					}

					calc._evaluator = Evaluator.Evaluate(userInput);

					__state["$"] = calc._evaluator.Result;

					Console.WriteLine("{0}{1}", __state["$"], Environment.NewLine);
				}
			}
		}

		#endregion
	}
}

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

				string expression = String.Empty;
				while (true)
				{
					Console.Write("> ");
					expression = Console.ReadLine();
					expression = String.Join("", expression.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

					if (expression.Equals(__quitOperation))
					{
						break;
					}

					if (!TokenMatcher.IsExpression(expression) && !TokenMatcher.ExtractionTable[TokenType.NumericResult].IsMatch(expression))
					{
						Console.WriteLine("I do not understand the expression `{0}`.", expression);
						expression = "0";
					}

					if (SpecialNumber.IsMatch(expression))
					{
						expression = SpecialNumber.Inject(expression);
					}

					calc._evaluator = Evaluator.Evaluate(expression);

					__state["$"] = calc._evaluator.Result;

					Console.WriteLine("{0}{1}", __state["$"], Environment.NewLine);
				}

				Console.WriteLine("Thank you for using PC.NET");
			}
		}

		#endregion
	}
}

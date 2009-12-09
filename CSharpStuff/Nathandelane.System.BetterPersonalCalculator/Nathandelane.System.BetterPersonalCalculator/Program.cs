using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	class Program
	{
		#region Fields

		private CalculatorContext _context;

		#endregion

		#region Constructors

		private Program(ProgramArguments args)
		{
			bool continueExecution = true;

			_context = CalculatorContext.GetInstance();

			if (args.Args.Count > 0)
			{
				continueExecution = HandleArguments(args.Args);
			}

			if (continueExecution)
			{
				if (!String.IsNullOrEmpty(args.Expression))
				{
					Console.WriteLine("{0}", PerformEvaluation(args.Expression));
				}
				else
				{
					string userInput = String.Empty;
					bool userWantsToQuit = false;

					while (!userWantsToQuit)
					{
						Console.Write(">>> ");

						userInput = Console.ReadLine();
						userInput = userInput.Trim();

						if (userInput.Equals("q", StringComparison.InvariantCultureIgnoreCase))
						{
							userWantsToQuit = true;
							continue;
						}

						Console.WriteLine("{0}", PerformEvaluation(userInput));
					}

					Console.WriteLine("Thank you for using Better Personal Calculator.");
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Displays how to use BPC.
		/// </summary>
		private void DisplayUsage()
		{
			Console.WriteLine("Usage: bpc <options> <mathematical-expression>{0}Using BPC without any arguments enters into interactive mode.", Environment.NewLine);
		}

		/// <summary>
		/// Handle any program arguments.
		/// </summary>
		/// <param name="args"></param>
		/// <returns>Whether or not to continue program execution.</returns>
		private bool HandleArguments(IEnumerable<string> args)
		{
			bool continueExecution = true;

			return continueExecution;
		}

		/// <summary>
		/// Performs an evaluation on an expression.
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		private string PerformEvaluation(string strExpression)
		{
			ITokenizer tokenizer = new BpcTokenizer(strExpression);
			Expression expression = ExpressionYard.Formulate(tokenizer);
			Token result = expression.Evaluate();

			return String.Format("{0}", result);
		}

		/// <summary>
		/// Entry point for the program.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Program program = new Program(ProgramArguments.ParseArgs(args));
		}

		#endregion
	}
}

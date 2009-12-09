using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

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

					DisplayGreeting();

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

					DisplayThankYou();
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
		/// Displays the greeting.
		/// </summary>
		private void DisplayGreeting()
		{
			Console.WriteLine("BPC - Better Personal Calculator, Copyright (C) 2009 Nathandelane");
			Console.WriteLine("Version: {0}", Assembly.GetEntryAssembly().GetName().Version);
			Console.WriteLine("Type ? to get help; l to view license; q to quit.{0}", Environment.NewLine);
		}

		/// <summary>
		/// Displays the thank you message.
		/// </summary>
		private void DisplayThankYou()
		{
			Console.WriteLine("Thank you for using Better Personal Calculator.");
		}

		/// <summary>
		/// Displays all help messages.
		/// </summary>
		private void DisplayLongHelp()
		{
			Console.WriteLine(@"Usage: bpc [OPTIONS] <expression>
Version: 1.2.2.0
Options:
--mode-degrees        Sets the calculator in degree mode.
--mode-radians        Sets the calculator in radian mode (default).
--help                Displays this help message.
--version             Displays the version of BCP currently running.
--license             Displays the current license information for BPC.{0}", Environment.NewLine);

			DisplayHelp();
		}

		/// <summary>
		/// Displays internal help messages.
		/// </summary>
		private void DisplayHelp()
		{
			Console.WriteLine(@"Supported functionality:
Decimal numbers; hexadecimal numbers ending with h, octal numbers ending with o, binary numbers ending with b.
Arithmetic operators: +, -, *, /
Functions: ** (power), // (div), % (mod), ! (factorial), cos, acos, cosh, sin, asin, sinh, tan, atan, tanh, sqrt, toh, tod, tob, too
Constants: pi, e, $ (last result)
Parentheses: (, )
Reserved: ? (displays help); v (displays version); l (displays license); q (quits)");
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
			string strResult = String.Format("{0}", result);

			if (_context[CalculatorContext.DisplayBase].ToString().Equals("2", StringComparison.InvariantCultureIgnoreCase))
			{
				strResult = ((NumberToken)result).AsBin();

				ResetDisplayBase();
			}
			else if (_context[CalculatorContext.DisplayBase].ToString().Equals("8", StringComparison.InvariantCultureIgnoreCase))
			{
				strResult = ((NumberToken)result).AsOct();

				ResetDisplayBase();
			}
			else if (_context[CalculatorContext.DisplayBase].ToString().Equals("16", StringComparison.InvariantCultureIgnoreCase))
			{
				strResult = ((NumberToken)result).AsHex();

				ResetDisplayBase();
			}

			return strResult;
		}

		/// <summary>
		/// Resets the display base to 10.
		/// </summary>
		private void ResetDisplayBase()
		{
			_context[CalculatorContext.DisplayBase] = new NumberToken("10");
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

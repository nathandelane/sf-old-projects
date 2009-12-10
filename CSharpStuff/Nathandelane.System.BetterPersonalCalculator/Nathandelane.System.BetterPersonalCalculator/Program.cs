/*
Nathan Lane, Nathandelane Copyright (C) 2009, Nathandelane.

Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 3, or (at your option)
any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
02111-1307, USA.
*/

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
				if (String.IsNullOrEmpty(args.Expression))
				{
					Interact();
				}
				else
				{
					Console.WriteLine("{0}", PerformEvaluation(args.Expression));
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Interactive mode program loop.
		/// </summary>
		private void Interact()
		{
			string consoleTitle = Console.Title;
			string userInput = String.Empty;
			bool userWantsToQuit = false;
			
			Console.Title = "BPC.NET - Better Personal Calculator";

			DisplayGreeting();

			while (!userWantsToQuit)
			{
				Console.Write("{0}>>> ", Environment.NewLine);

				userInput = Console.ReadLine();
				userInput = userInput.Trim();

				if (userInput.Equals("q", StringComparison.InvariantCultureIgnoreCase))
				{
					userWantsToQuit = true;
					continue;
				}
				else if (userInput.Equals("?", StringComparison.InvariantCultureIgnoreCase))
				{
					DisplayHelp();
					continue;
				}
				else if (userInput.Equals("l", StringComparison.InvariantCultureIgnoreCase))
				{
					DisplayLicense();
					continue;
				}

				Console.WriteLine("{0}", PerformEvaluation(userInput));
			}

			DisplayThankYou();

			Console.Title = consoleTitle;
		}

		/// <summary>
		/// Displays the license.
		/// </summary>
		private void DisplayLicense()
		{
			Console.WriteLine(@"PC.NET, BPC, and Better Personal Calculator are all names used to 
describe this software which is copyrighted by:

Nathan Lane, Nathandelane Copyright (C) 2009, Nathandelane.

GNU General Public Licence, version 3.

Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 3, or (at your option)
any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
02111-1307, USA.");
		}

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

			DisplayVersion();

			Console.WriteLine("Type ? to get help; l to view license; q to quit.");
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
			DisplayUsage();

			Console.WriteLine(@"Version: 1.2.2.0
Options:
--help                Displays this help message.
--license             Displays the current license information for BPC.
--mode-degrees        Sets the calculator in degree mode.
--mode-radians        Sets the calculator in radian mode (default).
--version             Displays the version of BCP currently running.{0}", Environment.NewLine);

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
		/// Displays the version.
		/// </summary>
		private void DisplayVersion()
		{
			Console.WriteLine("Version: {0}", Assembly.GetEntryAssembly().GetName().Version);
		}

		/// <summary>
		/// Handle any program arguments.
		/// </summary>
		/// <param name="args"></param>
		/// <returns>Whether or not to continue program execution.</returns>
		private bool HandleArguments(IEnumerable<string> args)
		{
			bool continueExecution = true;

			foreach (string nextArg in args)
			{
				if (nextArg.Equals("mode-degrees", StringComparison.InvariantCultureIgnoreCase))
				{
					_context[CalculatorContext.Mode] = new VariableToken("deg");
				}
				else if (nextArg.Equals("help", StringComparison.InvariantCultureIgnoreCase))
				{
					DisplayHelp();

					continueExecution = false;
				}
				else if (nextArg.Equals("version", StringComparison.InvariantCultureIgnoreCase))
				{
					DisplayVersion();

					continueExecution = false;
				}
				else if (nextArg.Equals("license", StringComparison.InvariantCultureIgnoreCase))
				{
					DisplayLicense();
					continueExecution = false;
				}
			}

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

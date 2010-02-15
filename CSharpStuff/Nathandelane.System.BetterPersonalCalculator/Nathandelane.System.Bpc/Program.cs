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

namespace Nathandelane.System.Bpc
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
					string[] expressions = GetExpressions(args.Expression);

					foreach (string nextExpression in expressions)
					{
						try
						{
							Console.WriteLine("{0}", PerformEvaluation(nextExpression));
						}
						catch (Exception ex)
						{
							Console.WriteLine("{0}", ex.Message);
							Environment.ExitCode = 1;
						}
					}
				}
			}
			else
			{
				Console.WriteLine();
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///  Gets multiple expressions.
		/// </summary>
		/// <param name="wholeExpression"></param>
		/// <returns></returns>
		private string[] GetExpressions(string wholeExpression)
		{
			return wholeExpression.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
		}

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
				else if (userInput.Equals("v", StringComparison.InvariantCultureIgnoreCase))
				{
					DisplayVersion();
					continue;
				}

				if (!String.IsNullOrEmpty(userInput))
				{
					foreach (string nextExpression in GetExpressions(userInput))
					{
						try
						{
							Console.WriteLine("{0}", PerformEvaluation(nextExpression));
						}
						catch (Exception ex)
						{
							Console.WriteLine("{0}", ex.Message);
						}
					}
				}
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
			Console.WriteLine("Thank you for using Better Personal Calculator.{0}", Environment.NewLine);
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
			DisplayReserved();
		}

		/// <summary>
		/// Displays internal help messages.
		/// </summary>
		private void DisplayHelp()
		{
			Console.WriteLine(@"Supported functionality:
Decimal numbers; hexadecimal numbers ending with h, octal numbers ending with o, binary numbers ending with b.
Arithmetic operators: +, -, *, /
Binary operators: & (and), | (or), ^ (xor)
Boolean operators (not chainable): ==, <=, >=, !=
Functions: ** (power), // (div), % (mod), ! (factorial), cos, acos, cosh, sin, asin, sinh, tan, atan, tanh, sqrt, log (base-10), ln, lb (ld|lg, base-2)
Conversion: toh (hex), tod (dec), tob (bin), too (oct), deg, rad
Constants: pi, e, $ (last result)
Parentheses: (, )
Expression delimiter: ; (separate multiple expressions on teh same line)
Variables: assignment using =, inline usage, names must begin with underscore (_)");
		}

		/// <summary>
		/// Displays reserved keywords, used only in interactive mode.
		/// </summary>
		private void DisplayReserved()
		{
			Console.WriteLine("Reserved: ? (displays help), v (displays version), l (displays license), q (quits)");
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
			string strResult = "0";
			ITokenizer tokenizer = new BpcTokenizer(strExpression);
			ITokenizer postfixTokenizer = new PostfixTokenizer(tokenizer);
			Expression expression = ExpressionYard.Formulate(postfixTokenizer);
			Token result = expression.Evaluate();

			if (result is VariableToken)
			{
				result = _context[result.ToString()];
			}

			strResult = String.Format("{0}", result);

			if (result is NumberToken)
			{
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
			}
			else if (!(result is BooleanToken))
			{
				throw new Exception(String.Format("Unexpected token was returned as result. Type: {0} value: {1}", result.Type, result));
			}

			_context[CalculatorContext.LastResult] = result;

			return strResult;
		}

		/// <summary>
		/// Resets the display base to 10.
		/// </summary>
		private void ResetDisplayBase()
		{
			_context[CalculatorContext.DisplayBase] = new NumberToken("10", "10");
		}

		/// <summary>
		/// Entry point for the program.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Program program = new Program(ProgramArguments.ParseArgs(args));
			Environment.Exit(0);
		}

		#endregion
	}
}

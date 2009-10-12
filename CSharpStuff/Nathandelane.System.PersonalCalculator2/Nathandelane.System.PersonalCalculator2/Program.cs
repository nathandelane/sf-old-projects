/*  Copyright (C) 2009, Nathandelane.
	License:
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
using System.Reflection;
using System.Linq;

namespace Nathandelane.System.PersonalCalculator2
{
	class Program
	{
		#region Fields

		private static readonly string Name = Assembly.GetEntryAssembly().GetName().Name;
		private static readonly string Version = Assembly.GetEntryAssembly().GetName().Version.ToString();

		private static readonly string ModeDegreesArg = "mode-degrees";
		private static readonly string ModeRadiansArg = "mode-radians";
		private static readonly string HelpArg = "help";
		private static readonly ArgumentMap __argumentMap = new ArgumentMap()
		{
			{ ModeDegreesArg, ArgumentType.Null },
			{ ModeRadiansArg, ArgumentType.Null },
			{ HelpArg, ArgumentType.Null }
		};

		#endregion

		#region Methods

		/// <summary>
		/// Runs the evaluation loop.
		/// </summary>
		/// <param name="expression"></param>
		private static void Run(string expression)
		{
			string[] tokens = expression.Tokenize(Enumerable.ToArray<string>(Calculator.Patterns.Keys));

			if (tokens.Length > 0)
			{
				Console.WriteLine("{0}", ExpressionEvaluator.Evaluate(tokens));

				Console.ReadLine();
			}
			else
			{
				string userInput = String.Empty;

				Console.WriteLine("BPC - Better Personal Calculator, Copyright (C) 2009 Nathandelane, Version {0}", Version);
				while (!userInput.Equals("quit"))
				{
					Console.Write(">>> ");
					
					userInput = Console.ReadLine();
					tokens = userInput.Tokenize(Enumerable.ToArray<string>(Calculator.Patterns.Keys));

					Console.WriteLine("{0}", ExpressionEvaluator.Evaluate(tokens));
				}
			}
		}

		/// <summary>
		/// Displays system help.
		/// </summary>
		private static void DisplayHelp()
		{
			Console.WriteLine("Usage: {0} [OPTIONS] <expression>", Name);
			Console.WriteLine("Version: {0}", Version);
			Console.WriteLine(@"Options:
--mode-degrees        Sets the calculator in degree mode.
--mode-radians        Sets the calculator in radian mode (default).
--help                Displays this help message.
--version             Displays the version of BCP currently running.");
		}

		private static void DisplayVersion()
		{
			Console.WriteLine("Version: {0}", Version);
		}

		/// <summary>
		/// Removes optional arguments from the command-line input.
		/// </summary>
		/// <param name="arguments"></param>
		/// <param name="argumentCollection"></param>
		/// <returns>string</returns>
		private static string RemoveKnownArguments(string arguments, ArgumentCollection argumentCollection)
		{
			foreach (string key in argumentCollection.Keys)
			{
				if (__argumentMap[key] == ArgumentType.Null)
				{
					string arg = String.Concat(ArgumentCollection.Delimiter, key);
					int index = arguments.IndexOf(arg);
					if (index > -1)
					{
						arguments = arguments.Remove(index, arg.Length);
					}
				}
				else
				{
					string arg = String.Concat(ArgumentCollection.Delimiter, key, "=", argumentCollection[key]);
					int index = arguments.IndexOf(arg);
					if (index > -1)
					{
						arguments = arguments.Remove(index, arg.Length);
					}
				}
			}
			
			return arguments;
		}

		#region Main Method

		static void Main(string[] args)
		{
			string arguments = args.Join();

			if (!arguments.IsNullOrEmpty())
			{
				ArgumentCollection argumentCollection = ArgumentCollection.Parse(args, __argumentMap);

				arguments = RemoveKnownArguments(arguments, argumentCollection);
			}

			Run(arguments.Trim());
		}

		#endregion

		#endregion
	}
}

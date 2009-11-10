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
		private static readonly string VersionArg = "version";
		private static readonly ArgumentMap __argumentMap = new ArgumentMap()
		{
			{ ModeDegreesArg, ArgumentType.Null },
			{ ModeRadiansArg, ArgumentType.Null },
			{ HelpArg, ArgumentType.Null },
			{ VersionArg, ArgumentType.Null }
		};

		#endregion

		#region Methods

		/// <summary>
		/// Runs the evaluation loop.
		/// </summary>
		/// <param name="expression"></param>
		private static void Run(string expression)
		{
			expression = ReplaceSymbols(expression);
			string[] tokens = expression.Tokenize(Enumerable.ToArray<string>(Calculator.Patterns.Keys));

			if (tokens.Length > 0)
			{
				try
				{
					Console.WriteLine("{0}", ExpressionEvaluator.Evaluate(tokens));
				}
				catch (Exception e)
				{
					Console.WriteLine("Error occurred: {0}", e.Message);
					Logger.Error(String.Format("{0}", e.StackTrace));
				}
			}
			else
			{
				string userInput = String.Empty;

				Program.DisplayCopyright();
				Program.DisplayVersion();
				Console.WriteLine("Type ? to get help; q to quit.{0}", Environment.NewLine);

				while (true)
				{
					Console.Write(">>> ");

					userInput = Console.ReadLine();

					if (userInput.Equals("q") || userInput.Equals("quit"))
					{
						break;
					}
					else if (userInput.Equals("?") || userInput.Equals("help"))
					{
						Program.DisplayHelp();
					}
					else if (userInput.Equals("v") || userInput.Equals("ver") || userInput.Equals("version"))
					{
						Program.DisplayVersion();
					}
					else
					{
						userInput = ReplaceSymbols(userInput);
						tokens = userInput.Tokenize(Enumerable.ToArray<string>(Calculator.Patterns.Keys));

						try
						{
							ExpressionEvaluator result = ExpressionEvaluator.Evaluate(tokens);
							Calculator.Heap["$"] = result.ToString();

							Console.WriteLine("{0}", Calculator.Heap["$"]);
						}
						catch (Exception e)
						{
							Console.WriteLine("Error occurred: {0}", e.Message);
							Logger.Error(String.Format("{0}", e.StackTrace));
						}
					}

					Console.WriteLine(String.Empty);
				}
			}
		}

		/// <summary>
		/// Replaces special constant symbols and other meaningless symbols.
		/// </summary>
		/// <param name="userInput"></param>
		/// <returns>expression without constant symbols</returns>
		private static string ReplaceSymbols(string userInput)
		{
			string result = userInput;

			result = result.Replace("\t", String.Empty);
			result = result.Replace(" ", String.Empty);
			result = result.Replace("$", Calculator.Heap["$"].ToString());
			result = result.Replace("pi", Math.PI.ToString());
			result = result.Replace("e", Math.E.ToString());

			return result;
		}

		/// <summary>
		/// Displays system help.
		/// </summary>
		private static void DisplayHelp()
		{
			Console.WriteLine(@"Supported functionality: 
Arithmetic operators: +, -, *, /
Functions: ** (power), // (div), % (mod), ! (factorial), cos, acos, cosh, sin, asin, sinh, tan, atan, tanh, sqrt
Constants: pi, e, $ (last result)
Parentheses: (, )
Reserved: ? (displays help); v (displays version); q (quits)");
		}

		/// <summary>
		/// Displays the usage of the system when run in commandline mode rather than interactive mode.
		/// </summary>
		private static void DisplayUsage()
		{
			Console.WriteLine("Usage: {0} [OPTIONS] <expression>", Name);
			Console.WriteLine("Version: {0}", Version);
			Console.WriteLine(@"Options:
--mode-degrees        Sets the calculator in degree mode.
--mode-radians        Sets the calculator in radian mode (default).
--help                Displays this help message.
--version             Displays the version of BCP currently running.
");
		}

		/// <summary>
		/// Displays the version.
		/// </summary>
		private static void DisplayVersion()
		{
			Console.WriteLine("Version: {0}", Version);
		}

		/// <summary>
		/// Displays copyright information.
		/// </summary>
		private static void DisplayCopyright()
		{
			Console.WriteLine("BPC - Better Personal Calculator, Copyright (C) 2009 Nathandelane");
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
			ArgumentCollection argumentCollection = null;

			Calculator.Heap.Add("$", "0");
			Calculator.Heap.Add("mode", "rad");

			string arguments = args.Join();

			if (!arguments.IsNullOrEmpty())
			{
				argumentCollection = ArgumentCollection.Parse(args, __argumentMap);
				arguments = RemoveKnownArguments(arguments, argumentCollection);
			}

			if (!argumentCollection.IsNull())
			{
				if (argumentCollection.ContainsKey(HelpArg))
				{
					DisplayUsage();
					DisplayHelp();
				}
				else if (argumentCollection.ContainsKey(VersionArg))
				{
					DisplayVersion();
				}
				else
				{
					if (argumentCollection.ContainsKey(ModeDegreesArg))
					{
						Calculator.Heap["mode"] = "deg";
					}
					else if (argumentCollection.ContainsKey(ModeRadiansArg))
					{
						Calculator.Heap["mode"] = "rad";
					}

					Run(arguments.Trim().Replace(" ", String.Empty));
				}
			}
			else
			{
				string title = Console.Title;

				Console.Title = "BPC.NET - Better Personal Calculator";

				Run(String.Empty);

				Console.Title = title;
			}
		}

		#endregion

		#endregion
	}
}

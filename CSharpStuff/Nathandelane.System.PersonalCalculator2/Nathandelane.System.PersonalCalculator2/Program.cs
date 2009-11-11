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
using System.Text.RegularExpressions;

namespace Nathandelane.System.PersonalCalculator2
{
	class Program
	{
		#region Fields

		public static readonly string Name = Assembly.GetEntryAssembly().GetName().Name;
		public static readonly string Version = Assembly.GetEntryAssembly().GetName().Version.ToString();

		private static readonly string PromptKey = "prompt";
		private static readonly string ModeKey = "mode";
		private static readonly string LastResultKey = "$";
		private static readonly string ModeDegreesArg = "mode-degrees";
		private static readonly string ModeRadiansArg = "mode-radians";
		private static readonly string HelpArg = "help";
		private static readonly string VersionArg = "version";
		private static readonly string LicenseArg = "license";
		private static readonly ArgumentMap __argumentMap = new ArgumentMap()
		{
			{ ModeDegreesArg, ArgumentType.Null },
			{ ModeRadiansArg, ArgumentType.Null },
			{ HelpArg, ArgumentType.Null },
			{ VersionArg, ArgumentType.Null },
			{ LicenseArg, ArgumentType.Null }
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

					if (e.Message.Contains("Stack empty"))
					{
						Console.WriteLine(Messages.StackEmptyException);
					}

					Logger.Error(String.Format("{0}", e.StackTrace));
				}
			}
			else
			{
				string userInput = String.Empty;

				Program.DisplayCopyright();
				Program.DisplayVersion();
				Console.WriteLine(Messages.InteractiveBriefHelp);

				while (true)
				{
					Console.Write("{0} ", Calculator.Heap[Program.PromptKey]);

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
					else if (userInput.Equals("l") || userInput.Equals("license"))
					{
						Program.DisplayLicense();
					}
					else
					{
						userInput = ReplaceSymbols(userInput);
						tokens = userInput.Tokenize(Enumerable.ToArray<string>(Calculator.Patterns.Keys));

						try
						{
							ExpressionEvaluator result = ExpressionEvaluator.Evaluate(tokens);
							Calculator.Heap[Program.LastResultKey] = result.ToString();

							Console.WriteLine("{0}", Calculator.Heap[Program.LastResultKey]);
						}
						catch (Exception e)
						{
							Console.WriteLine("Error occurred: {0}", e.Message);

							if (e.Message.Contains("Stack empty"))
							{
								Console.WriteLine(Messages.StackEmptyException);
							}

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

			result = result.Replace(new Regex("(#){1}[A-Za-z.\\d\\s,]+"), String.Empty);
			result = result.Replace("\t", String.Empty);
			result = result.Replace(" ", String.Empty);
			result = result.Replace("$", Calculator.Heap[Program.LastResultKey].ToString());
			result = result.Replace("pi", Math.PI.ToString());
			result = result.Replace("e", Math.E.ToString());

			return result;
		}

		/// <summary>
		/// Displays the license.
		/// </summary>
		private static void DisplayLicense()
		{
			Console.WriteLine(Messages.ShortLicense);
		}

		/// <summary>
		/// Displays system help.
		/// </summary>
		private static void DisplayHelp()
		{
			Console.WriteLine(Messages.Help);
		}

		/// <summary>
		/// Displays the usage of the system when run in commandline mode rather than interactive mode.
		/// </summary>
		private static void DisplayUsage()
		{
			Console.WriteLine(Messages.Usage);
		}

		/// <summary>
		/// Displays the version.
		/// </summary>
		private static void DisplayVersion()
		{
			Console.WriteLine(Messages.ShortVersion);
		}

		/// <summary>
		/// Displays copyright information.
		/// </summary>
		private static void DisplayCopyright()
		{
			Console.WriteLine(Messages.Copyright);
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

			Calculator.Heap.Add(Program.LastResultKey, "0");
			Calculator.Heap.Add(Program.ModeKey, "rad");
			Calculator.Heap.Add(Program.PromptKey, ">>>");

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
				else if (argumentCollection.ContainsKey(LicenseArg))
				{
					DisplayLicense();
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

				Console.Title = Messages.BpcConsoleTitle;

				Run(String.Empty);

				Console.Title = title;
			}
		}

		#endregion

		#endregion
	}
}

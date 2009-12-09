﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class ProgramArguments
	{
		#region Fields

		private IList<string> _args;
		private string _expression;

		#endregion

		/// <summary>
		/// Gets the actual program arguments.
		/// </summary>
		public IList<string> Args
		{
			get { return _args; }
		}

		/// <summary>
		/// Gets the supplied expression.
		/// </summary>
		public string Expression
		{
			get { return _expression; }
		}

		#region Constructors

		private ProgramArguments()
		{
			_args = new List<string>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Parses args as a set of program arguments specific to BPC.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static ProgramArguments ParseArgs(string[] args)
		{
			ProgramArguments programArguments = new ProgramArguments();
			Regex argRegex = new Regex("^([-]{1,2}|[/]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			StringBuilder expressionBuilder = new StringBuilder();

			foreach (string nextArg in args)
			{
				if (argRegex.IsMatch(nextArg))
				{
					programArguments._args.Add(RemoveArgDelimiters(nextArg));
				}
				else
				{
					expressionBuilder.Append(nextArg);
				}
			}

			programArguments._expression = expressionBuilder.ToString();

			return programArguments;
		}

		/// <summary>
		/// Replace argument delimiters.
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		private static string RemoveArgDelimiters(string arg)
		{
			string internalArg = arg;

			if (arg.StartsWith("--"))
			{
				internalArg = internalArg.Replace("--", String.Empty);
			}
			else if (arg.StartsWith("-"))
			{
				internalArg = internalArg.Replace("-", String.Empty);
			}
			else if (arg.StartsWith("/"))
			{
				internalArg = internalArg.Replace("/", String.Empty);
			}

			return internalArg;
		}

		#endregion
	}
}

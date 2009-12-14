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
			Regex argRegex = new Regex("^([-]{2}|[/]{1}){1}[A-Za-z\\d-=]+", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			StringBuilder expressionBuilder = new StringBuilder();

			foreach (string nextArg in args)
			{
				string[] subArgs = nextArg.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string sub in subArgs)
				{
					if (argRegex.IsMatch(sub))
					{
						programArguments._args.Add(RemoveArgDelimiters(nextArg));
					}
					else
					{
						expressionBuilder.Append(String.Concat(sub, " "));
					}
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
			else if (arg.StartsWith("/"))
			{
				internalArg = internalArg.Replace("/", String.Empty);
			}

			return internalArg;
		}

		#endregion
	}
}

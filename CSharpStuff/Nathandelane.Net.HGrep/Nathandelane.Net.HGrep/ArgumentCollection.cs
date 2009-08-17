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
using System.Collections.Generic;

namespace Nathandelane.Net.HGrep
{
	public class ArgumentCollection : Dictionary<string, object>
	{
		#region Fields

		public static readonly string CleanArg = "clean";
		public static readonly string CountOnlyArg = "count-only";
		public static readonly string FindArg = "find";
		public static readonly string FindRegexpArg = "find-regexp";
		public static readonly string HelpArg = "help";
		public static readonly string IgnoreBadCertsArg = "ignore-bad-certs";
		public static readonly string NoAttributesArg = "no-attributes";
		public static readonly string NoInnerHtmlArg = "no-inner-html";
		public static readonly string NoNumberingArg = "no-numbering";
		public static readonly string PostBodyArg = "post-body";
		public static readonly string ReturnAttributesArg = "return-attributes";
		public static readonly string ReturnDataArg = "return-data";
		public static readonly string ReturnHeadersArg = "return-headers";
		public static readonly string ReturnUrlArg = "return-url";
		public static readonly string ScrubArg = "scrub";
		public static readonly string TimeoutArg = "timeout";
		public static readonly string UriArg = "uri";
		public static readonly string VersionArg = "version";

		private static readonly Dictionary<string, ArgumentType> __map = new Dictionary<string, ArgumentType>()
		{
			{ CleanArg, ArgumentType.Null },
			{ CountOnlyArg, ArgumentType.Null },
			{ FindArg, ArgumentType.String },
			{ FindRegexpArg, ArgumentType.String },
			{ HelpArg, ArgumentType.String },
			{ IgnoreBadCertsArg, ArgumentType.Null },
			{ NoAttributesArg, ArgumentType.Null },
			{ NoInnerHtmlArg, ArgumentType.Null },
			{ NoNumberingArg, ArgumentType.Null },
			{ PostBodyArg, ArgumentType.String },
			{ ReturnAttributesArg, ArgumentType.StringArray },
			{ ReturnDataArg, ArgumentType.Null },
			{ ReturnHeadersArg, ArgumentType.Null },
			{ ReturnUrlArg, ArgumentType.Null },
			{ ScrubArg, ArgumentType.Null },
			{ TimeoutArg, ArgumentType.Int },
			{ UriArg, ArgumentType.String },
			{ VersionArg, ArgumentType.Null }
		};

		#endregion

		#region Constructors

		private ArgumentCollection()
			: base()
		{
		}

		#endregion

		#region Methods

		public static ArgumentCollection Parse(string[] args)
		{
			ArgumentCollection collection = new ArgumentCollection();

			foreach (string nextArg in args)
			{
				string currentArg = StripArgumentIdentifyer(nextArg);
				string argName = String.Empty;
				object argValue = null;

				if (currentArg.Contains("="))
				{
					int indexOfEqualsSign = currentArg.IndexOf("=");
					argName = currentArg.Substring(0, indexOfEqualsSign);

					if (__map.ContainsKey(argName))
					{
						if (__map[argName] == ArgumentType.Null)
						{
							argValue = null;
						}
						else if (__map[argName] == ArgumentType.String)
						{
							argValue = currentArg.Substring(indexOfEqualsSign + 1);
						}
						else if (__map[argName] == ArgumentType.StringArray)
						{
							argValue = currentArg.Substring(indexOfEqualsSign + 1).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
						}
						else if (__map[argName] == ArgumentType.Int)
						{
							int outValue = 0;
							if (Int32.TryParse(currentArg, out outValue))
							{
								argValue = outValue;
							}
							else
							{
								throw new ArgumentException(String.Format("Argument {0} must be of int (Int32) type", argName));
							}
						}
					}
					else
					{
						throw new ArgumentException(String.Format("{0} is not recognized as a valid argument", argName));
					}
				}
				else
				{
					argName = currentArg;
				}

				collection.Add(argName, argValue);
			}

			return collection;
		}

		private static string StripArgumentIdentifyer(string arg)
		{
			if (arg.StartsWith("--"))
			{
				arg = arg.Substring(2);
			}
			else if (arg.StartsWith("-"))
			{
				arg = arg.Substring(1);
			}
			else if (arg.StartsWith("/"))
			{
				arg = arg.Substring(1);
			}

			return arg;
		}

		#endregion
	}
}

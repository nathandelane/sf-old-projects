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

namespace Nathandelane.System
{
	public class ArgumentCollection : Dictionary<string, object>
	{
		#region Fields

		public static readonly string Delimiter = "--";

		private static ArgumentMap __argumentMap;

		#endregion

		#region Properties

		public ArgumentMap Map
		{
			get { return __argumentMap; }
		}

		#endregion

		#region Constructors

		private ArgumentCollection(ArgumentMap argumentMap)
			: base()
		{
			__argumentMap = argumentMap;
		}

		#endregion

		#region Methods

		public static ArgumentCollection Parse(string[] args, ArgumentMap argumentMap)
		{
			ArgumentCollection collection = new ArgumentCollection(argumentMap);

			foreach (string nextArg in args)
			{
				string currentArg = nextArg;
				string argName = String.Empty;
				object argValue = null;

				if (currentArg.StartsWith(ArgumentCollection.Delimiter))
				{
					if (currentArg.Contains("="))
					{
						int indexOfEqualsSign = currentArg.IndexOf("=");
						argName = StripArgumentIdentifyer(currentArg).Substring(0, indexOfEqualsSign);

						if (collection.Map.ContainsKey(argName))
						{
							if (collection.Map[argName] == ArgumentType.Null)
							{
								argValue = null;
							}
							else if (collection.Map[argName] == ArgumentType.String)
							{
								argValue = currentArg.Substring(indexOfEqualsSign + 1);
							}
							else if (collection.Map[argName] == ArgumentType.StringArray)
							{
								argValue = currentArg.Substring(indexOfEqualsSign + 1).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
							}
							else if (collection.Map[argName] == ArgumentType.Int)
							{
								int outValue = 0;
								if (Int32.TryParse(currentArg.Substring(indexOfEqualsSign + 1), out outValue))
								{
									argValue = outValue;
								}
								else
								{
									throw new ArgumentException(String.Format("Argument {0} must be of int (Int32) type", argName));
								}
							}
							else if (collection.Map[argName] == ArgumentType.IntOrString)
							{
								int outValue = 0;
								if (Int32.TryParse(currentArg.Substring(indexOfEqualsSign + 1), out outValue))
								{
									argValue = outValue;
								}
								else
								{
									argValue = currentArg.Substring(indexOfEqualsSign + 1);
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
						argName = StripArgumentIdentifyer(currentArg);
					}

					collection.Add(argName, argValue);
				}
			}

			return collection;
		}

		private static string StripArgumentIdentifyer(string arg)
		{
			return arg.Substring(ArgumentCollection.Delimiter.Length);
		}

		#endregion
	}
}

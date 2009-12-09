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

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class CalculatorContext
	{
		#region Fields

		public const string LastResult = "$";
		public const string DisplayBase = "base";
		public const string Mode = "mode";

		private static CalculatorContext __instance = new CalculatorContext();
		private static Object __lockObject = new Object();

		private Dictionary<string, Token> _values;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a context item.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public Token this[string key]
		{
			get
			{
				Token token = new NullToken();

				if (_values.ContainsKey(key))
				{
					token = (Token)_values[key];
				}

				return token;
			}
			set
			{
				if (!_values.ContainsKey(key))
				{
					_values.Add(key, value);
				}
				else
				{
					if (value == null)
					{
						_values.Remove(key);
					}
					else
					{
						_values[key] = value;
					}
				}
			}
		}

		#endregion

		#region Constructors

		private CalculatorContext()
		{
			_values = new Dictionary<string, Token>();

			this[CalculatorContext.LastResult] = new NumberToken();
			this[CalculatorContext.DisplayBase] = new NumberToken("10");
			this[CalculatorContext.Mode] = new VariableToken("rad");
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a reference to the singleton instance of CalculatorContext.
		/// </summary>
		/// <returns></returns>
		public static CalculatorContext GetInstance()
		{
			if (CalculatorContext.__instance == null)
			{
				lock (CalculatorContext.__lockObject)
				{
					CalculatorContext.__instance = new CalculatorContext();
				}
			}

			return CalculatorContext.__instance;
		}

		/// <summary>
		/// Gets the last result.
		/// </summary>
		/// <returns></returns>
		public Token GetLastResult()
		{
			return (Token)this[LastResult];
		}

		/// <summary>
		/// Determines whether a certain key is contained in the context.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(string key)
		{
			return _values.ContainsKey(key);
		}

		#endregion
	}
}

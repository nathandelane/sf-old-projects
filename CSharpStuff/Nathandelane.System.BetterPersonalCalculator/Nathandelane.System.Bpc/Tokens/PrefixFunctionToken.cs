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

namespace Nathandelane.System.Bpc
{
	public class PrefixFunctionToken : FunctionToken, IOperation
	{
		#region Fields

		private static readonly Regex __functionPattern = new Regex("^(abs|cosh|sinh|tanh|cos|sin|tan|acos|asin|atan|log|ln|lb|ld|lg|deg|rad|sqrt|tod|toh|too|tob|-\\(|[!]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		#endregion

		#region Properties

		public OperationType OperationType
		{
			get { return OperationType.PrefixFunction; }
		}

		#endregion

		#region Constructors

		public PrefixFunctionToken(string value)
			: base(value)
		{
		}

		public PrefixFunctionToken(Token other)
			: base(other)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Attempts to parse a token and returns success or failure.
		/// </summary>
		/// <param name="line">String from which to take the next token.</param>
		/// <param name="token">Out parameter to send token to if successful.</param>
		/// <returns></returns>
		public static bool TryParse(string line, out Token token)
		{
			bool parseSuccessful = false;

			token = new NullToken();

			if ((token = Parse(line)) is PrefixFunctionToken)
			{
				parseSuccessful = true;
			}

			return parseSuccessful;
		}

		/// <summary>
		/// Gets a Token of type PrefixFunctionToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public new static Token Parse(string line)
		{
			Token token = new NullToken();

			if (PrefixFunctionToken.__functionPattern.IsMatch(line))
			{
				string matchText = PrefixFunctionToken.__functionPattern.Matches(line)[0].Value;

				if (matchText.Equals("-(", StringComparison.InvariantCultureIgnoreCase))
				{
					matchText = "-";
				}

				token = new PrefixFunctionToken(matchText);
			}

			return token;
		}

		#endregion
	}
}

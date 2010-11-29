/*
Nathan Lane, Nathandelane Copyright (C) 2010, Nathandelane.

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
	/// <summary>
	/// Description of SetContainerToken.
	/// </summary>
	public class SetContainerToken : Token
	{
		#region Fields

		private static readonly Regex __setContainerPattern = new Regex("^[\\[\\]]{1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private SetContainerTokenType _setContainerType;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the SetContainerType.
		/// </summary>
		public SetContainerTokenType SetContainerType
		{
			get { return _setContainerType; }
		}

		/// <summary>
		/// Gets the token type.
		/// </summary>
		public override TokenType Type
		{
			get { return TokenType.Number; }
		}

		/// <summary>
		/// Gets the expression precedence.
		/// </summary>
		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Number; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of SectContainerToken.
		/// </summary>
		/// <param name="value"></param>
		public SetContainerToken(string value)
			: base(value)
		{
		}

		/// <summary>
		/// Creates an instance of SetContainerToken.
		/// </summary>
		/// <param name="other"></param>
		public SetContainerToken(Token other)
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

			if ((token = Parse(line)) is SetContainerToken)
			{
				parseSuccessful = true;
			}

			return parseSuccessful;
		}

		/// <summary>
		/// Gets a Token of type SetContainerToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public new static Token Parse(string line)
		{
			Token token = new NullToken();

			if (SetContainerToken.__setContainerPattern.IsMatch(line))
			{
				string matchText = SetContainerToken.__setContainerPattern.Matches(line)[0].Value;

				token = new SetContainerToken(matchText);

				if (matchText.Equals("(", StringComparison.InvariantCultureIgnoreCase))
				{
					((SetContainerToken)token)._setContainerType = SetContainerTokenType.Open;
				}
				else
				{
					((SetContainerToken)token)._setContainerType = SetContainerTokenType.Closed;
				}
			}

			return token;
		}

		#endregion
	}
}

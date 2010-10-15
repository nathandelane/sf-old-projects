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
using System.Reflection;

namespace Nathandelane.System.Bpc
{
	public abstract class Token
	{
		#region Fields

		private string _value;

		#endregion

		#region Properties

		/// <summary>
		/// Gets this Token's token type.
		/// </summary>
		public abstract TokenType Type { get; }

		/// <summary>
		/// Gets this Token's precedence.
		/// </summary>
		public abstract ExpressionPrecedence Precedence { get; }

		/// <summary>
		/// Gets the length of the token value.
		/// </summary>
		public int Length
		{
			get { return _value.Length; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of Token.
		/// </summary>
		/// <param name="value"></param>
		protected Token(string value)
		{
			_value = value;
		}

		/// <summary>
		/// Creates an instance of Token.
		/// </summary>
		/// <param name="other"></param>
		protected Token(Token other)
		{
			_value = other._value;
		}

		#endregion

		#region Methods

		/// <summary>
		/// This method must be overridden by the derived type.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token Parse(string line)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns a string representation of this Token.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return _value;
		}

		#endregion
	}
}

﻿/*
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

namespace Nathandelane.System.Bpc
{
	public class NullToken : Token
	{
		#region Properties

		/// <summary>
		/// Gets this NullToken's token type.
		/// </summary>
		public override TokenType Type
		{
			get { return TokenType.Null; }
		}

		/// <summary>
		/// Gets this NullToken's precedence.
		/// </summary>
		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Number; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of NullToken.
		/// </summary>
		public NullToken()
			: base(String.Empty)
		{
		}

		#endregion
	}
}

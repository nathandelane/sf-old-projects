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

namespace Nathandelane.System.Bpc
{
	/// <summary>
	/// Defines the different types of operations that can occur.
	/// Infix means that it is a basic operator that requires two operands, a left and a right.
	/// InfixFunction means that it is considered a function, but works like an infix operator.
	/// PostfixFunction means that it is a function that is placed after its single operand.
	/// PrefixFunction means that it is a function that is placed before its singleoperand.
	/// </summary>
	public enum OperationType
	{
		Infix,
		InfixFunction,
		PostfixFunction,
		PrefixFunction
	}
}

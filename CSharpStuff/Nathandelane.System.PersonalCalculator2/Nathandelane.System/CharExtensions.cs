﻿/*  Copyright (C) 2009, Nathandelane.
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

namespace Nathandelane.System
{
	public static class CharExtensions
	{
		public static bool IsDigit(this char c)
		{
			return Char.IsDigit(c);
		}

		public static bool IsUpper(this char c)
		{
			return Char.IsUpper(c);
		}

		public static bool IsLower(this char c)
		{
			return Char.IsLower(c);
		}

		public static bool IsLetter(this char c)
		{
		    return Char.IsLetter(c);
		}

		public static char ToUpper(this char c)
		{
			return Char.ToUpper(c);
		}

		public static char ToLower(this char c)
		{
			return Char.ToLower(c);
		}
	}
}

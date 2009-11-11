using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator2
{
	internal sealed class Messages
	{
		public static readonly string StackEmptyException = @"For some reason there were not enough operands (numbers) to complete 
the calculation. Perhaps you forgot to suffix a hex number with 'h' 
or need a second number for an arithmetic operation such as +.";
		public static readonly string InteractiveBriefHelp = String.Format("Type ? to get help; l to view license; q to quit.{0}", Environment.NewLine);
		public static readonly string ShortVersion = String.Format("Version: {0}", Program.Version);
		public static readonly string ShortLicense = @"PC.NET, BPC, and Better Personal Calculator are all names used to describe this software which is copyrighted by 
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
";
		public static readonly string Help = @"Supported functionality: 
Decimal numbers; hexadecimal numbers ending with h like Fh or 23h
Arithmetic operators: +, -, *, /
Functions: ** (power), // (div), % (mod), ! (factorial), cos, acos, cosh, sin, asin, sinh, tan, atan, tanh, sqrt, tohx, todc
Constants: pi, e, $ (last result)
Parentheses: (, )
Reserved: ? (displays help); v (displays version); l (displays license); q (quits)";
		public static readonly string Usage = String.Format(@"Usage: {0} [OPTIONS] <expression>
{1}
Options:
--mode-degrees        Sets the calculator in degree mode.
--mode-radians        Sets the calculator in radian mode (default).
--help                Displays this help message.
--version             Displays the version of BCP currently running.
--license             Displays the current license information for BPC.
", Program.Name, Messages.ShortVersion);
		public static readonly string Copyright = "BPC - Better Personal Calculator, Copyright (C) 2009 Nathandelane";
		public static readonly string BpcConsoleTitle = "BPC.NET - Better Personal Calculator";
	}
}

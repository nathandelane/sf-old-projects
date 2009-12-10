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
	public abstract class Expression
	{
		#region Fields

		private ExpressionPrecedence _precedence;
		private Token _operation;
		private IList<Expression> _operands;

		#endregion

		#region Properties

		public ExpressionPrecedence Precedence
		{
			get { return _precedence; }
		}

		public Token Operation
		{
			get { return _operation; }
		}

		public IList<Expression> Operands
		{
			get { return _operands; }
		}

		#endregion

		#region Constructors

		protected Expression(ExpressionPrecedence precedence, Token operation, IList<Expression> operands)
		{
			_precedence = precedence;
			_operation = operation;
			_operands = operands;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Evaluates this expression.
		/// </summary>
		/// <returns></returns>
		public abstract Token Evaluate();

		public override string ToString()
		{
			string[] strOperands = new string[_operands.Count];

			for(int counter = 0; counter < strOperands.Length; counter++)
			{
				strOperands[counter] = _operands[counter].ToString();
			}

			return String.Concat(Operation.ToString(), " ", String.Join(" ", strOperands));
		}

		#endregion
	}
}

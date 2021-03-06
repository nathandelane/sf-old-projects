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
	public abstract class Expression
	{
		#region Fields

		private ExpressionPrecedence _precedence;
		private Token _operation;
		private IList<Expression> _operands;

		#endregion

		#region Properties

		/// <summary>
		/// Gets this Expression's precedence.
		/// </summary>
		public ExpressionPrecedence Precedence
		{
			get { return _precedence; }
		}

		/// <summary>
		/// Gets the operation of this expression.
		/// </summary>
		public Token Operation
		{
			get { return _operation; }
		}

		/// <summary>
		/// Gets a list of operands of this expression.
		/// </summary>
		public IList<Expression> Operands
		{
			get { return _operands; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of Expression.
		/// </summary>
		/// <param name="precedence"></param>
		/// <param name="operation"></param>
		/// <param name="operands"></param>
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

		/// <summary>
		/// Evaluates a sub-expression.
		/// </summary>
		/// <param name="operandIndex"></param>
		/// <returns></returns>
		protected Token EvaluateOperand(int operandIndex)
		{
			Token evaluation = _operands[operandIndex].Evaluate();

			if (evaluation is VariableToken)
			{
				CalculatorContext context = CalculatorContext.GetInstance();

				if (context.ContainsKey(evaluation.ToString()))
				{
					evaluation = context[evaluation.ToString()];
				}
				else
				{
					throw new MalformedExpressionException(String.Format("Variable named {0} is not defined", evaluation));
				}
			}

			return evaluation;
		}

		/// <summary>
		/// Returns a string representation of this Expression.
		/// </summary>
		/// <returns></returns>
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

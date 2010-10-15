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
	public static class Evaluator
	{
		#region Methods
		
		/// <summary>
		/// Performs an evaluation on an expression.
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string PerformEvaluation(string strExpression)
		{
			CalculatorContext context = CalculatorContext.GetInstance();
			string strResult = "0";
			ITokenizer tokenizer = new BpcTokenizer(strExpression);
			ITokenizer postfixTokenizer = new PostfixTokenizer(tokenizer);
			Expression expression = ExpressionYard.Formulate(postfixTokenizer);
			Token result = expression.Evaluate();

			if (result is VariableToken)
			{
				result = context[result.ToString()];
			}

			strResult = String.Format("{0}", result);

			if (result is NumberToken)
			{
				if (context[CalculatorContext.DisplayBase].ToString().Equals(DisplayBase.BinaryBase, StringComparison.InvariantCultureIgnoreCase))
				{
					strResult = ((NumberToken)result).AsBin();

					DisplayBase.ResetDisplayBase();
				}
				else if (context[CalculatorContext.DisplayBase].ToString().Equals(DisplayBase.OctalBase, StringComparison.InvariantCultureIgnoreCase))
				{
					strResult = ((NumberToken)result).AsOct();

					DisplayBase.ResetDisplayBase();
				}
				else if (context[CalculatorContext.DisplayBase].ToString().Equals(DisplayBase.HexadecimalBase, StringComparison.InvariantCultureIgnoreCase))
				{
					strResult = ((NumberToken)result).AsHex();

					DisplayBase.ResetDisplayBase();
				}
			}
			else if (!(result is BooleanToken))
			{
				throw new Exception(String.Format("Unexpected token was returned as result. Type: {0} value: {1}", result.Type, result));
			}

			context[CalculatorContext.LastResult] = result;

			return strResult;
		}
		
		#endregion
	}
}

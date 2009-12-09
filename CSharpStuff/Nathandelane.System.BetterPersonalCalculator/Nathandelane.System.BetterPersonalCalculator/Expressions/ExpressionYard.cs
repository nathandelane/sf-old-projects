using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	static class ExpressionYard
	{
		/// <summary>
		/// Forumulates the tokens contained in the tokenizer
		/// </summary>
		/// <param name="tokenizer"></param>
		/// <returns></returns>
		public static Expression Formulate(ITokenizer tokenizer)
		{
			Expression expression = default(Expression);

			if (tokenizer.HasTokens)
			{
				foreach (Token token in tokenizer.Tokens)
				{
					if (token is NumberToken || token is ConstantToken)
					{
						
					}
					else if (token is OperatorToken)
					{
						
					}
				}
			}

			return expression;
		}
	}
}

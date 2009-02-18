using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.Math.Processor.Tokens;

namespace Nathandelane.Math.Processor.Evaluation
{
	public class TokenParser
	{
		#region Public Methods

		public static Expression Parse(string expression)
		{
			Expression expressionObject = new Expression();
			IToken lastToken = new NullToken();
			bool nextNumberIsNegative = false;

			expression = RemoveSpaces(expression);

			while (expression.Length > 0)
			{
				IToken nextToken = GetNextToken(expression);

				if (IsNegation(lastToken, nextToken))
				{
					nextNumberIsNegative = true;
				}
				else if ((nextToken is Number) && nextNumberIsNegative)
				{
					string tokenValue = String.Concat("-", nextToken.Value);
					expression = String.Concat("-", expression);
					nextNumberIsNegative = false;
					nextToken = new Number(tokenValue);

					expressionObject.Remove(lastToken);
				}

				expressionObject.Add(nextToken);

				lastToken = nextToken;

				int tokenLength = nextToken.Value.Length;
				expression = expression.Substring(tokenLength);
			}

			return expressionObject;
		}

		#endregion

		#region Private Methods

		private static string RemoveSpaces(string expression)
		{
			string processedExpression = expression;

			while (processedExpression.Contains(" "))
			{
				processedExpression = processedExpression.Replace(" ", String.Empty);
			}

			return processedExpression;
		}

		private static bool IsNegation(IToken lastToken, IToken nextToken)
		{
			bool result = false;

			if(nextToken.Type == TokenType.Operator)
			{
				if ((lastToken.Type == TokenType.Operator) || (lastToken.Type == TokenType.Structure) || (lastToken.Type == TokenType.Null))
				{
					result = true;
				}
			}

			return result;
		}

		private static IToken GetNextToken(string expression)
		{
			IToken nextToken = null;

			var tokens = from t in AllTokens.Set
						 where t.Matches(expression)
						 select t;

			IToken firstMatchingToken = tokens.First<IToken>();

			if (firstMatchingToken is Number)
			{
				nextToken = new Number(firstMatchingToken.FirstMatch(expression));
			}
			else
			{
				nextToken = firstMatchingToken;
			}

			return nextToken;
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.Math.Processor.Tokens;

namespace Nathandelane.Math.Processor.Evaluation
{
	public class TokenParser
	{
		#region Fields

		private static IList<IToken> _expression;

		#endregion

		#region Properties

		public IList<IToken> Expression
		{
			get { return _expression; }
		}

		#endregion

		#region Constructors

		private TokenParser()
		{
			_expression = new List<IToken>();
		}

		#endregion

		#region Public Methods

		public static TokenParser Parse(string expression)
		{
			TokenParser tokenParser = new TokenParser();
			IToken lastToken = new NullToken();
			bool nextNumberIsNegative = false;

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

					Remove(lastToken);
				}

				Add(nextToken);

				lastToken = nextToken;

				int tokenLength = nextToken.Value.Length;
				expression = expression.Substring(tokenLength);
			}

			return tokenParser;
		}

		#endregion

		#region Private Methods

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

		private static void Add(IToken token)
		{
			_expression.Add(token);
		}

		private static void Remove(IToken token)
		{
			_expression.Remove(token);
		}

		private static void RemoveAt(int index)
		{
			_expression.RemoveAt(index);
		}

		private static int Count()
		{
			return _expression.Count;
		}

		#endregion
	}
}

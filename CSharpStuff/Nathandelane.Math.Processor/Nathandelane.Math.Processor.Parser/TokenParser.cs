using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.Math.Processor.Tokens;

namespace Nathandelane.Math.Processor.Parser
{
	public class TokenParser
	{
		#region Fields

		private static IList<IToken> _expression;

		#endregion

		#region Properties

		public IEnumerator<IToken> Expression
		{
			get { return _expression.GetEnumerator(); }
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

			while (expression.Length > 0)
			{
				IToken nextToken = GetNextToken(expression);

				Add(nextToken);

				int tokenLength = nextToken.Value.Length;
				expression = expression.Substring(tokenLength - 1);
			}

			return tokenParser;
		}

		#endregion

		#region Private Methods

		private static IToken GetNextToken(string expression)
		{
			IToken nextToken = null;

			var tokens = from t in AllTokens.Set
						 where t.Matches(expression)
						 select t;

			IToken firstMatchingToken = tokens.First<IToken>();

			if (firstMatchingToken is Number)
			{
				nextToken = TokenFactory.CreateToken<Number>(firstMatchingToken.FirstMatch(expression));
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

		#endregion
	}
}

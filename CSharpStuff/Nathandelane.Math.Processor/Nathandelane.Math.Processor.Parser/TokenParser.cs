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

			return tokenParser;
		}

		#endregion

		#region Private Methods

		private void Add(IToken token)
		{
			_expression.Add(token);
		}

		private void Remove(IToken token)
		{
			_expression.Remove(token);
		}

		private void RemoveAt(int index)
		{
			_expression.RemoveAt(index);
		}

		#endregion
	}
}

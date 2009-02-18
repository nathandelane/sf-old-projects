using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Math.Processor.Tokens
{
	public class TokenSet
	{
		#region Fields

		private static IList<IToken> _tokens = new List<IToken>()
		{
			new Number(0),
			new OpenPerenthesis(),
			new ClosePerenthesis(),
			new Multiplication(),
			new Division(),
			new Subtraction(),
			new Addition(),
			new Sine(),
			new Cosine(),
			new Tangent()
		};

		#endregion

		#region IEnumerable Members

		public static IEnumerator<IToken> GetEnumerator()
		{
			return _tokens.GetEnumerator();
		}

		#endregion
	}
}

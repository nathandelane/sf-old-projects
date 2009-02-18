using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Math.Processor.Tokens
{
	public class AllTokens
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

		#region Properties

		public static IList<IToken> Set
		{
			get { return _tokens; }
		}

		#endregion
	}
}

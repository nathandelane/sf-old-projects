using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class TokenListStackAdapter
	{
		#region Fields

		private IList<Token> _tokens;

		#endregion

		#region Properties

		public int Count
		{
			get { return _tokens.Count; }
		}

		#endregion

		#region Constructor

		public TokenListStackAdapter(IList<Token> tokens)
		{
			_tokens = tokens;
		}

		#endregion

		#region Methods

		public Token Pop()
		{
			Token token = _tokens[_tokens.Count - 1];
			
			_tokens.RemoveAt(_tokens.Count - 1);

			return token;
		}

		#endregion
	}
}

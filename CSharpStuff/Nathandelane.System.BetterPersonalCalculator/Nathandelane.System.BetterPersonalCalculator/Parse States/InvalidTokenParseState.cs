using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class InvalidTokenParseState : TokenParseState
	{
		#region Methods

		public override bool GetNextToken(string internalLine, out Token token, out TokenParseState state)
		{
			throw new InvalidTokenException(String.Format("Invalid or unrecognized token at {0}.", internalLine));
		}

		#endregion
	}
}

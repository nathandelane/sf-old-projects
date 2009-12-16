using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public abstract class TokenParseState
	{
		#region Methods

		public abstract bool GetNextToken(string internalLine, out Token token, out TokenParseState state);

		#endregion
	}
}

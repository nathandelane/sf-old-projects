using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public interface ITokenizer
	{
		IList<Token> Tokens;

		bool HasTokens;
	}
}

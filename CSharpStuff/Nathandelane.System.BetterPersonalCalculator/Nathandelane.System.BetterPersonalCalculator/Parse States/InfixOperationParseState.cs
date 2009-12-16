using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class InfixOperationParseState : TokenParseState
	{
		#region Methods

		public override bool GetNextToken(string internalLine, out Token token, out TokenParseState state)
		{
			bool result = false;

			if ((result = PrefixFunctionToken.TryParse(internalLine, out token)))
			{
				state = new PrefixFunctionTokenParseState();
			}
			else if((result = BooleanToken.TryParse(internalLine, out token) || ConstantToken.TryParse(internalLine, out token) || NumberToken.TryParse(internalLine, out token) || LastResultToken.TryParse(internalLine, out token)))
			{
				state = new ValueTokenParseState();
			}
			else if((result = VariableToken.TryParse(internalLine, out token)))
			{
				state = new VariableTokenParseState();
			}
			else if((result = (PerenthesisToken.TryParse(internalLine, out token) && ((PerenthesisToken)token).PerenthesisType == PerenthesisType.Open)))
			{
				state = new PerenthesisTokenParseState();
			}
			else if((result = CommentToken.TryParse(internalLine, out token)))
			{
				state = new NullTokenParseState();
			}
			else
			{
				state = new InvalidTokenParseState();
			}

			return result;
		}

		#endregion
	}
}

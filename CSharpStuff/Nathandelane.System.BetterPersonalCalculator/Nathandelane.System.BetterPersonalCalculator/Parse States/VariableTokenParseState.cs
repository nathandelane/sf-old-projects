using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class VariableTokenParseState : TokenParseState
	{
		#region Methods

		public override bool GetNextToken(string internalLine, out Token token, out TokenParseState state)
		{
			bool result = false;

			if ((result = PerenthesisToken.TryParse(internalLine, out token) && ((PerenthesisToken)token).PerenthesisType == PerenthesisType.Closed))
			{
				state = new PerenthesisTokenParseState();
			}
			else if((result = InfixFunctionToken.TryParse(internalLine, out token) || ArithmeticOperatorToken.TryParse(internalLine, out token) || BinaryOperatorToken.TryParse(internalLine, out token) || BooleanOperatorToken.TryParse(internalLine, out token) || AssignmentOperatorToken.TryParse(internalLine, out token)))
			{
				state = new InfixOperationParseState();
			}
			else if((result = PostfixFunctionToken.TryParse(internalLine, out token)))
			{
				state = new PostfixFunctionParseState();
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

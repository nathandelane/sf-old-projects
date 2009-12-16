using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NullTokenParseState : TokenParseState
	{
		#region Methods

		/// <summary>
		/// Gets the next Token if the last token was a NullToken. It first tries to get a normal,
		/// valid first token. If that fails, then it checks for a normal secondary token, and if 
		/// that passes then the context LastResultIsImplied is set to true.
		/// </summary>
		/// <param name="internalLine"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		public override bool GetNextToken(string internalLine, out Token token, out TokenParseState state)
		{
			bool result = false;

			if ((result = PerenthesisToken.TryParse(internalLine, out token)))
			{
				state = new PerenthesisTokenParseState();
			}
			else if ((result = PrefixFunctionToken.TryParse(internalLine, out token)))
			{
				state = new PrefixFunctionTokenParseState();
			}
			else if ((result = BooleanToken.TryParse(internalLine, out token) || ConstantToken.TryParse(internalLine, out token) || NumberToken.TryParse(internalLine, out token) || LastResultToken.TryParse(internalLine, out token)))
			{
				state = new ValueTokenParseState();
			}
			else if ((result = VariableToken.TryParse(internalLine, out token)))
			{
				state = new VariableTokenParseState();
			}
			else if ((result = InfixFunctionToken.TryParse(internalLine, out token) || ArithmeticOperatorToken.TryParse(internalLine, out token) || BinaryOperatorToken.TryParse(internalLine, out token) || BooleanOperatorToken.TryParse(internalLine, out token)))
			{
				state = new InfixOperationParseState();
				CalculatorContext.GetInstance().LastResultIsImplied = true;
			}
			else if ((result = PostfixFunctionToken.TryParse(internalLine, out token)))
			{
				state = new PostfixFunctionParseState();
				CalculatorContext.GetInstance().LastResultIsImplied = true;
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

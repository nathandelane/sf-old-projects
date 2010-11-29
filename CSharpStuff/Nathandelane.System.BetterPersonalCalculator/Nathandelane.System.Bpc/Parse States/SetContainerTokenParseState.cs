using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.Bpc
{
	public class SetContainerTokenParseState : TokenParseState
	{
		#region Methods
		
		/// <summary>
		/// Gets the next token while in the set container token parse state.
		/// </summary>
		/// <param name="internalLine"></param>
		/// <param name="token"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public override bool GetNextToken(string internalLine, out Token token, out TokenParseState state)
		{
			bool result = false;
			Token lastToken = CalculatorContext.GetInstance()[CalculatorContext.LastToken];

			if ((result = SetContainerToken.TryParse(internalLine, out token) && ((SetContainerToken)token).SetContainerType == ((SetContainerToken)lastToken).SetContainerType))
			{
				state = new SetContainerTokenParseState();
			}
			else if ((result = CommentToken.TryParse(internalLine, out token)))
			{
				state = new NullTokenParseState();
			}
			else if (((SetContainerToken)lastToken).SetContainerType == SetContainerTokenType.Open)
			{
				if ((result = PrefixFunctionToken.TryParse(internalLine, out token)))
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
				else
				{
					state = new InvalidTokenParseState();
				}
			}
			else if ((result = InfixFunctionToken.TryParse(internalLine, out token) || ArithmeticOperatorToken.TryParse(internalLine, out token) || BinaryOperatorToken.TryParse(internalLine, out token) || BooleanOperatorToken.TryParse(internalLine, out token)))
			{
				state = new InfixOperationParseState();
			}
			else if ((result = PostfixFunctionToken.TryParse(internalLine, out token)))
			{
				state = new PostfixFunctionParseState();
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

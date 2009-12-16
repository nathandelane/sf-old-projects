/*
Nathan Lane, Nathandelane Copyright (C) 2009, Nathandelane.

Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 3, or (at your option)
any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
02111-1307, USA.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class BpcTokenizer : ITokenizer
	{
		#region Fields

		private IList<Token> _tokens;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the token list.
		/// </summary>
		public IList<Token> Tokens
		{
			get { return _tokens; }
		}

		/// <summary>
		/// Gets whether this BpcTokenizer has tokens.
		/// </summary>
		public bool HasTokens
		{
			get { return _tokens != null && _tokens.Count > 0; }
		}

		#endregion

		#region Constructors

		public BpcTokenizer(string line)
		{
			_tokens = new List<Token>();

			ParseTokens(line);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Parses tokens out of the string given.
		/// </summary>
		/// <param name="line"></param>
		private void ParseTokens(string line)
		{
			string internalLine = line;
			Regex spaceRegex = new Regex("^[\\s]+", RegexOptions.Compiled | RegexOptions.CultureInvariant);
			TokenParseState tokenParseState = new NullTokenParseState();
			CalculatorContext context = CalculatorContext.GetInstance();

			while (!String.IsNullOrEmpty(internalLine))
			{
				Token token = new NullToken();
				bool tokenIsValid = false;

				if (spaceRegex.IsMatch(internalLine))
				{
					int length = spaceRegex.Matches(internalLine)[0].Value.Length;

					internalLine = RemoveToken(length, internalLine);
				}
				else
				{
					tokenIsValid = tokenParseState.GetNextToken(internalLine, out token, out tokenParseState);

					#region Old Conditional Code
					//    if (lastToken is NullToken)
				//    {
				//        if (GetFirstToken(internalLine, out token))
				//        {
				//            tokenIsValid = true;
				//        }
				//        else if (InfixFunctionToken.TryParse(internalLine, out token)
				//            || ArithmeticOperatorToken.TryParse(internalLine, out token)
				//            || BinaryOperatorToken.TryParse(internalLine, out token)
				//            || BooleanOperatorToken.TryParse(internalLine, out token)
				//            || PostfixFunctionToken.TryParse(internalLine, out token))
				//        {
				//            _tokens.Add(CalculatorContext.GetInstance()[CalculatorContext.LastResult]);

				//            lastToken = _tokens[0];
				//        }
				//        else
				//        {
				//            throw new InvalidTokenException(String.Format("Invalid or unrecognized token at {0}.", internalLine));
				//        }
				//    }
				//    else if (lastToken is VariableToken)
				//    {
				//        if ((PerenthesisToken.TryParse(internalLine, out token) && ((PerenthesisToken)token).PerenthesisType == PerenthesisType.Closed)
				//            || InfixFunctionToken.TryParse(internalLine, out token)
				//            || ArithmeticOperatorToken.TryParse(internalLine, out token)
				//            || BinaryOperatorToken.TryParse(internalLine, out token)
				//            || BooleanOperatorToken.TryParse(internalLine, out token)
				//            || PostfixFunctionToken.TryParse(internalLine, out token)
				//            || AssignmentOperatorToken.TryParse(internalLine, out token)
				//            || CommentToken.TryParse(internalLine, out token))
				//        {
				//            tokenIsValid = true;
				//        }
				//    }
				//    else if (lastToken is NumberToken || lastToken is BooleanToken)
				//    {
				//        if ((PerenthesisToken.TryParse(internalLine, out token) && ((PerenthesisToken)token).PerenthesisType == PerenthesisType.Closed) 
				//            || InfixFunctionToken.TryParse(internalLine, out token) 
				//            || ArithmeticOperatorToken.TryParse(internalLine, out token) 
				//            || BinaryOperatorToken.TryParse(internalLine, out token) 
				//            || BooleanOperatorToken.TryParse(internalLine, out token) 
				//            || PostfixFunctionToken.TryParse(internalLine, out token) 
				//            || CommentToken.TryParse(internalLine, out token))
				//        {
				//            tokenIsValid = true;
				//        }
				//    }
				//    else if (lastToken is PerenthesisToken)
				//    {
				//        if (PerenthesisToken.TryParse(internalLine, out token) && ((PerenthesisToken)token).PerenthesisType == ((PerenthesisToken)lastToken).PerenthesisType 
				//            || CommentToken.TryParse(internalLine, out token))
				//        {
				//            tokenIsValid = true;
				//        }
				//        else if (((PerenthesisToken)lastToken).PerenthesisType == PerenthesisType.Open)
				//        {
				//            if (PrefixFunctionToken.TryParse(internalLine, out token) 
				//                || BooleanToken.TryParse(internalLine, out token)
				//                || VariableToken.TryParse(internalLine, out token)
				//                || ConstantToken.TryParse(internalLine, out token) 
				//                || NumberToken.TryParse(internalLine, out token)
				//                || LastResultToken.TryParse(internalLine, out token))
				//            {
				//                tokenIsValid = true;
				//            }
				//        }
				//        else
				//        {
				//            if (InfixFunctionToken.TryParse(internalLine, out token) 
				//                || ArithmeticOperatorToken.TryParse(internalLine, out token) 
				//                || BinaryOperatorToken.TryParse(internalLine, out token) 
				//                || BooleanOperatorToken.TryParse(internalLine, out token)
				//                || PostfixFunctionToken.TryParse(internalLine, out token))
				//            {
				//                tokenIsValid = true;
				//            }
				//        }
				//    }
				//    else if (lastToken is PrefixFunctionToken 
				//        && ((PerenthesisToken.TryParse(internalLine, out token) && ((PerenthesisToken)token).PerenthesisType == PerenthesisType.Open)
				//            || lastToken.ToString().Equals("!", StringComparison.InvariantCultureIgnoreCase)
				//                && (BooleanToken.TryParse(internalLine, out token)
				//                || VariableToken.TryParse(internalLine, out token)
				//                || ConstantToken.TryParse(internalLine, out token)
				//                || NumberToken.TryParse(internalLine, out token))))
				//    {
				//        tokenIsValid = true;
				//    }
				//    else if (lastToken is InfixFunctionToken || lastToken is OperatorToken)
				//    {
				//        if (PrefixFunctionToken.TryParse(internalLine, out token) 
				//            || BooleanToken.TryParse(internalLine, out token)
				//            || VariableToken.TryParse(internalLine, out token)
				//            || ConstantToken.TryParse(internalLine, out token)
				//            || NumberToken.TryParse(internalLine, out token)
				//            || LastResultToken.TryParse(internalLine, out token)
				//            || (PerenthesisToken.TryParse(internalLine, out token) && ((PerenthesisToken)token).PerenthesisType == PerenthesisType.Open)
				//            || CommentToken.TryParse(internalLine, out token))
				//        {
				//            tokenIsValid = true;
				//        }
				//    }
				//    else if (lastToken is PostfixFunctionToken)
				//    {
				//        if ((PerenthesisToken.TryParse(internalLine, out token) && ((PerenthesisToken)token).PerenthesisType == PerenthesisType.Closed) 
				//            || InfixFunctionToken.TryParse(internalLine, out token) 
				//            || ArithmeticOperatorToken.TryParse(internalLine, out token) 
				//            || BinaryOperatorToken.TryParse(internalLine, out token) 
				//            || BooleanOperatorToken.TryParse(internalLine, out token) 
				//            || PostfixFunctionToken.TryParse(internalLine, out token)
				//            || CommentToken.TryParse(internalLine, out token))
				//        {
				//            tokenIsValid = true;
				//        }
				//    }
				//    else
				//    {
				//        throw new InvalidTokenException(String.Format("Invalid or unrecognized token at {0}.", internalLine));
					//    }
					#endregion

					if (tokenIsValid)
					{
						internalLine = AddTokenToCollection(token, internalLine);
						context[CalculatorContext.LastToken] = token;
					}
					else if (CalculatorContext.GetInstance().LastResultIsImplied)
					{
						_tokens.Add(CalculatorContext.GetInstance()[CalculatorContext.LastResult]);

						context[CalculatorContext.LastToken] = _tokens[0];

						CalculatorContext.GetInstance().LastResultIsImplied = false;
					}
				}
			}
		}

		/// <summary>
		/// Gets the first Token from the line.
		/// </summary>
		/// <param name="internalLine"></param>
		/// <returns></returns>
		private bool GetFirstToken(string internalLine, out Token token)
		{
			return PerenthesisToken.TryParse(internalLine, out token)
				|| PrefixFunctionToken.TryParse(internalLine, out token)
				|| BooleanToken.TryParse(internalLine, out token)
				|| VariableToken.TryParse(internalLine, out token)
				|| ConstantToken.TryParse(internalLine, out token)
				|| NumberToken.TryParse(internalLine, out token)
				|| LastResultToken.TryParse(internalLine, out token);
		}

		/// <summary>
		/// Determines whether the last token was an abnormal postfix function
		/// </summary>
		/// <param name="lastToken"></param>
		/// <returns></returns>
		private bool LastTokenWasPostfixFunction(Token lastToken)
		{
			bool result = false;

			if (lastToken.ToString().Equals("!", StringComparison.InvariantCultureIgnoreCase))
			{
				result = true;
			}

			return result;
		}

		/// <summary>
		/// Removes the token from the beginning of the line and returns the line.
		/// </summary>
		/// <param name="token"></param>
		/// <param name="line"></param>
		/// <returns></returns>
		private string AddTokenToCollection(Token token, string line)
		{
			string internalLine = line;

			if (token is NumberToken)
			{
				internalLine = RemoveToken(((NumberToken)token).Representation.Length, internalLine);
			}
			else if (token is BooleanToken)
			{
				internalLine = RemoveToken(((BooleanToken)token).Representation.Length, internalLine);
			}
			else
			{
				internalLine = RemoveToken(token.Length, internalLine);
			}

			_tokens.Add(token);

			return internalLine;
		}

		/// <summary>
		/// Removes the token from the line.
		/// </summary>
		/// <param name="length"></param>
		/// <param name="line"></param>
		/// <returns></returns>
		private string RemoveToken(int length, string line)
		{
			return line.Substring(length);
		}

		#endregion
	}
}

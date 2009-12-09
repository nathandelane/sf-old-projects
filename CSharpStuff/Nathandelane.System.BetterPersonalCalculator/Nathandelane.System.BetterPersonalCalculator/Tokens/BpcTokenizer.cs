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
			Token lastToken = new NullToken();
			string internalLine = line;
			Regex spaceRegex = new Regex("^[\\s]+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

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
					if (lastToken is NullToken || !(lastToken is NumberToken))
					{
						if ((token = ConstantToken.Parse(internalLine)) is ConstantToken)
						{
							tokenIsValid = true;
						}
						else if ((token = FunctionToken.Parse(internalLine)) is FunctionToken)
						{
							tokenIsValid = true;
						}
						else if ((token = PerenthesisToken.Parse(internalLine)) is PerenthesisToken)
						{
							tokenIsValid = true;
						}
						else if ((token = NumberToken.Parse(internalLine)) is NumberToken)
						{
							tokenIsValid = true;
						}
						else if ((token = CommentToken.Parse(internalLine)) is CommentToken)
						{
							tokenIsValid = false;
							internalLine = RemoveToken(token.Length, internalLine);
						}
						else if ((token = VariableToken.Parse(internalLine)) is VariableToken)
						{
							tokenIsValid = true;
						}
						else
						{
							throw new UnrecognizedTokenException(String.Format("Unrecognized token at {0}.", internalLine));
						}
					}
					else if ((token = ConstantToken.Parse(internalLine)) is ConstantToken)
					{
						tokenIsValid = true;
					}
					else if ((token = FunctionToken.Parse(internalLine)) is FunctionToken)
					{
						tokenIsValid = true;
					}
					else if ((token = OperatorToken.Parse(internalLine)) is OperatorToken)
					{
						tokenIsValid = true;
					}
					else if ((token = PerenthesisToken.Parse(internalLine)) is PerenthesisToken)
					{
						tokenIsValid = true;
					}
					else if ((token = CommentToken.Parse(internalLine)) is CommentToken)
					{
						tokenIsValid = false;
						internalLine = RemoveToken(token.Length, internalLine);
					}
					else if ((token = VariableToken.Parse(internalLine)) is VariableToken)
					{
						tokenIsValid = true;
					}
					else
					{
						throw new UnrecognizedTokenException(String.Format("Unrecognized token at {0}.", internalLine));
					}

					if (tokenIsValid)
					{
						internalLine = AddTokenToCollection(token, internalLine);
						lastToken = token;
					}
				}
			}
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

			internalLine = RemoveToken(token.Length, line);

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

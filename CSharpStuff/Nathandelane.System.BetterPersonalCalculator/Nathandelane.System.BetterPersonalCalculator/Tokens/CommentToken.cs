using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class CommentToken : Token
	{
		#region Fields

		private static readonly Regex __commentPattern = new Regex("^(#){1}.*", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		#endregion

		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Number; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Number; }
		}

		#endregion

		#region Constructors

		public CommentToken(string value)
			: base(value)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a Token of type CommentToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token Parse(string line)
		{
			Token token = new NullToken();

			if (CommentToken.__commentPattern.IsMatch(line))
			{
				string matchText = CommentToken.__commentPattern.Matches(line)[0].Value;

				token = new CommentToken(matchText);
			}

			return token;
		}

		#endregion
	}
}

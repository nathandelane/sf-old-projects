using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class PerenthesisToken : Token
	{
		#region Fields

		private static readonly Regex __perenPattern = new Regex("^[()]{1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

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

		public PerenthesisToken(string value)
			: base(value)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a Token of type PerenthesisToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token Parse(string line)
		{
			Token token = new NullToken();

			if (PerenthesisToken.__perenPattern.IsMatch(line))
			{
				string matchText = PerenthesisToken.__perenPattern.Matches(line)[0].Value;

				token = new PerenthesisToken(matchText);
			}

			return token;
		}

		#endregion
	}
}

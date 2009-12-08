using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NumberToken : Token
	{
		#region Fields

		private static readonly Regex __numberPattern = new Regex("^(-){0,1}([\\d]+([.]{1}[\\d]+){0,1}|[\\dA-Za-z]+(H|h){1}|[0-7]+(O|o){1}|[01]+(B|b){1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		#endregion

		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Number; }
		}

		#endregion

		#region Constructors

		public NumberToken()
			: base("0")
		{
		}

		public NumberToken(string value)
			: base(value)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a Token of type NumberToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token Parse(string line)
		{
			Token token = new NotANumberToken();

			if (NumberToken.__numberPattern.IsMatch(line))
			{
				string matchText = NumberToken.__numberPattern.Matches(line)[0].Value;

				token = new NumberToken(matchText);
			}

			return token;
		}

		#endregion
	}
}

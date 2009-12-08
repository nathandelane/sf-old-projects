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

		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Number; }
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
			Token token = new NullToken();

			if (NumberToken.__numberPattern.IsMatch(line))
			{
				string matchText = NumberToken.__numberPattern.Matches(line)[0].Value;

				token = new NumberToken(matchText);
			}

			return token;
		}

		/// <summary>
		/// Gets the whole part of the number.
		/// </summary>
		/// <returns></returns>
		public string WholePart()
		{
			string value = this.ToString();
			int index = -1;

			if ((index = value.IndexOf(".", StringComparison.InvariantCultureIgnoreCase)) > -1)
			{
				value = value.Substring(0, index);
			}

			return value;
		}

		/// <summary>
		/// Gets the fractional part of the number.
		/// </summary>
		/// <returns></returns>
		public string FractionalPart()
		{
			string value = this.ToString();
			int index = -1;

			if ((index = value.IndexOf(".", StringComparison.InvariantCultureIgnoreCase)) > -1)
			{
				value = value.Substring(index + 1);
			}

			return value;
		}

		#endregion
	}
}

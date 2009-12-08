using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class ConstantToken : NumberToken
	{
		#region Fields

		private static readonly Regex __constantPattern = new Regex("^(e|E|pi|PI|[$]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		#endregion

		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Constant; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Constant; }
		}

		#endregion

		#region Constructors

		public ConstantToken(string value)
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
		public new static Token Parse(string line)
		{
			Token token = new NullToken();

			if (ConstantToken.__constantPattern.IsMatch(line))
			{
				string matchText = ConstantToken.__constantPattern.Matches(line)[0].Value;

				if (matchText.Equals("e", StringComparison.InvariantCultureIgnoreCase))
				{
					token = new ConstantToken(Math.E.ToString());
				}
				else if (matchText.Equals("pi", StringComparison.InvariantCultureIgnoreCase))
				{
					token = new ConstantToken(Math.PI.ToString());
				}
				else if (matchText.Equals("$", StringComparison.InvariantCultureIgnoreCase))
				{
				}
			}

			return token;
		}

		#endregion
	}
}

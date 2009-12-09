using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class VariableToken : NumberToken
	{
		#region Fields

		private static readonly Regex __variablePattern = new Regex("^[A-Za-z_]{1}[A-Za-z_\\d]+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		#endregion

		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Variable; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { return ExpressionPrecedence.Variable; }
		}

		#endregion

		#region Constructors

		public VariableToken(string value)
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

			if (VariableToken.__variablePattern.IsMatch(line))
			{
				string matchText = VariableToken.__variablePattern.Matches(line)[0].Value;

				token = new VariableToken(matchText);
			}

			return token;
		}

		#endregion
	}
}

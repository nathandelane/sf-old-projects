using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class TokenYard
	{
		#region Fields

		private static readonly IDictionary<string, Regex> __regexes = new Dictionary<string, Regex>()
		{
			{ "numeric", new Regex(Numeric.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) },
			{ "division", new Regex(Division.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) },
			{ "multiplication", new Regex(Multiplication.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) },
			{ "subtraction", new Regex(Subtraction.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) },
			{ "addition", new Regex(Addition.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) }
		};

		private IEnumerable<IToken> _tokens;

		#endregion

		#region Properties

		public IEnumerable<IToken> Tokens
		{
			get { return _tokens; }
		}

		#endregion

		#region Constructors

		public TokenYard(string expression)
		{
			ParseTokens(expression);
		}

		#endregion

		#region Methods

		private void ParseTokens(string expression)
		{
		}

		private void PostFixateTokens()
		{
		}

		#endregion
	}
}

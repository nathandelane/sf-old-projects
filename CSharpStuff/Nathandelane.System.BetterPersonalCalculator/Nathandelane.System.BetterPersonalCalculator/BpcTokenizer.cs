using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Nathandelane.System.BetterPersonalCalculator.Configuration;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class BpcTokenizer
	{
		#region Fields

		private static readonly IDictionary<string, Token> _tokenTypes = new Dictionary<string, Token>()
		{
		};

		private IEnumerable<Token> _tokens;

		#endregion

		#region Constructors

		public BpcTokenizer(string line)
		{
			_tokens = new List<Token>();

			ParseTokens(line);
		}

		#endregion

		#region Methods

		private void Configure()
		{
		}

		private void ParseTokens(string line)
		{
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.System.ClassExtensions;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Evaluator
	{
		#region Fields

		private static readonly string[] __regexStrings = new string[]
		{
			Numeric.MatchExpression,
			Division.MatchExpression,
			Multiplication.MatchExpression,
			Subtraction.MatchExpression,
			Addition.MatchExpression
		};

		private IExpression _expressionTree;

		#endregion

		#region Constructors

		public Evaluator(string expression)
		{
		}

		#endregion

		#region Methods

		private IEnumerable<IToken> Tokenize(string expression)
		{
			IEnumerable<IToken> tokens = new List<IToken>();

			return tokens;
		}

		public IExpression evaluate(IDictionary<string, IExpression> context)
		{
			return _expressionTree.Calculate(context);
		}

		#endregion
	}
}

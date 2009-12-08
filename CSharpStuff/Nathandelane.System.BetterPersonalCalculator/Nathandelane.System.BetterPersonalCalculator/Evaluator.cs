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
		private static readonly IDictionary<string, Regex> __regexes = new Dictionary<string, Regex>()
		{
			{ "numeric", new Regex(Numeric.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) },
			{ "division", new Regex(Division.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) },
			{ "multiplication", new Regex(Multiplication.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) },
			{ "subtraction", new Regex(Subtraction.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) },
			{ "addition", new Regex(Addition.MatchExpression, RegexOptions.Compiled | RegexOptions.CultureInvariant) }
		};

		private IExpression _expressionTree;

		#endregion

		#region Constructors

		public Evaluator(string expression)
		{
			Stack<IExpression> expressionStack = new Stack<IExpression>();
			string[] tokens = expression.Tokenize(Evaluator.__regexStrings);

			foreach (string nextToken in tokens)
			{
				if (Evaluator.__regexes["numeric"].IsMatch(nextToken))
				{
					expressionStack.Push(new Numeric(nextToken));
				}
				else if (Evaluator.__regexes["division"].IsMatch(nextToken))
				{
					IExpression subExpression = new Division(expressionStack.Pop(), expressionStack.Pop());
					expressionStack.Push(subExpression);
				}
				else if (Evaluator.__regexes["multiplication"].IsMatch(nextToken))
				{
					IExpression subExpression = new Multiplication(expressionStack.Pop(), expressionStack.Pop());
					expressionStack.Push(subExpression);
				}
				else if (Evaluator.__regexes["subtraction"].IsMatch(nextToken))
				{
					IExpression subExpression = new Subtraction(expressionStack.Pop(), expressionStack.Pop());
					expressionStack.Push(subExpression);
				}
				else if (Evaluator.__regexes["addition"].IsMatch(nextToken))
				{
					IExpression subExpression = new Addition(expressionStack.Pop(), expressionStack.Pop());
					expressionStack.Push(subExpression);
				}
			}

			_expressionTree = expressionStack.Pop();
		}

		#endregion

		#region Methods

		public IExpression evaluate(IDictionary<string, IExpression> context)
		{
			return _expressionTree.Calculate(context);
		}

		#endregion
	}
}

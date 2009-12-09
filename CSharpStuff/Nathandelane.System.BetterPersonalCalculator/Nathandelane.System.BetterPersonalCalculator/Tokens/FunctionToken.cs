using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class FunctionToken : Token
	{
		#region Fields

		private static readonly Regex __functionPattern = new Regex("^(cos|sin|tan|acos|asin|atan|sqrt|[*]{2}|[/]{2}|[%]{1}|[!]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __singleArgFunction = new Regex("^(cos|sin|tan|acos|asin|atan|sqrt|[!]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex __twoArgFunction = new Regex("^([*]{2}|[/]{2}|[%]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private ExpressionPrecedence _precedence;
		private int _numArguments;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the TokenType.
		/// </summary>
		public override TokenType Type
		{
			get { return TokenType.Function; }
		}

		/// <summary>
		/// Gets the ExpressionPrecedence.
		/// </summary>
		public override ExpressionPrecedence Precedence
		{
			get { return _precedence; }
		}

		/// <summary>
		/// Gets the number of arguments required for the function.
		/// </summary>
		public int NumArguments
		{
			get { return _numArguments; }
		}

		#endregion

		#region Constructors

		public FunctionToken(string value)
			: base(value)
		{
			_precedence = ExpressionPrecedence.Function;

			if (FunctionToken.__singleArgFunction.IsMatch(value))
			{
				_numArguments = 1;
			}
			else if (FunctionToken.__twoArgFunction.IsMatch(value))
			{
				_numArguments = 2;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a Token of type FunctionToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token Parse(string line)
		{
			Token token = new NullToken();

			if (FunctionToken.__functionPattern.IsMatch(line))
			{
				string matchText = FunctionToken.__functionPattern.Matches(line)[0].Value;

				token = new FunctionToken(matchText);
			}

			return token;
		}

		#endregion
	}
}

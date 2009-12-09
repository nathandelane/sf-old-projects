using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class OperatorToken : Token
	{
		#region Fields

		private static readonly Regex __operatorPattern = new Regex("^([-]{1}|[+]{1}|[*]{1}|[/]{1}){1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private ExpressionPrecedence _precedence;

		#endregion

		#region Properties

		public override TokenType Type
		{
			get { return TokenType.Operator; }
		}

		public override ExpressionPrecedence Precedence
		{
			get { return _precedence; }
		}

		#endregion

		#region Constructors

		public OperatorToken(string value)
			: base(value)
		{
			DeterminePrecedence(value);
		}

		public OperatorToken(Token other)
			: base(other)
		{
			DeterminePrecedence(other.ToString());
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a Token of type OperatorToken from the beginning of a line of text.
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public static Token Parse(string line)
		{
			Token token = new NullToken();

			if (OperatorToken.__operatorPattern.IsMatch(line))
			{
				string matchText = OperatorToken.__operatorPattern.Matches(line)[0].Value;

				token = new OperatorToken(matchText);
			}

			return token;
		}

		private void DeterminePrecedence(string value)
		{
			if (value.Equals("+"))
			{
				_precedence = ExpressionPrecedence.Add;
			}
			else if(value.Equals("-"))
			{
				_precedence = ExpressionPrecedence.Subtract;
			}
			else if (value.Equals("*"))
			{
				_precedence = ExpressionPrecedence.MultiplyOrDivide;
			}
			else if (value.Equals("/"))
			{
				_precedence = ExpressionPrecedence.MultiplyOrDivide;
			}
		}

		#endregion
	}
}

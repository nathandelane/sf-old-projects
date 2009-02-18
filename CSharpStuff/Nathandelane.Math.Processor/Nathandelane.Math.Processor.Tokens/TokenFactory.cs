using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Math.Processor.Tokens
{
	public static class TokenFactory
	{
		#region Factory Methods

		public static IToken CreateToken<TType>()
		{
			IToken nextToken = null;
			TType token = default(TType);

			if (token is IToken)
			{
				if (nextToken is Addition)
				{
					nextToken = new Addition();
				}
				else if (nextToken is ClosePerenthesis)
				{
					nextToken = new ClosePerenthesis();
				}
				else if (nextToken is Division)
				{
					nextToken = new Division();
				}
				else if (nextToken is Multiplication)
				{
					nextToken = new Multiplication();
				}
				else if (nextToken is OpenPerenthesis)
				{
					nextToken = new OpenPerenthesis();
				}
				else if (nextToken is Subtraction)
				{
					nextToken = new Subtraction();
				}
				else if (nextToken is Cosine)
				{
					nextToken = new Cosine();
				}
				else if (nextToken is Sine)
				{
					nextToken = new Sine();
				}
				else if (nextToken is Tangent)
				{
					nextToken = new Tangent();
				}
			}

			return nextToken;
		}

		public static IToken CreateToken<TType>(string value)
		{
			IToken nextToken = null;
			TType token = default(TType);

			if (token is IToken)
			{
				nextToken = new Number(value);
			}

			return nextToken;
		}

		public static IToken CreateToken<TType>(int value)
		{
			IToken nextToken = null;
			TType token = default(TType);

			if (token is IToken)
			{
				nextToken = new Number(value);
			}

			return nextToken;
		}

		public static IToken CreateToken<TType>(long value)
		{
			IToken nextToken = null;
			TType token = default(TType);

			if (token is IToken)
			{
				nextToken = new Number(value);
			}

			return nextToken;
		}

		public static IToken CreateToken<TType>(double value)
		{
			IToken nextToken = null;
			TType token = default(TType);

			if (token is IToken)
			{
				nextToken = new Number(value);
			}

			return nextToken;
		}

		#endregion
	}
}

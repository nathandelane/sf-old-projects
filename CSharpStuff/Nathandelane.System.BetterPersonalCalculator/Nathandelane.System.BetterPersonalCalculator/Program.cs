using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	class Program
	{
		private CalculatorContext _context;

		private Program(string[] args)
		{
			if (args.Length > 0)
			{
				_context = CalculatorContext.GetInstance();

				ITokenizer tokenizer = new BpcTokenizer("3 + 4 * 2 / (1 - 5) ** 2 ** 3 # This is an example from Wikipedia.");
				Expression expression = ExpressionYard.Formulate(tokenizer);
				Token result = expression.Evaluate();
			}
			else
			{
				DisplayUsage();
			}
		}

		private void DisplayUsage()
		{
			Console.WriteLine("Usage: bpc <options> <mathematical-expression>");
		}

		static void Main(string[] args)
		{
			Program program = new Program(args);
		}
	}
}

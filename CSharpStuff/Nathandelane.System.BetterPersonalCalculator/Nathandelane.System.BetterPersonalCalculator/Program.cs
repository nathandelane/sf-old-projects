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

		private Program()
		{
			_context = CalculatorContext.GetInstance();

			ITokenizer tokenizer = new BpcTokenizer("3+4-2+13*9");
			Expression expression = ExpressionYard.Formulate(tokenizer);
			Token result = expression.Evaluate();
		}

		static void Main(string[] args)
		{
			Program program = new Program();
		}
	}
}

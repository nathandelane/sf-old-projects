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

			BpcTokenizer tokenizer = new BpcTokenizer("-24 + 12 - 13 +42--14");
		}

		static void Main(string[] args)
		{
			Program program = new Program();
		}
	}
}

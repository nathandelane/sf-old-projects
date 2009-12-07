using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			IExpression leftNumber = new Numeric("12");
			IExpression rightNumber = new Numeric("13");
			IExpression additionExp = new Addition(leftNumber, rightNumber);

			Console.WriteLine("{0} + {1} = {2}", leftNumber.Calculate(), rightNumber.Calculate(), additionExp.Calculate());
		}
	}
}

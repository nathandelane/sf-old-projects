using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Nathandelane.System.ClassExtensions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			IDictionary<string, IExpression> vars = new Dictionary<string, IExpression>();
			Console.WriteLine("Please enter a mathematical expression");

			string userInput = Console.ReadLine();
			Evaluator evalutaor = new Evaluator(userInput);

			Console.WriteLine("{0}", evalutaor.evaluate(vars).ToString());
		}
	}
}

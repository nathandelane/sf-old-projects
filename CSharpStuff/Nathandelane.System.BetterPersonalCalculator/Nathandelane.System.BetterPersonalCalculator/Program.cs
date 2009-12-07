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
			string[] regularExpressionList = new string[]
			{
				Numeric.MatchExpression,
				Multiplication.MatchExpression,
				Division.MatchExpression,
				Subtraction.MatchExpression,
				Addition.MatchExpression
			};

			Console.WriteLine("Please enter a mathematical expression");

			string userInput = Console.ReadLine();
			string[] tokens = userInput.Tokenize(regularExpressionList);

			foreach (string next in tokens)
			{
				
			}
		}
	}
}

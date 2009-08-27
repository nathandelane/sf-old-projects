using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator
{
	public class Calculator
	{
		#region Fields

		private static readonly string __quitOperation = "q";

		private Equation _equation;

		#endregion

		#region Constructors

		private Calculator()
		{
		}

		private Calculator(string[] args)
		{
			_equation = Equation.Parse(String.Join("", args));
		}

		#endregion

		#region Methods

		public static void Run(string[] args)
		{
			Calculator calc = null;

			if (args.Length > 0)
			{
				calc = new Calculator(args);

				Console.WriteLine("{0}", calc.Evaluate());
			}
			else
			{
				calc = new Calculator();

				string userInput = String.Empty;
				while (!userInput.Equals(__quitOperation))
				{
				}
			}
		}

		private double Evaluate()
		{
			double result = 0.0d;

			return result;
		}

		#endregion
	}
}

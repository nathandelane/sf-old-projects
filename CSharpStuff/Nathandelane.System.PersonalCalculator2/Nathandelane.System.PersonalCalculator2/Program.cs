using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.System;

namespace Nathandelane.System.PersonalCalculator2
{
	class Program
	{
		static void Main(string[] args)
		{
			string expression = "156+23.2--27";
			string[] patterns = new string[] { "^[-]{0,1}[\\d]+([.]{1}[\\d]+){0,1}", "^[+]{1}", "^[-]{1}" };

			string[] tokens = expression.Tokenize(patterns);

			foreach (string token in tokens)
			{
				Console.WriteLine("{0}", token);
			}

			Console.ReadLine();
		}
	}
}

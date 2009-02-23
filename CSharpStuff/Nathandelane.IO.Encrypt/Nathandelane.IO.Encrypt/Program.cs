using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Encrypt
{
	class Program
	{
		private Program(Input userInput)
		{
		}

		static void Main(string[] args)
		{
			using (Input userInput = Input.CheckAndSanitizeInput(args))
			{
				if (userInput.IsValid)
				{
					new Program(userInput);
				}
				else
				{
					Input.Help();
				}
			}
		}
	}
}

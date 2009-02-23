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
			if (userInput.Algorithm == EncryptionAlgorithm.Xor)
			{
				Xor xorAlgorithm = new Xor(userInput.Key, userInput.FilePath);
				xorAlgorithm.Run();
			}
		}

		static void Main(string[] args)
		{
			try
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
			catch (ArgumentException ex)
			{
				Console.WriteLine("Invalid argument for {0}", ex.Message);
			}
		}
	}
}

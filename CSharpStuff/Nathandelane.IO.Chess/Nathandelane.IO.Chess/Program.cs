using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.IO.Chess
{
	class Program
	{
		static void Main(string[] args)
		{
			Game chessGame = new Game();
			chessGame.Start();

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}
	}
}

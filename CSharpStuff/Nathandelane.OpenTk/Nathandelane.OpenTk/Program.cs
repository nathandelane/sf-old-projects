using System;
using System.Collections.Generic;

namespace Nathandelane.Otk
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			GameWindow gw = new GameWindow(800, 600);
			gw.Run();
		}
	}
}
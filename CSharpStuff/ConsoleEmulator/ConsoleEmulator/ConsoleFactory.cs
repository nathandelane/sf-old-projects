using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleEmulator
{
	public class ConsoleFactory
	{
		#region Fields

		private static ConsoleFactory __instance;
		private ConsoleCollection _consoleCollection;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of ConsoleFactory.
		/// </summary>
		private ConsoleFactory()
		{
			_consoleCollection = new ConsoleCollection();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets an instance of the console factory.
		/// </summary>
		/// <returns></returns>
		public static ConsoleFactory GetFactory()
		{
			if (ConsoleFactory.__instance == null)
			{
				ConsoleFactory.__instance = new ConsoleFactory();
			}

			return ConsoleFactory.__instance;
		}

		/// <summary>
		/// Creates a command console.
		/// </summary>
		/// <returns></returns>
		public Console CreateCmdConsole()
		{
			Console newCmdConsole = new Console(ConsoleType.Cmd);

			_consoleCollection.Add(newCmdConsole);

			return newCmdConsole;
		}

		/// <summary>
		/// Creates a cygwin console.
		/// </summary>
		/// <returns></returns>
		public Console CreateCygWinConsole()
		{
			Console newCygWinConsole = new Console(ConsoleType.CygWin);

			_consoleCollection.Add(newCygWinConsole);

			return newCygWinConsole;
		}

		/// <summary>
		/// Creates a powershell console.
		/// </summary>
		/// <returns></returns>
		public Console CreatePowerShellConsole()
		{
			Console newPsConsole = new Console(ConsoleType.PowerShell);

			_consoleCollection.Add(newPsConsole);

			return newPsConsole;
		}

		#endregion
	}
}

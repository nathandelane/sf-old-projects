using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Configuration;

namespace ConsoleEmulator
{
	public class Console
	{
		#region Fields

		public const string CmdExecutionStringKey = "CmdExecutionString";
		public const string CmdArgumentsKey = "CmdArguments";
		public const string CygWinExecutionStringKey = "CygWinExecutionString";
		public const string CygWinArgumentsKey = "CygWinArguments";
		public const string PowerShellExecutionStringKey = "PowerShellExecutionString";

		private Process _process;
		private string _executionString;
		private string _arguments;
		private ConsoleType _type;

		#endregion

		#region Properties

		/// <summary>
		/// Gets this console's type.
		/// </summary>
		public ConsoleType Type
		{
			get { return _type; }
		}

		/// <summary>
		/// Gets the output stream for the console.
		/// </summary>
		public StreamReader OutputStream
		{
			get { return _process.StandardOutput; }
		}

		/// <summary>
		/// Gets the input stream for the console
		/// </summary>
		public StreamWriter InputStream
		{
			get { return _process.StandardInput; }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Creates an instance of Console.
		/// </summary>
		/// <param name="consoleType"></param>
		public Console(ConsoleType consoleType)
		{
			_type = consoleType;

			if (consoleType == ConsoleType.Cmd)
			{
				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[Console.CmdExecutionStringKey]))
				{
					_executionString = ConfigurationManager.AppSettings[Console.CmdExecutionStringKey];
					_arguments = ConfigurationManager.AppSettings[Console.CmdArgumentsKey];
				}
				else
				{
					throw new ConfigurationErrorsException("Cmd is not configured.");
				}
			}
			else if (consoleType == ConsoleType.CygWin)
			{
				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[Console.CygWinExecutionStringKey]))
				{
					_executionString = ConfigurationManager.AppSettings[Console.CygWinExecutionStringKey];
					_arguments = ConfigurationManager.AppSettings[Console.CygWinArgumentsKey];
				}
				else
				{
					throw new ConfigurationErrorsException("CygWin is not configured.");
				}
			}
			else if (consoleType == ConsoleType.PowerShell)
			{
				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[Console.PowerShellExecutionStringKey]))
				{
					_executionString = ConfigurationManager.AppSettings[Console.PowerShellExecutionStringKey];
				}
				else
				{
					throw new ConfigurationErrorsException("PowerShell is not configured.");
				}
			}
			else
			{
				throw new ArgumentException("ConsoleType is not of a recognized or configured type");
			}

			_process = Process.Start(_executionString, _arguments);
			_process.StartInfo.CreateNoWindow = true;
			_process.StartInfo.UseShellExecute = false;
			_process.StartInfo.RedirectStandardInput = true;
			_process.StartInfo.RedirectStandardError = true;
			_process.StartInfo.RedirectStandardOutput = true;
			_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			_process.Start();
		}

		#endregion
	}
}

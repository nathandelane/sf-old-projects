using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.HttpGrep
{
	/// <summary>
	/// This is the entry point for HTTP Grep.
	/// </summary>
	class Program
	{
		#region Fields

		private Context _context;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor for Program.
		/// </summary>
		/// <param name="args"></param>
		private Program(string[] args)
		{
			try
			{
				_context = Context.SetContext(args);
			}
			catch (ArgumentException e)
			{
				Console.WriteLine(String.Format("{2}An Error occurred. {1}", e.GetType(), e.Message, Environment.NewLine));
			}
		}

		#endregion

		#region Entry Point

		/// <summary>
		/// Main method. Program entry point.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				Program httpGrep = new Program(args);
				httpGrep.Run();
			}
			else
			{
				Program.DispalyHelp();
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Runs the process defined by the context.
		/// </summary>
		private void Run()
		{
			if (_context == null || _context.ArgumentIsDefined(Context.Help))
			{
				Program.DispalyHelp();
			}
			else if (_context.ArgumentIsDefined(Context.Url))
			{
				Agent agent = new Agent();
				agent.Run();
			}
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Displays help.
		/// </summary>
		private static void DispalyHelp()
		{
			Console.WriteLine("{0}", Context.GeneralHelp);
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Reflection;

namespace Nathandelane.Math.PersonalCalculator
{
    /// <summary>
    /// Program is the entry point for Personal Calculator .NET.
    /// </summary>
	class Program
	{
        /// <summary>
        /// Program entry point. Command line arguments may be supplied, in which case they are immediately evaluated and the user is 
        /// returned to the system shell. If no command line arguments are supplied or the command line argument for interactive shell 
        /// is supplied, then Personal Calculator goes into an interactive mode.
        /// </summary>
        /// <param name="args"></param>
		static void Main(string[] args)
		{
            if (args.Length == 0)
            {
                Calculator.Run();
            }
            else if (args[0].Equals("-i") || args[0].Equals("--interactive"))
            {
                Calculator.Run();
            }
            else
            {
                string arguments = String.Join("", args);

                using (Calculator staticCalculator = new Calculator(arguments))
                {
                }
            }
		}
	}
}

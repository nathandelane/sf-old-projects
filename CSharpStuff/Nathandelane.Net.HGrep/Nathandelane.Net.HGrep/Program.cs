using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.HGrep
{
	class Program
	{
		#region Fields

		private ArgumentCollection _arguments;

		#endregion

		#region Constructors

		private Program(string[] args)
		{
			_arguments = ArgumentCollection.Parse(args);

			if (_arguments.Contains(Argument.Help))
			{
				Console.WriteLine("HGrep --url=<fully qualified url> [--find=<xpath expression>]");
			}
		}

		#endregion

		static void Main(string[] args)
		{
			new Program(args);
		}
	}
}

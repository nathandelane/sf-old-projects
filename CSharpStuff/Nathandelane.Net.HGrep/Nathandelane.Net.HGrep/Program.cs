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
		}

		#endregion

		static void Main(string[] args)
		{
			new Program(args);
		}
	}
}

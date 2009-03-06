using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Analyzer
{
	public interface IAnalyzer
	{
		#region Properties

		string Location { get; }

		#endregion

		#region Methods

		void Run();

		#endregion
	}
}

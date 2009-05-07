using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ReplaceAll
{
	public class Replacement
	{
		#region Properties

		public string Name { get; set; }

		public string OldValue { get; set; }

		public string NewValue { get; set; }

		public List<string> FilePaths { get; set; }

		#endregion

		#region Constructors

		public Replacement()
		{
			FilePaths = new List<string>();
		}

		#endregion
	}
}

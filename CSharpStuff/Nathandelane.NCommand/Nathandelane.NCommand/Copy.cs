using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.NCommand
{
	internal class Copy
	{
		private string _source;
		private string _destination;
		private List<string> _arguments;

		public string Source
		{
			get { return _source; }
		}

		public string Destination
		{
			get { return _destination; }
		}

		public List<string>.Enumerator Arguments
		{
			get { return _arguments.GetEnumerator(); }
		}

		public Copy(string[] arguments)
		{
			_source = arguments[0];
		}

		public int Run()
		{
			int retVal = 0;

			/* PDL.
			 * 
			 * 
			 */

			return retVal;
		}

		private bool SourceFileExists()
		{
			return File.Exists(_source);
		}

		private bool SourceDirectoryExists()
		{
			return Directory.Exists(_source);
		}
	}
}

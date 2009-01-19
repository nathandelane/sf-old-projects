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

			if (arguments.Length > 1)
			{
				switch (arguments.Length)
				{
					case 2:
						_destination = arguments[1];
						break;
					default:
						_arguments = new List<string>();
						for (int i = 2; i < arguments.Length; i++)
						{
							_arguments.Add(arguments[i]);
						}
						break;
				}
			}
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

		protected bool SourceFileExists()
		{
			return File.Exists(_source);
		}

		protected bool SourceDirectoryExists()
		{
			return Directory.Exists(_source);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Concatenate
{
	public class OptionMap
	{
		private readonly Dictionary<string, string> _options = new Dictionary<string, string>()
		{
			{ "--number-nonblank", "b" },
			{ "--show-ends", "E" },
			{ "--show-tabs", "T" },
			{ "--number", "n" },
			{ "-b", "b" },
			{ "-E", "E" },
			{ "-T", "T" },
			{ "-n", "n" }
		};

		public string this[string key]
		{
			get { return _options[key]; }
		}

		public bool OptionIsValid(string key)
		{
			bool result = false;

			if (_options.ContainsKey(key))
			{
				result = true;
			}

			return result;
		}
	}
}

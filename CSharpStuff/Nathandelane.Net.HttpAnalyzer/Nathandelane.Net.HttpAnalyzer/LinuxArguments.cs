using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Net.HttpAnalyzer
{
	public class LinuxArguments
	{
		private Dictionary<string, object> _args;

		public string[] Args
		{
			set { _args = ParseArguments(value); }
		}

		public object this[string key]
		{
			get { return _args[key]; }
		}

		public LinuxArguments(string[] args)
		{
			_args = new Dictionary<string, object>();
		}

		private Dictionary<string, object> ParseArguments(string[] args)
		{
			List<string> keys = new List<string>();
			List<string> values = new List<string>();

			foreach (string arg in args)
			{
				if (arg.StartsWith("-"))
				{
					keys.Add(arg);
				}
				else
				{
					values.Add(arg);
				}
			}

			return _args;
		}

		private string Preprocess(string arg)
		{
			string result = String.Empty;

			if (arg.StartsWith("\""))
			{
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.NCommand
{
	public class CommandLineArgument
	{
		private string _name;
		private object _argType;
		private string _description;

		public string Name
		{
			get { return _name; }
		}

		public object ArgType
		{
			get { return _argType; }
		}

		public string Description
		{
			get { return _description; }
		}

		public CommandLineArgument(string name, object argType, string description)
		{
			_name = name;
			_argType = argType;
			_description = description;
		}
	}
}

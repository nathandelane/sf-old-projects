using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.ListSegments
{
	public class Option
	{
		#region Fields

		private string _name;
		private List<string> _value;

		#endregion

		#region Properties

		public string Name
		{
			get { return _name; }
		}

		public List<string> Value
		{
			get { return _value; }
		}

		#endregion

		#region Constructors

		public Option(string name, List<string> value)
		{
			_name = name;
			_value = value;
		}

		#endregion
	}
}

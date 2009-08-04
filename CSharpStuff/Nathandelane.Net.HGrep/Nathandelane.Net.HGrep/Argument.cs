using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.HGrep
{
	public class Argument
	{
		#region Fields

		private string _name;
		private object _value;

		#endregion

		#region Properties

		public string Name
		{
			get { return _name; }
		}

		public object Value
		{
			get { return _value; }
		}

		#endregion

		#region Constructors

		public Argument(string name, object value)
		{
			_name = name;
			_value = value;
		}

		#endregion

		#region Methods

		public bool Equals(Argument argument)
		{
			return (argument.Name.Equals(this.Name) && (argument.Value == argument.Value));
		}

		#endregion

		#region Static Members

		public static readonly Argument Help = new Argument("help", null);

		#endregion
	}
}

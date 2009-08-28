using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator
{
	public class State
	{
		#region Fields

		private Dictionary<string, string> __variables;

		#endregion

		#region Properties

		public string this[string key]
		{
			get
			{
				string result = String.Empty;

				if (__variables.ContainsKey(key))
				{
					result = __variables[key];
				}
				else
				{
					Console.WriteLine("The variable {0} does not exists.", key);
				}

				return result;
			}
			set
			{
				if (__variables.ContainsKey(key))
				{
					__variables[key] = value;
				}
				else
				{
					__variables.Add(key, value);
				}
			}
		}

		#endregion

		#region Constructor

		public State()
		{
			__variables = new Dictionary<string, string>();
			__variables["$"] = "0";
		}

		#endregion
	}
}

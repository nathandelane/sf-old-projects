using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class AdminElement : ConfigurationElement
	{
		#region Properties

		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("value", IsRequired = true)]
		public string Value
		{
			get { return (string)this["value"]; }
			set { this["value"] = value; }
		}

		#endregion

		#region Constructors

		public AdminElement()
		{
		}

		public AdminElement(string name, string ipMask)
		{
			this.Name = name;
			this.Value = ipMask;
		}

		#endregion
	}
}

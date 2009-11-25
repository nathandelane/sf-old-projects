using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class HatElement : ConfigurationElement
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

		public HatElement()
		{
		}

		public HatElement(string name, string ipMask)
		{
			this.Name = name;
			this.Value = ipMask;
		}

		#endregion
	}
}

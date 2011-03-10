using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class CustomHostsElement : ConfigurationElement
	{
		#region Properties

		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		#endregion

		#region Constructors

		public CustomHostsElement()
		{
		}

		public CustomHostsElement(string name)
		{
			this.Name = name;
		}

		#endregion
	}
}

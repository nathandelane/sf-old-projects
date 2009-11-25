using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class ServerElement : ConfigurationElement
	{
		#region Properties

		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("ipMask", IsRequired = true)]
		public string IpMask
		{
			get { return (string)this["ipMask"]; }
			set { this["ipMask"] = value; }
		}

		#endregion

		#region Constructors

		public ServerElement()
		{
		}

		public ServerElement(string name, string ipMask)
		{
			this.Name = name;
			this.IpMask = ipMask;
		}

		#endregion
	}
}

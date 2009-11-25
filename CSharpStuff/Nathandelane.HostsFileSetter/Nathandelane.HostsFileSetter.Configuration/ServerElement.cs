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

		[ConfigurationProperty("pool", IsRequired = true)]
		public string Pool
		{
			get { return (string)this["pool"]; }
			set { this["pool"] = value; }
		}

		#endregion

		#region Constructors

		public ServerElement()
		{
		}

		public ServerElement(string name, string ipMask, string pool)
		{
			this.Name = name;
			this.IpMask = ipMask;
			this.Pool = pool;
		}

		#endregion
	}
}

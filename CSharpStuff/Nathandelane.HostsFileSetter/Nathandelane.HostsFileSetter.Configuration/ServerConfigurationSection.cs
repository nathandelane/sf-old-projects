using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class ServerConfigurationSection : ConfigurationSection
	{
		#region Properties

		[ConfigurationProperty("servers", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(ServerCollection), AddItemName = "addServer", ClearItemsName = "clearServers", RemoveItemName = "removeServer")]
		public ServerCollection Elements
		{
			get { return (ServerCollection)base["servers"]; }
		}

		#endregion

		#region Constructors

		public ServerConfigurationSection()
		{
		}

		#endregion
	}
}

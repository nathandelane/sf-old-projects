using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class CustomHostsConfigurationSection : ConfigurationSection
	{
		#region Properties

		[ConfigurationProperty("customHosts", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(CustomHostsElementCollection), AddItemName = "newHosts", ClearItemsName = "clearHosts", RemoveItemName = "removeHosts")]
		public CustomHostsElementCollection Elements
		{
			get { return (CustomHostsElementCollection)base["customHosts"]; }
		}

		#endregion

		#region Constructors

		public CustomHostsConfigurationSection()
		{
		}

		#endregion
	}
}

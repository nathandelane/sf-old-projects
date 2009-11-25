using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class AdminConfigurationSection : ConfigurationSection
	{
		#region Properties

		[ConfigurationProperty("admin", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(AdminElementCollection), AddItemName = "addAdmin", ClearItemsName = "clearAdmin", RemoveItemName = "removeAdmin")]
		public AdminElementCollection Elements
		{
			get { return (AdminElementCollection)base["admin"]; }
		}

		#endregion

		#region Constructors

		public AdminConfigurationSection()
		{
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class HatConfigurationSection : ConfigurationSection
	{
		#region Properties

		[ConfigurationProperty("hats", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(HatCollection), AddItemName = "addHat", ClearItemsName = "clearHats", RemoveItemName = "removeHat")]
		public HatCollection Elements
		{
			get { return (HatCollection)base["hats"]; }
		}

		#endregion

		#region Constructors

		public HatConfigurationSection()
		{
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class CustomConfigurationSection : ConfigurationSection
	{
		#region Properties

		[ConfigurationProperty("customConfigurations", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(CustomElementCollection), AddItemName = "addEntry", ClearItemsName = "clearEntry", RemoveItemName = "removeEntry")]
		public CustomElementCollection Elements
		{
			get { return (CustomElementCollection)base["customConfigurations"]; }
		}

		#endregion

		#region Constructors

		public CustomConfigurationSection()
		{
		}

		#endregion
	}
}

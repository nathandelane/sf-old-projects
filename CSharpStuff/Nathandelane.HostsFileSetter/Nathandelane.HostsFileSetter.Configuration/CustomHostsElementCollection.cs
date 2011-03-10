using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	[ConfigurationCollection(typeof(CustomHostsElementCollection), AddItemName = "newHosts", ClearItemsName = "clearHosts", RemoveItemName = "removeHosts")]
	public class CustomHostsElementCollection : ConfigurationElementCollection
	{
		#region Methods

		public void Add(CustomHostsElement element)
		{
			BaseAdd(element);
		}

		public void Clear()
		{
			BaseClear();
		}

		public void Remove(CustomHostsElement element)
		{
			BaseRemove(element.Name);
		}

		public void Remove(string name)
		{
			BaseRemove(name);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}

		/// <summary>
		/// Creates a new CustomHostsElement.
		/// </summary>
		/// <returns></returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new CustomHostsElement();
		}

		/// <summary>
		/// Gets the element key (name).
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CustomHostsElement)element).Name;
		}

		#endregion
	}
}

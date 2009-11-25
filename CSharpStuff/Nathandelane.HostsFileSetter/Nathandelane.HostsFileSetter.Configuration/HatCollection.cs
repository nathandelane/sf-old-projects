using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	[ConfigurationCollection(typeof(HatCollection), AddItemName = "addHat", ClearItemsName = "clearHats", RemoveItemName = "removeHat")]
	public class HatCollection : ConfigurationElementCollection
	{
		#region Methods

		public void Add(HatElement element)
		{
			BaseAdd(element);
		}

		public void Clear()
		{
			BaseClear();
		}

		public void Remove(HatElement element)
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
		/// Creates a new HatElement.
		/// </summary>
		/// <returns></returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new HatElement();
		}

		/// <summary>
		/// Gets the element key (name).
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((HatElement)element).Name;
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	[ConfigurationCollection(typeof(CustomElementCollection), AddItemName = "addEntry", ClearItemsName = "clearEntry", RemoveItemName = "removeEntry")]
	public class CustomElementCollection : ConfigurationElementCollection
	{
		#region Methods

		public void Add(CustomElement element)
		{
			BaseAdd(element);
		}

		public void Clear()
		{
			BaseClear();
		}

		public void Remove(CustomElement element)
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
		/// Creates a new CustomElement.
		/// </summary>
		/// <returns></returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new CustomElement();
		}

		/// <summary>
		/// Gets the element key (name).
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CustomElement)element).Name;
		}

		#endregion
	}
}

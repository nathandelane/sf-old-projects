using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Configuration
{
	public class ServerCollection : ConfigurationElementCollection
	{
		#region Properties

		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
		}

		public ServerElement this[int index]
		{
			get { return (ServerElement)BaseGet(index); }
			set
			{
				if(BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}

				BaseAdd(index, value);
			}
		}

		#endregion

		#region Constructors

		public ServerCollection()
		{
		}

		#endregion

		#region Methods

		public void Add(ServerElement element)
		{
			BaseAdd(element);
		}

		public void Clear()
		{
			BaseClear();
		}

		public int IndexOf(ServerElement element)
		{
			return BaseIndexOf(element);
		}

		public void Remove(ServerElement element)
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
		/// Creates a new ServerElement.
		/// </summary>
		/// <returns></returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new ServerElement();
		}

		/// <summary>
		/// Gets the element key (name).
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ServerElement)element).Name;
		}

		#endregion
	}
}

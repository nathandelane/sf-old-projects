using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	public class ZoneCollection : ConfigurationElementCollection
	{
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		public ZoneElement this[int index]
		{
			get { return BaseGet(index) as ZoneElement; }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}

				BaseAdd(index, value);
			}
		}

		public void Add(ZoneElement element)
		{
			BaseAdd(element);
		}

		public void Clear()
		{
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ZoneElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as ZoneElement).Zone;
		}

		public void Remove(ZoneElement element)
		{
			BaseRemove(element.Zone);
		}

		public void Remove(string zone)
		{
			BaseRemove(zone);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}
	}

	public class ZoneElement : ConfigurationElement
	{
		public ZoneElement()
		{
		}

		public ZoneElement(string zone, string members)
		{
			this.Zone = zone;
			this.Members = members;
		}

		[ConfigurationProperty("Zone", IsRequired = true, DefaultValue = "")]
		public string Zone
		{
			get { return this["Zone"] as String; }
			set { this["Zone"] = value; }
		}

		[ConfigurationProperty("Members", IsRequired = true, DefaultValue = "")]
		public string Members
		{
			get { return this["Members"] as String; }
			set { this["Members"] = value; }
		}
	}
}


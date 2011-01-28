/*
 * Created by SharpDevelop.
 * User: nalane
 * Date: 1/28/2011
 * Time: 11:33 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;

namespace Nathandelane.Net.Spider.Configuration
{
	/// <summary>
	/// Configuration section collection.
	/// </summary>
	[ConfigurationCollection(typeof(CredentialsElement), AddItemName = "Credentials", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class AuthenticationCredentialsCollection : ConfigurationElementCollection
	{
		#region Properties
		
		/// <summary>
		/// Gets a CredentialsElement base on the key supplied.
		/// </summary>
		new public CredentialsElement this[string key]
		{
			get { return (CredentialsElement)BaseGet(key); }
		}
		
		#endregion
		
		#region Constructor
		
		public AuthenticationCredentialsCollection()
		{
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Gets the Key of a CredentialsElement.
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CredentialsElement)element).Key;
		}
		
		/// <summary>
		/// Inherited from ConfigurationElementCollection.
		/// </summary>
		/// <returns></returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new CredentialsElement();
		}

		#endregion
	}
}

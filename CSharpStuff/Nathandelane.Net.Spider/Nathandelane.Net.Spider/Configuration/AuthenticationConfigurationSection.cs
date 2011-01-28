/*
 * Created by SharpDevelop.
 * User: nalane
 * Date: 1/28/2011
 * Time: 11:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;

namespace Nathandelane.Net.Spider.Configuration
{
	/// <summary>
	/// Configuration section named AuthenticationConfiguration.
	/// </summary>
	public class AuthenticationConfigurationSection : ConfigurationSection
	{
		#region Fields
		
		private const string PrimaryPropertyName = "primary";
		private const string AuthenticationCredentialsPropertyName = "AuthenticationCredentials";
		
		private static readonly ConfigurationProperty __primary = new ConfigurationProperty(AuthenticationConfigurationSection.PrimaryPropertyName, typeof(string), String.Empty, ConfigurationPropertyOptions.None);
		private static readonly ConfigurationProperty __authenticationCredentials = new ConfigurationProperty(AuthenticationConfigurationSection.AuthenticationCredentialsPropertyName, typeof(AuthenticationCredentialsCollection), null, ConfigurationPropertyOptions.IsRequired);
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets the value of the Primary property.
		/// </summary>
		[ConfigurationProperty(AuthenticationConfigurationSection.PrimaryPropertyName, IsRequired = false)]
		public string Primary
		{
			get { return (string)this[__primary]; }
		}
		
		/// <summary>
		/// Gets the AuthenticationCredentials collection.
		/// </summary>
		[ConfigurationProperty(AuthenticationConfigurationSection.AuthenticationCredentialsPropertyName, IsRequired = true)]
		public AuthenticationCredentialsCollection AuthenticationCredentials
		{
			get { return (AuthenticationCredentialsCollection)this[__authenticationCredentials]; }
		}
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Creates an instance of AuthenticationConfigurationSection.
		/// </summary>
		public AuthenticationConfigurationSection()
		{
			base.Properties.Add(AuthenticationConfigurationSection.__primary);
		}

		#endregion
	}
}

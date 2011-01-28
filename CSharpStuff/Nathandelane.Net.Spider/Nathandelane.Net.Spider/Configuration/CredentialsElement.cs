/*
 * Created by SharpDevelop.
 * User: nalane
 * Date: 1/28/2011
 * Time: 11:13 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;

namespace Nathandelane.Net.Spider.Configuration
{
	/// <summary>
	/// Creates an instance of CredentialsElement.
	/// </summary>
	public class CredentialsElement : ConfigurationElement
	{
		#region Fields
		
		private const string KeyPropertyName = "key";
		private const string UsernamePropertyName = "username";
		private const string UsernameKeyPropertyName = "usernameKey";
		private const string PasswordPropertyName = "password";
		private const string PasswordKeyPropertyName = "passwordKey";
		private const string MethodPropertyName = "method";
		private const string DomainPropertyName = "domain";
		private const string RealmPropertyName = "realm";
		
		private static readonly ConfigurationProperty __key = new ConfigurationProperty(CredentialsElement.KeyPropertyName, typeof(string), String.Empty, ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty __username = new ConfigurationProperty(CredentialsElement.UsernamePropertyName, typeof(string), String.Empty, ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty __usernameKey = new ConfigurationProperty(CredentialsElement.UsernameKeyPropertyName, typeof(string), String.Empty, ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty __password = new ConfigurationProperty(CredentialsElement.PasswordPropertyName, typeof(string), String.Empty, ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty __passwordKey = new ConfigurationProperty(CredentialsElement.PasswordKeyPropertyName, typeof(string), String.Empty, ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty __method = new ConfigurationProperty(CredentialsElement.MethodPropertyName, typeof(AuthenticationMethod), AuthenticationMethod.HttpPost, ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty __domain = new ConfigurationProperty(CredentialsElement.DomainPropertyName, typeof(string), String.Empty, ConfigurationPropertyOptions.None);
		private static readonly ConfigurationProperty __realm = new ConfigurationProperty(CredentialsElement.RealmPropertyName, typeof(string), String.Empty, ConfigurationPropertyOptions.None);
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets the key for this credentials element.
		/// </summary>
		[ConfigurationProperty(CredentialsElement.KeyPropertyName, IsRequired = true)]
		public string Key
		{
			get { return (string)this[__key]; }
		}
		
		/// <summary>
		/// Gets the username for this credentials element.
		/// </summary>
		[ConfigurationProperty(CredentialsElement.UsernamePropertyName, IsRequired = true)]
		public string Username
		{
			get { return (string)this[__username]; }
		}
		
		/// <summary>
		/// Gets the username key for this credentials element.
		/// </summary>
		[ConfigurationProperty(CredentialsElement.UsernameKeyPropertyName, IsRequired = true)]
		public string UsernameKey
		{
			get { return (string)this[__usernameKey]; }
		}
		
		/// <summary>
		/// Gets the password for this credentials element.
		/// </summary>
		[ConfigurationProperty(CredentialsElement.PasswordPropertyName, IsRequired = true)]
		public string Password
		{
			get { return (string)this[__password]; }
		}
		
		/// <summary>
		/// Gets the password key for this credentials element.
		/// </summary>
		[ConfigurationProperty(CredentialsElement.PasswordKeyPropertyName, IsRequired = true)]
		public string PasswordKey
		{
			get { return (string)this[__passwordKey]; }
		}
		
		/// <summary>
		/// Gets the method for this credentials element.
		/// </summary>
		[ConfigurationProperty(CredentialsElement.MethodPropertyName, IsRequired = true)]
		public AuthenticationMethod Method
		{
			get { return (AuthenticationMethod)Enum.Parse(typeof(AuthenticationMethod), ((string)this[__method])); }
		}
		
		/// <summary>
		/// Gets the domain for this credentials element.
		/// </summary>
		[ConfigurationProperty(CredentialsElement.DomainPropertyName, IsRequired = false)]
		public string Domain
		{
			get { return (string)this[__domain]; }
		}
		
		/// <summary>
		/// Gets the realm for this credentials element.
		/// </summary>
		[ConfigurationProperty(CredentialsElement.RealmPropertyName, IsRequired = false)]
		public string Realm
		{
			get { return (string)this[__realm]; }
		}
		
		#endregion
		
		#region Constructor
		
		/// <summary>
		/// Creates an instance of CredentialsElement.
		/// </summary>
		public CredentialsElement()
		{
			base.Properties.Add(CredentialsElement.__username);
			base.Properties.Add(CredentialsElement.__password);
			base.Properties.Add(CredentialsElement.__method);
			base.Properties.Add(CredentialsElement.__domain);
			base.Properties.Add(CredentialsElement.__realm);
		}
		
		#endregion
	}
}

/*
 * Created by SharpDevelop.
 * User: nalane
 * Date: 1/28/2011
 * Time: 10:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Nathandelane.Net.Spider.Configuration;

namespace Nathandelane.Net.Spider
{
	/// <summary>
	/// The class contains credentials and the method used for any authentication required at initialization.
	/// </summary>
	public class AuthenticationCredentials
	{
		#region Fields
			
		private string _username;
		private string _password;
		private string _domain;
		private string _realm;
		private AuthenticationMethod _method;
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets the username.
		/// </summary>
		public string Username
		{
			get { return _username; }
		}
		
		/// <summary>
		/// Gets the password.
		/// </summary>
		public string Password
		{
			get { return _password; }
		}
		
		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		public string Domain
		{
			get { return _domain; }
			set { _domain = value; }
		}
		
		/// <summary>
		/// Gets or sets the realm.
		/// </summary>
		public string Realm
		{
			get { return _realm; }
			set { _realm = value; }
		}
		
		/// <summary>
		/// Gets the authentication method.
		/// </summary>
		public AuthenticationMethod Method
		{
			get { return _method; }
		}
		
		#endregion
		
		#region Constructor

		/// <summary>
		/// Creates an instance of AuthenticationCredentials.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="method"></param>
		public AuthenticationCredentials(string username, string password, AuthenticationMethod method)
		{
			_username = username;
			_password = password;
			_method = method;
		}
		
		#endregion
	}
}

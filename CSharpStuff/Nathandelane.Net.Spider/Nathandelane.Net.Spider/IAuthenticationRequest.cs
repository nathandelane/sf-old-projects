/*
 * Created by SharpDevelop.
 * User: nalane
 * Date: 1/28/2011
 * Time: 10:50 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Nathandelane.Net.Spider
{
	/// <summary>
	/// This interface represents an authentication request. All authentication request objects must inherit this interface.
	/// </summary>
	public interface IAuthenticationRequest
	{
		/// <summary>
		/// Gets a response for the credentials and Uri given.
		/// </summary>
		/// <returns></returns>
		HttpWebResponse GetResponse();
	}
}

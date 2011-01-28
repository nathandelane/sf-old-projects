/*
 * Created by SharpDevelop.
 * User: nalane
 * Date: 1/28/2011
 * Time: 10:54 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;

namespace Nathandelane.Net.Spider
{
	/// <summary>
	/// Description of HttpPostAuthentication.
	/// </summary>
	public class HttpPostAuthentication : IAuthenticationRequest
	{
		#region Constructor
		
		/// <summary>
		/// Creates an instance of HttpPostAuthentication.
		/// </summary>
		public HttpPostAuthentication()
		{
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Gets the response to an HTTP POST authentication attempt.
		/// </summary>
		/// <returns></returns>
		public HttpWebResponse GetResponse()
		{
			throw new NotImplementedException();
		}
		
		#endregion
	}
}

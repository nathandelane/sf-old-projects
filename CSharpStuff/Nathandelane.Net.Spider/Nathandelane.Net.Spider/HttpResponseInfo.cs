/*
 * Created by SharpDevelop.
 * User: nalane
 * Date: 1/28/2011
 * Time: 12:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Nathandelane.Net.Spider
{
	/// <summary>
	/// Contains data pertaining to the HTTP response to a request.
	/// </summary>
	public class HttpResponseInfo
	{
		#region Fields
		
		private int _errorCode;
		private string _message;
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets the error code for this HTTP response.
		/// </summary>
		public int ErrorCode
		{
			get { return _errorCode; }
		}
		
		/// <summary>
		/// Gets the message for this HTTP response.
		/// </summary>
		public string Message
		{
			get { return _message; }
		}
		
		#endregion
		
		#region Constructor
		
		/// <summary>
		/// Creates an instance of HttpResponseInfo.
		/// </summary>
		/// <param name="completeHttpResponseMessage"></param>
		public HttpResponseInfo(string completeHttpResponseMessage)
		{
			string errorCode = completeHttpResponseMessage.Substring(0, 3);
			
			_errorCode = Int32.Parse(errorCode);
			_message = completeHttpResponseMessage.Substring(3);
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Returns a string representation of this HttpResponsInfo object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("{0} {1}", _errorCode, _message);
		}

		
		#endregion
	}
}

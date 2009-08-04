using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.Configuration;

namespace Nathandelane.Net.HGrep
{
	public class Agent
	{
		#region Fields

		private HttpWebRequest _request;
		private HttpWebResponse _response;

		#endregion

		#region Properties

		public HttpWebRequest Request
		{
			get { return _request; }
			set { _request = value; }
		}

		public HttpWebResponse Response
		{
			get { return _response; }
		}

		#endregion

		#region Constructors

		public Agent(Uri requestUri)
		{
			_request = HttpWebRequest.CreateDefault(requestUri) as HttpWebRequest;
			_request.Accept = ConfigurationManager.AppSettings["Accept"];
			_request.Timeout = int.Parse(ConfigurationManager.AppSettings["Timeout"]);
			_request.UserAgent = ConfigurationManager.AppSettings["UserAgentString"];
			_request.Headers.Add("Accept-Language", ConfigurationManager.AppSettings["AcceptLanguage"]);
			_request.Headers.Add("Accept-Encoding", ConfigurationManager.AppSettings["AcceptEncoding"]);
		}

		#endregion

		#region Methods

		public void Run()
		{
			try
			{
				_response = _request.GetResponse() as HttpWebResponse;
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: {0}", e.StackTrace);
			}
		}

		#endregion
	}
}

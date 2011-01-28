using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using HtmlAgilityPack;

namespace Nathandelane.Net.Spider
{
	public class Agent
	{
		#region Fields

		private static long __id = 0L;

		private AgentRequest _request;
		private CookieCollection _cookies;
		private HttpResponseInfo _responseInfo;

		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets this Agent's request.
		/// </summary>
		public AgentRequest Request
		{
			get { return _request; }
		}
		
		#endregion

		#region Constructors

		public Agent(SpiderUrl target, CookieCollection cookies)
		{
			_request = new AgentRequest(target, cookies);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Runs the agent.
		/// </summary>
		public void Run()
		{
			Agent.__id++;

			_responseInfo = _request.Execute();
		}

		/// <summary>
		/// Provides a string representation for this agent.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("{0},{1},\"{2}\",\"{3}\",\"{4}\",\"{5}\",{6}", Agent.__id, DateTime.Now.ToString("hh:mm:ss.fff"), _responseInfo, _request.Target, _request.Referrer, _request.DocumentTitle, _request.ElapsedTime);
		}

		#endregion
	}
}

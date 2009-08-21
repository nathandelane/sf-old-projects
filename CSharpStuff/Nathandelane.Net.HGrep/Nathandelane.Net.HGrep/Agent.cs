/*  Copyright (C) 2009, Nathandelane.
	License:
	Copyright 1992, 1997-1999, 2000 Free Software Foundation, Inc.

	This program is free software; you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation; either version 3, or (at your option)
	any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
	02111-1307, USA.
*/

using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Nathandelane.Net.HGrep
{
	public class Agent
	{
		#region Fields

		private HttpWebRequest _request;
		private HttpWebResponse _response;
		private int _timeout;

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
			InitializeRequest(requestUri);
			SetGlobalCertficateDefaultPolicy();
		}

		public Agent(Uri requestUri, bool ignoreBadCertificates)
		{
			InitializeRequest(requestUri);
			SetGlobalCertificatePolicyAlwaysTrue();
		}

		public Agent(Uri requestUri, string postBody)
		{
			InitializeRequest(requestUri);
			SetGlobalCertficateDefaultPolicy();
			AddPostBody(postBody);
		}

		public Agent(Uri requestUri, string postBody, bool ignoreBadCertificates)
		{
			InitializeRequest(requestUri);
			SetGlobalCertificatePolicyAlwaysTrue();
			AddPostBody(postBody);
		}

		public Agent(Uri requestUri, int timeout)
		{
			InitializeRequest(requestUri);
			SetGlobalCertficateDefaultPolicy();

			_timeout = timeout;
		}

		public Agent(Uri requestUri, bool ignoreBadCertificates, int timeout)
		{
			InitializeRequest(requestUri);
			SetGlobalCertificatePolicyAlwaysTrue();

			_timeout = timeout;
		}

		public Agent(Uri requestUri, string postBody, int timeout)
		{
			InitializeRequest(requestUri);
			SetGlobalCertficateDefaultPolicy();
			AddPostBody(postBody);

			_timeout = timeout;
		}

		public Agent(Uri requestUri, string postBody, bool ignoreBadCertificates, int timeout)
		{
			InitializeRequest(requestUri);
			SetGlobalCertificatePolicyAlwaysTrue();
			AddPostBody(postBody);

			_timeout = timeout;
		}

		#endregion

		#region Methods

		public void Run()
		{
			_response = _request.GetResponse() as HttpWebResponse;
		}

		private void InitializeRequest(Uri requestUri)
		{
			_request = HttpWebRequest.CreateDefault(requestUri) as HttpWebRequest;
			_request.Accept = ConfigurationManager.AppSettings["Accept"];
			_request.Timeout = ((_timeout == 0) ? int.Parse(ConfigurationManager.AppSettings["Timeout"]) : _timeout);
			_request.UserAgent = ConfigurationManager.AppSettings["UserAgentString"];
			_request.Headers.Add("Accept-Language", ConfigurationManager.AppSettings["AcceptLanguage"]);
			_request.Headers.Add("Accept-Encoding", ConfigurationManager.AppSettings["AcceptEncoding"]);
			_request.AllowAutoRedirect = true;
		}

		private void SetGlobalCertificatePolicyAlwaysTrue()
		{
			ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateAllRemoteCertificates);
		}

		private void SetGlobalCertficateDefaultPolicy()
		{
			ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(DefaultValidateRemoteCertificates);
		}

		private void AddPostBody(string postBody)
		{
			_request.Method = "POST";
			_request.ContentType = "application/x-www-form-urlencoded";
			_request.ContentLength = postBody.Length;

			using (StreamWriter writer = new StreamWriter(_request.GetRequestStream(), Encoding.ASCII))
			{
				writer.Write(postBody);
			}
		}

		private static bool ValidateAllRemoteCertificates(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return true;
		}

		private static bool DefaultValidateRemoteCertificates(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return policyErrors == SslPolicyErrors.None;
		}

		#endregion
	}
}

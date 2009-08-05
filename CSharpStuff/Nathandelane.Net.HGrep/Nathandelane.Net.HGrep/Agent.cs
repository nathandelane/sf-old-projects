using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

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
			_request.Timeout = int.Parse(ConfigurationManager.AppSettings["Timeout"]);
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

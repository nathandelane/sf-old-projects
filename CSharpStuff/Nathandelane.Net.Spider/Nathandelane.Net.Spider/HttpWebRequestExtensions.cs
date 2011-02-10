using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Nathandelane.Net.Spider
{
	public static class HttpWebRequestExtensions
	{
		#region Methods

		/// <summary>
		/// Creates a copy of this HttpWebRequest.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static HttpWebRequest Clone(this HttpWebRequest source)
		{
			HttpWebRequest result = WebRequest.Create(source.RequestUri) as HttpWebRequest;
			result.Accept = source.Accept;
			result.AllowAutoRedirect = source.AllowAutoRedirect;
			result.AllowWriteStreamBuffering = source.AllowWriteStreamBuffering;
			result.AuthenticationLevel = source.AuthenticationLevel;
			result.AutomaticDecompression = source.AutomaticDecompression;
			result.CachePolicy = source.CachePolicy;
			result.ClientCertificates = source.ClientCertificates;
			result.ConnectionGroupName = source.ConnectionGroupName;
			result.ContentType = source.ContentType;
			result.ContinueDelegate = source.ContinueDelegate;
			result.CookieContainer = source.CookieContainer;
			result.Credentials = source.Credentials;
			result.Expect = source.Expect;
			result.IfModifiedSince = source.IfModifiedSince;
			result.ImpersonationLevel = source.ImpersonationLevel;
			result.KeepAlive = source.KeepAlive;
			result.MaximumAutomaticRedirections = source.MaximumAutomaticRedirections;
			result.MaximumResponseHeadersLength = source.MaximumResponseHeadersLength;
			result.MediaType = source.MediaType;
			result.Method = source.Method;
			result.Pipelined = source.Pipelined;
			result.PreAuthenticate = source.PreAuthenticate;
			result.ProtocolVersion = source.ProtocolVersion;
			result.Proxy = source.Proxy;
			result.ReadWriteTimeout = source.ReadWriteTimeout;
			result.Referer = source.Referer;
			result.SendChunked = source.SendChunked;
			result.Timeout = source.Timeout;
			result.TransferEncoding = source.TransferEncoding;
			result.UnsafeAuthenticatedConnectionSharing = source.UnsafeAuthenticatedConnectionSharing;
			result.UseDefaultCredentials = source.UseDefaultCredentials;
			result.UserAgent = source.UserAgent;

			return result;
		}

		#endregion
	}
}

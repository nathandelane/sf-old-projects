using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;

namespace Nathandelane.Net.Spider
{
	public class Agent
	{
		#region Fields

		private HttpWebRequest _webRequest;
		private TimeSpan _elapsedTime;

		#endregion

		#region Properties

		private long Ticks
		{
			get { return DateTime.Now.Ticks; }
		}

		#endregion

		#region Constructors

		public Agent(string address)
		{
			Uri uri = new Uri(address, UriKind.Absolute);

			SetupWebRequest(uri);
		}

		#endregion

		#region Public Methods

		public void Run()
		{
			long startingTicks = Ticks;

			HttpWebResponse response = _webRequest.GetResponse() as HttpWebResponse;

			long mark = Ticks;

			_elapsedTime = new TimeSpan(mark - startingTicks);
		}

		#endregion

		#region Private Methods

		private void SetupWebRequest(Uri uri)
		{
			_webRequest = WebRequest.CreateDefault(uri) as HttpWebRequest;
			_webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			_webRequest.AllowAutoRedirect = true;
			_webRequest.AllowWriteStreamBuffering = true;
			_webRequest.AuthenticationLevel = AuthenticationLevel.None;
			_webRequest.AutomaticDecompression = DecompressionMethods.GZip;
			_webRequest.Connection = "keep-alive";
			_webRequest.CookieContainer = new CookieContainer();
			_webRequest.ImpersonationLevel = TokenImpersonationLevel.Impersonation;
			_webRequest.KeepAlive = true;
			_webRequest.Method = "GET";
			_webRequest.Timeout = 30000;
			_webRequest.UseDefaultCredentials = true;
			_webRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.8) Gecko/2009032609 Firefox/3.0.8 (.NET CLR 3.5.30729)";

			ServicePointManager.ServerCertificateValidationCallback +=
					delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
					{
						return true;
					};
		}

		#endregion
	}
}

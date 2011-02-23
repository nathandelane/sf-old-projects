using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HttpNetTest.Events;

namespace HttpNetTest
{
	public class WebTestRequest : WebTestItem
	{
		#region Fields

		public delegate void Validate(object o, ValidationEventArgs args);

		private HttpWebRequest _webRequest;
		private HttpWebResponse _webResponse;
		private Uri _uri;
		private IHttpBody _body;
		private string _responseBody;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the body of this request.
		/// </summary>
		public IHttpBody Body
		{
			get { return _body; }
			set { _body = value; }
		}

		#endregion

		#region Constructors

		public WebTestRequest(string url)
		{
			if (!Uri.TryCreate(url, UriKind.Absolute, out _uri))
			{
				throw new ArgumentException("Url must be an absolute address to a resource.");
			}

			InitializeWebRequest();
		}

		public WebTestRequest(Uri uri)
		{
			_uri = uri;

			InitializeWebRequest();
		}

		#endregion

		#region Methods

		private void InitializeWebRequest()
		{
			_webRequest = WebRequest.Create(_uri) as HttpWebRequest;
		}

		#endregion
	}
}

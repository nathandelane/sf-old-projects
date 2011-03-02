using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HttpNetTest.Events;
using System.IO;

namespace HttpNetTest
{
	public class NetTestRequest : NetTestItem
	{
		#region Fields

		public delegate void ValidationEventHandler(object o, ValidationEventArgs e);
		public delegate void ExtractionEventHandler(object o, ExtractionEventArgs e);

		public event ValidationEventHandler ValidateResponse;
		public event ExtractionEventHandler ExtractValues;

		private HttpWebRequest _webRequest;
		private HttpWebResponse _webResponse;
		private Uri _uri;
		private string _httpResponseBody;
		private NetTestContext _context;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the HTTP response body.
		/// </summary>
		public string HttpResponseBody
		{
			get { return _httpResponseBody; }
		}

		/// <summary>
		/// Gets the context for this test.
		/// </summary>
		public NetTestContext Context
		{
			get { return _context; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of WebTestRequest using a string URL.
		/// </summary>
		/// <param name="url"></param>
		public NetTestRequest(string url)
		{
			if (!Uri.TryCreate(url, UriKind.Absolute, out _uri))
			{
				throw new ArgumentException("Url must be an absolute address to a resource.");
			}

			InitializeWebRequest();
		}

		/// <summary>
		/// Creates an instance of WebTestRequest using a Uri object.
		/// </summary>
		/// <param name="uri"></param>
		public NetTestRequest(Uri uri)
		{
			_uri = uri;

			InitializeWebRequest();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Executes this WebTestRequest.
		/// </summary>
		public override void Execute()
		{
			_webResponse = _webRequest.GetResponse() as HttpWebResponse;

			using (StreamReader reader = new StreamReader(_webResponse.GetResponseStream()))
			{
				_httpResponseBody = reader.ReadToEnd();
			}

			OnValidate(new ValidationEventArgs(this));
			OnExtract(new ExtractionEventArgs(this));
		}

		/// <summary>
		/// Fires Validate event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnValidate(ValidationEventArgs e)
		{
			if (ValidateResponse != null)
			{
				ValidateResponse(this, e);
			}
		}

		/// <summary>
		/// Fires Extract event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnExtract(ExtractionEventArgs e)
		{
			if (ExtractValues != null)
			{
				ExtractValues(this, e);
			}
		}

		/// <summary>
		/// Initializes the web request object for this WebTestRequest.
		/// </summary>
		private void InitializeWebRequest()
		{
			_webRequest = WebRequest.Create(_uri) as HttpWebRequest;
			_context = NetTestContext.GetContext();
		}

		#endregion
	}
}

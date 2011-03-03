using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Nathandelane.TestingTools.WebTesting.Events;
using System.IO;

namespace Nathandelane.TestingTools.WebTesting
{
	public class NetTestRequest : NetTestItem
	{
		#region Fields

		public delegate void ValidationEventHandler(object o, ValidationEventArgs e);
		public delegate void ExtractionEventHandler(object o, ExtractionEventArgs e);
		public delegate void PreExecuteEventHandler(object o, ExecutionEventArgs e);
		public delegate void PostExecuteEventHandler(object o, ExecutionEventArgs e);

		public event ValidationEventHandler ValidateResponse;
		public event ExtractionEventHandler ExtractValues;
		public event PreExecuteEventHandler PreExecute;
		public event PostExecuteEventHandler PostExecute;

		private HttpWebRequest _webRequest;
		private HttpWebResponse _webResponse;
		private Uri _uri;
		private string _httpResponseBody;
		private string _responseUrl;
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

		/// <summary>
		/// Gets the response URL from the response.
		/// </summary>
		public string ResponseUrl
		{
			get { return _responseUrl; }
		}

		/// <summary>
		/// Gets or sets the User-Agent string for this NetTestRequest.
		/// </summary>
		public string UserAgent
		{
			get { return _webRequest.UserAgent; }
			set { _webRequest.UserAgent = value; }
		}

		/// <summary>
		/// Gets or sets the cookies associated with the request.
		/// </summary>
		public CookieContainer CookieContainer
		{
			get { return _webRequest.CookieContainer; }
			set { _webRequest.CookieContainer = value; }
		}

		/// <summary>
		/// Gets or sets a value that indicates whether the request should follow redirection responses.
		/// </summary>
		public bool AutomaticallyRedirect
		{
			get { return _webRequest.AllowAutoRedirect; }
			set { _webRequest.AllowAutoRedirect = value; }
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
			OnPreExecute(new ExecutionEventArgs(this));

			_webResponse = _webRequest.GetResponse() as HttpWebResponse;

			OnPostExecute(new ExecutionEventArgs(this));

			using (StreamReader reader = new StreamReader(_webResponse.GetResponseStream()))
			{
				_httpResponseBody = reader.ReadToEnd();
			}

			_responseUrl = _webResponse.ResponseUri.ToString();
			_context["cookies"] = _webResponse.Cookies;

			OnValidate(new ValidationEventArgs(this));
			OnExtract(new ExtractionEventArgs(this));
		}

		/// <summary>
		/// Fires the PreExecute event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnPreExecute(ExecutionEventArgs e)
		{
			if (PreExecute != null)
			{
				PreExecute(this, e);
			}
		}

		/// <summary>
		/// Fires the PostExecute event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnPostExecute(ExecutionEventArgs e)
		{
			if (PostExecute != null)
			{
				PostExecute(this, e);
			}
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

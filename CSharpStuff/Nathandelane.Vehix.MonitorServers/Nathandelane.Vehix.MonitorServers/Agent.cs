using System;
using System.Net;

namespace Nathandelane.Vehix.MonitorServers
{
	public class Agent
	{
		#region Fields

		private string _name;
		private Uri _uri;
		private bool _isAvailable;

		#endregion

		#region Properties

		public string MonitorName
		{
			get { return _name; }
		}

		public Uri MonitorUri
		{
			get { return _uri; }
		}

		public bool IsAvailable
		{
			get
			{
				Run();
				return _isAvailable;
			}
		}

		#endregion

		#region Constructors

		public Agent(string name, Uri uri)
		{
			_name = name;
			_uri = uri;
		}

		#endregion

		#region Methods

		private void Run()
		{
			HttpWebRequest request = WebRequest.Create(_uri) as HttpWebRequest;
			request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.11) Gecko/2009060215 Firefox/3.0.11 (.NET CLR 3.5.30729)";
			request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			request.Timeout = 30000;
			request.Headers = new WebHeaderCollection();
			request.Headers.Add("Accept-Language", "en-us,en;q=0.5");
			request.Headers.Add("Accept-Encoding", "gzip,deflate");
			request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");

			try
			{
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				_isAvailable = true;
			}
			catch (Exception ex)
			{
				_isAvailable = false;
			}
		}

		public override string ToString()
		{
			return String.Format("{0} ({1}) {2}", MonitorName, MonitorUri.AbsoluteUri, IsAvailable);
		}

		#endregion
	}
}

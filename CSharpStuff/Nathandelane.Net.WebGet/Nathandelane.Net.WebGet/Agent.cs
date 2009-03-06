using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Nathandelane.Net.WebGet
{
	public class Agent
	{
		private string _url;
		private string _fileName;
		private WebClient _client;

		public string Url
		{
			get { return _url; }
		}

		public string FileName
		{
			get { return _fileName; }
			set
			{
				_fileName = value;

				Logger.LogMessage(String.Format("Set filename to {0}.", _fileName));
			}
		}

		public WebClient Client
		{
			get { return _client; }
			set { _client = value; }
		}

		public Agent(string url)
		{
			_url = url;
			_fileName = _url.Substring(_url.LastIndexOf("/") + 1);
			_client = new WebClient();
			
			Logger.LogMessage(String.Format("Created new Agent: {0}, {1}", _url, _fileName));
		}

		public Agent(string url, string fileName)
		{
			_url = url;
			_fileName = fileName;
			_client = new WebClient();

			Logger.LogMessage(String.Format("Created new Agent: {0}, {1}.", _url, _fileName));
		}

		public void Run()
		{
			Uri uri = new Uri(Url);

			Logger.LogMessage(String.Format("Created uri: {0}; Beginning download for file named {1}.", uri.ToString(), FileName));

			Client.DownloadFile(uri, FileName);
		}
	}
}

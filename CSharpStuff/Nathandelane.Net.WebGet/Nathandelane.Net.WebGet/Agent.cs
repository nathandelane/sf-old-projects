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
		}

		public void Run()
		{
			Uri uri = new Uri(Url);

			Client = new WebClient();
			Client.DownloadFile(uri, FileName);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.Net.WebGet
{
	public class Agent
	{
		#region Fields

		private string _url;
		private string _fileName;
		private WebClient _client;
		private bool _hasGraphicalInterface;

		#endregion

		#region Properties

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
			}
		}

		public WebClient Client
		{
			get { return _client; }
			set { _client = value; }
		}

		#endregion

		#region Constructors

		public Agent(string url)
		{
			_url = url;
			_fileName = _url.Substring(_url.LastIndexOf("/") + 1);
			_client = new WebClient();
			_hasGraphicalInterface = false;
		}

		public Agent(string url, bool hasGraphicalInterface)
		{
			_url = url;
			_fileName = _url.Substring(_url.LastIndexOf("/") + 1);
			_client = new WebClient();
			_hasGraphicalInterface = hasGraphicalInterface;
		}


		public Agent(string url, string fileName)
		{
			_url = url;
			_fileName = fileName;
			_client = new WebClient();
			_hasGraphicalInterface = false;
		}

		public Agent(string url, string fileName, bool hasGraphicalInterface)
		{
			_url = url;
			_fileName = fileName;
			_client = new WebClient();
			_hasGraphicalInterface = hasGraphicalInterface;
		}

		#endregion

		#region Run Method

		public void Run()
		{
			try
			{
				Uri uri = new Uri(Url);

				Client.DownloadFile(uri, FileName);
			}
			catch (Exception e)
			{
				if (_hasGraphicalInterface)
				{
					MessageBox.Show(String.Format("Message: {0}\r\nStackTrace:\r\n{1}", e.Message, e.StackTrace));
				}
				else
				{
					Console.Write("Message: {0}\r\nStackTrace:\r\n{1}", e.Message, e.StackTrace);
				}
			}
		}

		#endregion
	}
}

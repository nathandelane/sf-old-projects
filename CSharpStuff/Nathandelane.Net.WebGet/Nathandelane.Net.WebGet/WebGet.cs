using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;

namespace Nathandelane.Net.WebGet
{
	public class WebGet : IDisposable
	{
		#region Fields
		
		private bool _isDisposed;
		private string _url;
		private string _fileName;
		
		#endregion
		
		#region Properties
		
		public bool IsDisposed
		{
			get { return _isDisposed; }
		}
		
		#endregion
		
		#region Constructors
		
		public WebGet(string url)
		{
			_url = url;
			_fileName = _url.Substring(_url.LastIndexOf("/") + 1);
		}
		
		#endregion
		
		#region Public Methods
		
		public void Run()
		{
			WebClient webClient = new WebClient();
			Uri uri = new Uri(_url);
			
			webClient.DownloadFile(uri, _fileName);
		}
		
		#endregion
		
		#region IDisposable Members
		
		public void Dispose()
		{
			Dispose(true);
		}
		
		private void Dispose(bool disposing)
		{
			if(!_isDisposed)
			{
				if(disposing)
				{
					
				}
				
				_isDisposed = true;
			}
		}
		
		#endregion
	}
}

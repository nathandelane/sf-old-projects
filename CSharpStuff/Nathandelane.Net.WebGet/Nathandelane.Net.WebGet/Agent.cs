using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;

namespace Nathandelane.Net.WebGet
{
	public class Agent : IDisposable
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
		
		public Agent(string url)
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
			
			webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnProgressChanged);
			webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadCompleted);
			
			webClient.DownloadFileAsync(uri, _fileName);
		}
		
		#endregion
		
		#region Private Methods
		
		private void OnProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Console.WriteLine("{0} bytes received. {1}% complete...", e.BytesReceived, e.ProgressPercentage);
		}
		
		private void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			Console.WriteLine("Download completed.");
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

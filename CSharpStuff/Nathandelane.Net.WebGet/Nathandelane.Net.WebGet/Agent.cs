using System;
using System.IO;
using System.Text;
using System.Net;

namespace Nathandelane.Net.WebGet
{
	public class Agent : IDisposable
	{
		#region Fields
		
		private bool _isDisposed;
		private string _url;
		
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
		}
		
		#endregion
		
		#region Public Methods
		
		public void Run()
		{
			string fileName = _url.Substring(_url.LastIndexOf("/"));
			WebClient webClient = new WebClient();
			byte[] data = webClient.DownloadData(_url);
			
			using(FileStream stream = new FileStream(fileName, FileMode.Append))
			{
				stream.Write(data, 0, data.Length);
			}
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

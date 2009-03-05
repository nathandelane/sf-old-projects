using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.WebGet.GraphicalWebGet
{
	public class WebGetDelegate
	{
		private WebGet _webGet;

		public WebGet WebGet
		{
			get { return _webGet; }
			set { _webGet = value; }
		}

		public WebGetDelegate(string url)
		{
			using (_webGet = new WebGet(url))
			{
				_webGet.Run();
			}
		}
	}
}

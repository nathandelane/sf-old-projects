using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Nathandelane.Net.Spider
{
	class Program
	{
		private Settings _settings;

		private Program()
		{
			_settings = new Settings();
			Agent agent = new Agent("http://www.vehix.com", "http://www.vehix.com/", 0L, _settings);
			agent.Request.Headers = new System.Net.WebHeaderCollection();
		}

		static void Main()
		{
			new Program();
		}
	}
}

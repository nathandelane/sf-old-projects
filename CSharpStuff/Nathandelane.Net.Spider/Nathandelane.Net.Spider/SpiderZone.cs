using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	public class SpiderZone
	{
		private string _name;
		private List<string> _pages;

		public string Name
		{
			get { return _name; }
		}

		public List<string> Pages
		{
			get { return _pages; }
		}

		public SpiderZone(string name, string[] pages)
		{
			_name = name;
			_pages = new List<string>(pages.AsEnumerable<string>());
		}

		public SpiderZone(string name, List<string> pages)
		{
			_name = name;
			_pages = pages;
		}
	}
}

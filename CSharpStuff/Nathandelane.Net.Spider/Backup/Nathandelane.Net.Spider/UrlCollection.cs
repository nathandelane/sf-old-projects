using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	public class UrlCollection : Queue<SpiderUrl>
	{
		#region Constructors

		public UrlCollection()
			: base()
		{
		}

		#endregion
	}
}

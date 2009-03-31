using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	public class SpiderUrl
	{
		#region Fields

		private string _target;
		private string _referrer;

		#endregion

		#region Properties

		public string Target
		{
			get { return _target; }
		}

		public string Referrer
		{
			get { return _referrer; }
		}

		#endregion

		#region Constructors

		public SpiderUrl(string target, string referrer)
		{
			_target = target;
			_referrer = referrer;
		}

		#endregion
	}
}

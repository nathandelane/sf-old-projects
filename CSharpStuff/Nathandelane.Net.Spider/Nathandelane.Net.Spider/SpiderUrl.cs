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
		private bool _isImage;

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

		public bool IsImage
		{
			get { return _isImage; }
		}

		#endregion

		#region Constructors

		public SpiderUrl(string target, string referrer)
		{
			_target = target;
			_referrer = referrer;

			if (target.EndsWith("gif") || target.EndsWith("jpg") || target.EndsWith("jpeg") || target.EndsWith("bmp") || target.EndsWith("png"))
			{
				_isImage = true;
			}
			else
			{
				_isImage = false;
			}
		}

		#endregion
	}
}

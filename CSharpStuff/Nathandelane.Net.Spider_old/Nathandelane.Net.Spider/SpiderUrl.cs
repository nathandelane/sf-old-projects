using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	internal class SpiderUrl
	{
		private string _url;
		private string _referringUrl;

		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}

		public string ReferringUrl
		{
			get { return _referringUrl; }
			set { _referringUrl = value; }
		}

		public SpiderUrl(string url, string referringUrl)
		{
			_url = url;
			_referringUrl = referringUrl;
		}

		public string Hash()
		{
			string result = String.Empty;

			SHA1Managed hash = new SHA1Managed();
			byte[] bytes = hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(_url));

			result = Convert.ToBase64String(bytes);

			return result;
		}

		public override bool Equals(object url)
		{
			bool result = false;
			SpiderUrl spiderUrl = url as SpiderUrl;

			if (spiderUrl.Url.Equals(_url) && spiderUrl.ReferringUrl.Equals(_referringUrl))
			{
				result = true;
			}

			return result;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}

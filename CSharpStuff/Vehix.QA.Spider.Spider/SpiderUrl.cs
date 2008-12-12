using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace Vehix.QA.Spider.Spider
{
	public class SpiderUrl
	{
		private string _url;
		private string _referringUrl;

		public string Url
		{
			get { return _url; }
		}

		public string ReferringUrl
		{
			get { return _referringUrl; }
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
	}
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Xml;

namespace Nathandelane.Net.Spider
{
	public class Agent
	{
		private string _data;
		private string[] _urls;
		private string _root;
		private string _referringUrl;
		private int _timeOut;
		private long _id;
		private HttpWebRequest _request;
		private HttpWebResponse _response;
		private Settings _settings;

		public string[] Urls
		{
			get { return _urls; }
		}

		public string Root
		{
			get { return _root; }
			set { _root = value; }
		}

		public string ReferringUrl
		{
			get { return _referringUrl; }
			set { _referringUrl = value; }
		}

		public int TimeOut
		{
			get { return _timeOut; }
			set { _timeOut = value; }
		}

		public long Id
		{
			get { return _id; }
		}

		public HttpWebRequest Request
		{
			get { return _request; }
			set { _request = value; }
		}

		public HttpWebResponse Response
		{
			get { return _response; }
		}

		public Agent(string referringurl, string root, long id, Settings settings)
		{
			_urls = new string[0];
			_referringUrl = referringurl;
			_root = root;
			_id = id;
			_settings = settings;
		}

		public void Run()
		{
			PerformHead();
			PerformGet();
		}

		private void PerformHead()
		{
			_request = WebRequest.Create(_root) as HttpWebRequest;
			_request.Accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
			_request.ImpersonationLevel = TokenImpersonationLevel.Impersonation;
			_request.KeepAlive = true;
			_request.Method = "HEAD";
			_request.Pipelined = true;
			_request.Referer = _referringUrl;
			_request.Timeout = int.Parse(_settings["timeOut"]);
			_request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; Media Center PC 5.0; .NET CLR 1.1.4322; Nathandelane.Net.Spider)";
			_response = _request.GetResponse() as HttpWebResponse;
			_request.CookieContainer = new CookieContainer();

			foreach (Cookie cookie in _response.Cookies)
			{
				_request.CookieContainer.Add(cookie);
			}
		}

		private void PerformGet()
		{
			_request.Method = "GET";
			_response = _request.GetResponse() as HttpWebResponse;

			GetAllLinkHrefs();
			GetPageTitle();
		}

		private void GetPageTitle()
		{
			
		}

		private void GetAllLinkHrefs()
		{
			throw new NotImplementedException();
		}
	}
}

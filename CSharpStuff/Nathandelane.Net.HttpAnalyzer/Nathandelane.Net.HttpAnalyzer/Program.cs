using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace Nathandelane.Net.HttpAnalyzer
{
	enum MessageType
	{
		INFO,
		ERROR
	}

	class Program
	{
		private static LinuxArguments _arguments;
		private static Dictionary<string, object> _argMap;
		private static Dictionary<string, string> _userAgentMap;
		private static HttpWebResponse _response;
		private static HttpWebRequest _webClient;
		private static string _data;

		public string Domain
		{
			get
			{
				string response = "vehix";
				if (_arguments.IsDefined("u"))
				{
					response = (((string)_arguments["u"]).Split(new char[] { '\\' }).Length == 2) ? ((string)_arguments["u"]).Split(new char[] { '\\' })[0] : "vehix";
				}
				else if (_arguments.IsDefined("user"))
				{
					response = (((string)_arguments["user"]).Split(new char[] { '\\' }).Length == 2) ? ((string)_arguments["user"]).Split(new char[] { '\\' })[0] : "vehix";
				}

				return response;
			}
		}

		public string User
		{
			get
			{
				string response = "qarobot";
				if (_arguments.IsDefined("u"))
				{
					response = (((string)_arguments["u"]).Split(new char[] { '\\' }).Length == 2) ? ((string)_arguments["u"]).Split(new char[] { '\\' })[1] : ((string)_arguments["u"]);
				}
				else if (_arguments.IsDefined("user"))
				{
					response = (((string)_arguments["user"]).Split(new char[] { '\\' }).Length == 2) ? ((string)_arguments["user"]).Split(new char[] { '\\' })[1] : ((string)_arguments["u"]);
				}

				return response;
			}
		}

		public string Password
		{
			get
			{
				string response = "v3h1x";
				if (_arguments.IsDefined("p"))
				{
					response = ((string)_arguments["p"]);
				}
				else if (_arguments.IsDefined("password"))
				{
					response = ((string)_arguments["password"]);
				}

				return response;
			}
		}

		public NetworkCredential Credentials
		{
			get
			{
				return new NetworkCredential(User, Password, Domain);
			}
		}

		public WebProxy Proxy
		{
			get
			{
				WebProxy response = null;
				if (_arguments.IsDefined("x"))
				{
					string[] addrParts = ((string)_arguments["x"]).Split(new char[] { ':' });
					response = new WebProxy(addrParts[0], int.Parse((addrParts.Length == 2) ? addrParts[1] : "80"));
				}
				else if (_arguments.IsDefined("proxy"))
				{
					string[] addrParts = ((string)_arguments["proxy"]).Split(new char[] { ':' });
					response = new WebProxy(addrParts[0], int.Parse((addrParts.Length == 2) ? addrParts[1] : "80"));
				}

				return response;
			}
		}

		public string Url
		{
			get
			{
				string response = String.Empty;
				if (_arguments.IsDefined("i"))
				{
					response = ((string)_arguments["i"]).Split(new char[] { '?' })[0];
				}
				else if (_arguments.IsDefined("uri"))
				{
					response = ((string)_arguments["uri"]).Split(new char[] { '?' })[0];
				}

				return response;
			}
		}

		public string Host
		{
			get
			{
				string response = String.Empty;
				if (_arguments.IsDefined("i"))
				{
					if(((string)_arguments["i"]).StartsWith("http://"))
					{
						string remainder = ((string)_arguments["i"]).Substring(7);
						response = remainder.Split(new char[] { '/' })[0];
					}
					else if(((string)_arguments["i"]).StartsWith("https://"))
					{
						string remainder = ((string)_arguments["i"]).Substring(8);
						response = remainder.Split(new char[] { '/' })[0];
					}
				}
				else if (_arguments.IsDefined("uri"))
				{
					if (((string)_arguments["uri"]).StartsWith("http://"))
					{
						string remainder = ((string)_arguments["uri"]).Substring(7);
						response = remainder.Split(new char[] { '/' })[0];
					}
					else if (((string)_arguments["uri"]).StartsWith("https://"))
					{
						string remainder = ((string)_arguments["uri"]).Substring(8);
						response = remainder.Split(new char[] { '/' })[0];
					}
				}

				return response;
			}
		}

		public NameValueCollection QueryString
		{
			get
			{
				NameValueCollection response = null;
				if (_arguments.IsDefined("i"))
				{
					if (((string)_arguments["i"]).Split(new char[] { '?' }).Length == 2)
					{
						response = new NameValueCollection();
						string qstring = ((string)_arguments["i"]).Split(new char[] { '?' })[1];
						string[] pairs = qstring.Split(new char[] { '&' });
						foreach (string pair in pairs)
						{
							string[] keyValueParts = pair.Split(new char[] { '=' });
							response.Add(keyValueParts[0], keyValueParts[1]);
						}
					}
				}
				else if (_arguments.IsDefined("uri"))
				{
					if (((string)_arguments["uri"]).Split(new char[] { '?' }).Length == 2)
					{
						response = new NameValueCollection();
						string qstring = ((string)_arguments["uri"]).Split(new char[] { '?' })[1];
						string[] pairs = qstring.Split(new char[] { '&' });
						foreach (string pair in pairs)
						{
							string[] keyValueParts = pair.Split(new char[] { '=' });
							response.Add(keyValueParts[0], keyValueParts[1]);
						}
					}
				}

				return response;
			}
		}

		public string QueryStringAsString
		{
			get
			{
				string response = String.Empty;
				if (_arguments.IsDefined("i"))
				{
					if (((string)_arguments["i"]).Split(new char[] { '?' }).Length == 2)
					{
						response = String.Format("{0}", ((string)_arguments["i"]).Split(new char[] { '?' })[1]);
					}
				}
				else if (_arguments.IsDefined("uri"))
				{
					if (((string)_arguments["uri"]).Split(new char[] { '?' }).Length == 2)
					{
						response = String.Format("{0}", ((string)_arguments["uri"]).Split(new char[] { '?' })[1]);
					}
				}

				return response;
			}
		}

		public string Accept
		{
			get
			{
				return "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			}
		}

		public string AcceptLanguage
		{
			get
			{
				return "en-us,en;q=0.5";
			}
		}

		public string AcceptCharset
		{
			get
			{
				return "ISO-8859-1,utf-8;q=0.7,*;q=0.7";
			}
		}

		public WebHeaderCollection Headers
		{
			get
			{
				WebHeaderCollection response = new WebHeaderCollection();
				response.Add("Accept", Accept);
				response.Add("Accept-Language", AcceptLanguage);
				response.Add("Accept-Charset", AcceptCharset);

				return response;
			}
		}

		public string UserAgent
		{
			get
			{
				string response = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SU 3.1; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; .NET CLR 1.1.4322; Tablet PC 2.0; .NET CLR 3.5.21022)";
				if (_arguments.IsDefined("g") || _arguments.IsDefined("user-agent"))
				{
					string userAgent = (_arguments.IsDefined("g")) ? ((string)_arguments["g"]) : ((string)_arguments["user-agent"]);
					if (_userAgentMap.ContainsKey(userAgent))
					{
						response = _userAgentMap[userAgent];
					}
				}

				return response;
			}
		}

		public Program()
		{
			using (Impersonator impersonator = new Impersonator(Domain, User, Password))
			{
				if (!_arguments.IsDefined("h") && !_arguments.IsDefined("help") && !_arguments.IsDefined("w") && !_arguments.IsDefined("help-with"))
				{
					if (String.IsNullOrEmpty(Url))
					{
						Report(MessageType.ERROR, "You must at least provide a Url to analyze using -i");
					}
					else if (!Url.StartsWith("http://") && !Url.StartsWith("https://"))
					{
						Report(MessageType.ERROR, "Url must start with http:// or https://");
					}
					else
					{
						_webClient = WebRequest.CreateDefault(new Uri(String.Format("{0}?{1}", Url, QueryStringAsString))) as HttpWebRequest;
						_webClient.Credentials = Credentials;
						_webClient.Proxy = Proxy;
						_webClient.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
						_webClient.PreAuthenticate = true;
						_webClient.UserAgent = UserAgent;

						try
						{
							_response = _webClient.GetResponse() as HttpWebResponse;
							if (_response != null)
							{
								using (StreamReader reader = new StreamReader(_response.GetResponseStream()))
								{
									_data = reader.ReadToEnd();
								}
							}
							else
							{
								Report(MessageType.ERROR, String.Format("Could not get a response from the server using {0}", String.Format("{0}?{1}", Url, QueryStringAsString)));
							}

							if (!_arguments.IsDefined("s") && !_arguments.IsDefined("suppress"))
							{
								DisplayResponseHeaders();
							}

							if (_arguments.IsDefined("r") || _arguments.IsDefined("return"))
							{
								string[] returnStuff = (_arguments.IsDefined("r")) ? ((string[])_arguments["r"]) : ((string[])_arguments["return"]);
								foreach (string s in returnStuff)
								{
									switch (s)
									{
										case "data":
											DisplayBody();
											break;
										case "request":
											DisplayRequestHeaders();
											break;
										case "response":
											if (_arguments.IsDefined("s") || _arguments.IsDefined("suppress"))
											{
												DisplayResponseHeaders();
											}
											break;
										default:
											Report(MessageType.ERROR, String.Format("I don't understand return = {0}", s));
											break;
									}
								}
							}

							string[] attributes = new string[0];
							if (_arguments.IsDefined("a") || _arguments.IsDefined("attributes"))
							{
								attributes = (_arguments.IsDefined("a")) ? ((string[])_arguments["a"]) : ((string[])_arguments["attributes"]);
							}

							if (_arguments.IsDefined("f") || _arguments.IsDefined("find"))
							{
								string xpath = (_arguments.IsDefined("f")) ? ((string)_arguments["f"]) : ((string)_arguments["find"]);
								Console.Write("$find = {0}", (new XPathFinder(_data, xpath, attributes)).ToString());
							}
						}
						catch (Exception ex)
						{
							Report(MessageType.ERROR, String.Format("Message: {0}; StackTrace: {1}", ex.Message, ex.StackTrace));
						}
					}
				}
				else // Display help
				{
					if (_arguments.IsDefined("h") || _arguments.IsDefined("help"))
					{
						DisplayHelp();
					}
					else if (_arguments.IsDefined("w") || _arguments.IsDefined("help-with"))
					{
						string val = String.Empty;
						if (_arguments.IsDefined("w"))
						{
							val = (string)_arguments["w"];
						}
						else
						{
							val = (string)_arguments["help-with"];
						}

						switch(val)
						{
							case "user-agent":
								DisplayUserAgentCatalog();
								break;
							case "g":
								DisplayUserAgentCatalog();
								break;
							default:
								Console.WriteLine("I don't know an argument named {0}", val);
								break;
						}
					}
				}
			}
		}

		static void Main(string[] args)
		{
			CreateUserAgentMap();
			CreateArgMap();
			_arguments = new LinuxArguments(args, _argMap);

			new Program();
		}

		private static void CreateUserAgentMap()
		{
			_userAgentMap = new Dictionary<string, string>();
			_userAgentMap.Add("none", String.Empty);
			_userAgentMap.Add("empty", String.Empty);
			_userAgentMap.Add("ie", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; Media Center PC 5.0; .NET CLR 1.1.4322)");
			_userAgentMap.Add("ie8", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; Media Center PC 5.0; .NET CLR 1.1.4322)");
			_userAgentMap.Add("ie7", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SU 3.1; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.0.04506; .NET CLR 1.1.4322; Tablet PC 2.0; .NET CLR 3.5.21022)");
			_userAgentMap.Add("ie6", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; NeosBrowser; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
			_userAgentMap.Add("ie5.5", "Mozilla/4.0 (compatible; MSIE 5.5; Windows 98)");
			_userAgentMap.Add("amaya", "amaya/9.51 libwww/5.4.0");
			_userAgentMap.Add("amaya9.5", "amaya/9.51 libwww/5.4.0");
			_userAgentMap.Add("amaya9.1", "amaya/9.1 libwww/5.4.0");
			_userAgentMap.Add("amaya6.2", "amaya/6.2 libwww/5.3.1");
			_userAgentMap.Add("apt-get", "Ubuntu APT-HTTP/1.3");
			_userAgentMap.Add("camino", "Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en; rv:1.8.1.14) Gecko/20080409 Camino/1.6 (like Firefox/2.0.0.14)");
			_userAgentMap.Add("camino1.6", "Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en; rv:1.8.1.14) Gecko/20080409 Camino/1.6 (like Firefox/2.0.0.14)");
			_userAgentMap.Add("camino1.5", "Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en; rv:1.8.1.6) Gecko/20070809 Camino/1.5.1");
			_userAgentMap.Add("camino1", "Mozilla/5.0 (Macintosh; U; Intel Mac OS X; en-US; rv:1.8.0.1) Gecko/20060118 Camino/1.0b2+");
			_userAgentMap.Add("camino0.7", "Mozilla/5.0 (Macintosh; U; PPC Mac OS X Mach-O; en-US; rv:1.5b) Gecko/20030917 Camino/0.7+");
			_userAgentMap.Add("camino0.6", "Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en-US; rv:1.0.1) Gecko/20021104 Chimera/0.6");
			_userAgentMap.Add("chrome", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/525.13 (KHTML, like Gecko) Chrome/0.2.149.27 Safari/525.13");
			_userAgentMap.Add("elinks", "ELinks/0.11.3-5ubuntu2-lite (textmode; Debian; Linux 2.6.24-19-generic i686; 126x37-2)");
			_userAgentMap.Add("emacs", "Emacs-W3/4.0pre.46 URL/p4.0pre.46 (i686-pc-linux; X11)");
			_userAgentMap.Add("epiphany", "Mozilla/5.0 (X11; U; Linux i686; en-US) AppleWebKit/420+ (KHTML, like Gecko)");
			_userAgentMap.Add("firefox", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.3) Gecko/2008092510 Ubuntu/8.04 (hardy) Firefox/3.0.3");
			_userAgentMap.Add("firefox3", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.3) Gecko/2008092510 Ubuntu/8.04 (hardy) Firefox/3.0.3");
			_userAgentMap.Add("firefox2", "Mozilla/5.0 (X11; U; OpenBSD i386; en-US; rv:1.8.1.14) Gecko/20080821 Firefox/2.0.0.14");
			_userAgentMap.Add("firefox1.5", "Mozilla/5.0 (X11; U; Darwin Power Macintosh; en-US; rv:1.8.0.12) Gecko/20070803 Firefox/1.5.0.12 Fink Community Edition");
			_userAgentMap.Add("firefox1", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.7.13) Gecko/20060410 Firefox/1.0.8");
			_userAgentMap.Add("flock", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.3) Gecko/2008100716 Firefox/3.0.3 Flock/2.0");
			_userAgentMap.Add("flock2", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.3) Gecko/2008100716 Firefox/3.0.3 Flock/2.0");
			_userAgentMap.Add("flock0.7", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.8.0.4) Gecko/20060612 Firefox/1.5.0.4 Flock/0.7.0.17.1");
			_userAgentMap.Add("flock1", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8b5) Gecko/20051019 Flock/0.4 Firefox/1.0+");
			_userAgentMap.Add("iceape", "Mozilla/5.0 (X11; U; GNU/kFreeBSD i686; en-US; rv:1.8.1.16) Gecko/20080702 Iceape/1.1.11 (Debian-1.1.11-1)");
			_userAgentMap.Add("iceweasel", "Mozilla/5.0 (X11; U; GNU/kFreeBSD i686; en-US; rv:1.9.0.1) Gecko/2008071502 Iceweasel/3.0.1 (Debian-3.0.1-1)");
			_userAgentMap.Add("icecat", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.1) Gecko/2008072716 IceCat/3.0.1-g1");
			_userAgentMap.Add("google-mediapartners", "Mediapartners-Google/2.1");
			_userAgentMap.Add("google-sitemaps", "Google-Sitemaps/1.0");
			_userAgentMap.Add("konqueror", "Mozilla/5.0 (compatible; Konqueror/4.0; Linux) KHTML/4.0.5 (like Gecko)");
			_userAgentMap.Add("links", "Links (2.1pre18; Linux 2.6.17-dyne i686; x)");
			_userAgentMap.Add("lynx", "Lynx/2.8.5dev.16 libwww-FM/2.14 SSL-MM/1.4.1 OpenSSL/0.9.6b");
			_userAgentMap.Add("mozilla", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.1b1pre) Gecko/20080915000512 SeaMonkey/2.0a1pre");
			_userAgentMap.Add("seamonkey", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.1b1pre) Gecko/20080915000512 SeaMonkey/2.0a1pre");
			_userAgentMap.Add("netscape", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.8pre) Gecko/20071019 Firefox/2.0.0.8 Navigator/9.0.0.1");
			_userAgentMap.Add("netscape9", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.8pre) Gecko/20071019 Firefox/2.0.0.8 Navigator/9.0.0.1");
			_userAgentMap.Add("netscape8", "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:1.7.5) Gecko/20050519 Netscape/8.0.1");
			_userAgentMap.Add("netscape7", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.7.2) Gecko/20040804 Netscape/7.2 (ax)");
			_userAgentMap.Add("netscape6", "Mozilla/5.0 (Windows; U; WinNT4.0; en-CA; rv:0.9.4) Gecko/20011128 Netscape6/6.2.1");
			_userAgentMap.Add("netscape4", "Mozilla/4.8 [en] (X11; U; Linux 2.4.20-8 i686)");
			_userAgentMap.Add("netscape3", "Mozilla/3.01 (WinNT; I) [AXP]");
			_userAgentMap.Add("netscape2", "Mozilla/2.02 [fr] (WinNT; I)");
			_userAgentMap.Add("netscape1", "Mozilla/0.96 Beta (X11; Linux 2.6.25.18-0.2-default i686)");
			_userAgentMap.Add("olpc", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.8.0.4) Gecko/20061019 pango-text");
			_userAgentMap.Add("onelaptopperchild", "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.8.0.4) Gecko/20061019 pango-text");
			_userAgentMap.Add("opera", "Opera/9.60 (Windows NT 5.1; U; en) Presto/2.1.1");
			_userAgentMap.Add("safariwin", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/525.19 (KHTML, like Gecko) Version/3.1.2 Safari/525.21");
			_userAgentMap.Add("safariiphone", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 2_1 like Mac OS X; en-us) AppleWebKit/525.18.1 (KHTML, like Gecko) Version/3.1.1 Mobile/5F136 Safari/525.20");
			_userAgentMap.Add("safariipod", "Mozilla/5.0 (iPod; U; CPU iPhone OS 2_0 like Mac OS X; de-de) AppleWebKit/525.18.1 (KHTML, like Gecko) Version/3.1.1 Mobile/5A347 Safari/525.20");
			_userAgentMap.Add("safarileopard", "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_5_2; en-us) AppleWebKit/525.13 (KHTML, like Gecko) Version/3.1 Safari/525.13");
			_userAgentMap.Add("wget", "Wget/1.8.1");

			_userAgentMap.Add("default", _userAgentMap["ie"]);
		}

		private static void CreateArgMap()
		{
			_argMap = new Dictionary<string, object>();
			_argMap.Add("i", String.Empty);
			_argMap.Add("uri", String.Empty);
			_argMap.Add("r", new string[1] { "," });
			_argMap.Add("return", new string[1] { "," });
			_argMap.Add("u", String.Empty);
			_argMap.Add("user", String.Empty);
			_argMap.Add("p", String.Empty);
			_argMap.Add("password", String.Empty);
			_argMap.Add("x", String.Empty);
			_argMap.Add("proxy", String.Empty);
			_argMap.Add("e", new string[2] { "&", "=" });
			_argMap.Add("headers", new string[2] { "&", "=" });
			_argMap.Add("t", 0L);
			_argMap.Add("timeout", 0L);
			_argMap.Add("f", String.Empty);
			_argMap.Add("find", String.Empty);
			_argMap.Add("a", new string[1] { "," });
			_argMap.Add("attributes", new string[1] { "," });
			_argMap.Add("o", new string[2] { "&", "=" });
			_argMap.Add("postbody", new string[2] { "&", "=" });
			_argMap.Add("s", null);
			_argMap.Add("suppress", null);
			_argMap.Add("g", String.Empty);
			_argMap.Add("user-agent", String.Empty);
			_argMap.Add("h", null);
			_argMap.Add("help", null);
			_argMap.Add("w", String.Empty);
			_argMap.Add("help-with", String.Empty);
		}

		private void Report(MessageType type, string message)
		{
			string msg = String.Empty;
			switch (type)
			{
				case MessageType.INFO:
					msg = String.Format("{0}", message);
					break;
				case MessageType.ERROR:
					msg = String.Format("Exception caught! {0}", message);
					break;
			}

			Console.WriteLine(msg);
		}

		private void DisplayRequestHeaders()
		{
			if (_response != null)
			{
				string response = String.Empty;
				foreach (string key in _webClient.Headers.AllKeys)
				{
					response = String.Format("{0}{1}", response, String.Format("{0} = {1}::", key, _webClient.Headers[key]));
				}
				Console.Write("$request = {0}; ", response);
			}
			else
			{
				Report(MessageType.ERROR, String.Format("Could not get a response from the server using {0}", String.Format("{0}?{1}", Url, QueryStringAsString)));
			}
		}

		private void DisplayResponseHeaders()
		{
			if (_response != null)
			{
				string response = String.Empty;
				foreach (string key in _response.Headers.AllKeys)
				{
					response = String.Format("{0}{1}", response, String.Format("{0} = {1}::", key, _response.Headers[key]));
				}
				Console.Write("$response = {0}; ", response);
			}
			else
			{
				Report(MessageType.ERROR, String.Format("Could not get a response from the server using {0}", String.Format("{0}?{1}", Url, QueryStringAsString)));
			}
		}

		private void DisplayBody()
		{
			if (_response != null)
			{
				Console.Write("$data = {0}; ", _data);
			}
			else
			{
				Report(MessageType.ERROR, String.Format("Could not get a response from the server using {0}", String.Format("{0}?{1}", Url, QueryStringAsString)));
			}
		}

		private void DisplayUserAgentCatalog()
		{
			Console.WriteLine("User-Agent strings derived from http://www.zytrax.com/tech/web/browser_ids.htm");
			foreach (string key in _userAgentMap.Keys)
			{
				Console.WriteLine("{0}, {1}", key, _userAgentMap[key]);
			}
		}

		private void DisplayHelp()
		{
			Console.WriteLine("HttpAnalyzer v3.0.340 (10.12.2008) Copyright (C) 2008, Nathandelane.");

			License.WriteDisclaimer();

			Console.WriteLine("Help with HttpAnalyzer");
			Console.WriteLine("    HttpAnalyzer is a text-based tool used to browse the Internet, pull down data, and analyze web sites from a text-mode point of view. It is usable for determining whether a web site complies with Accessibility standards");
			Console.WriteLine("Allowed arguments");

			foreach (string arg in _argMap.Keys)
			{
				if (arg.Length == 1)
				{
					if (_argMap[arg] != null)
					{
						Console.WriteLine("-{0}\t\t{1}", arg, _argMap[arg].GetType().Name);
					}
					else
					{
						Console.Write("-{0}\t\t", arg);
						switch (arg)
						{
							case "h":

								break;
							case "help":
								break;
							case "s":
								break;
							case "suppress":
								break;
							default:
								break;
						}
					}
				}
				else
				{
					if (_argMap[arg] != null)
					{
						Console.WriteLine("--{0}\t\t{1}", arg, _argMap[arg]);
					}
					else
					{
						Console.Write("--{0}\t\t", arg);
						switch (arg)
						{
							case "h":
								Console.WriteLine("Displays this help");
								break;
							case "help":
								Console.WriteLine("Displays this help");
								break;
							case "s":
								Console.WriteLine("Suppresses the default output");
								break;
							case "suppress":
								Console.WriteLine("Suppresses the default output");
								break;
							default:
								break;
						}
					}
				}
			}
		}
	}
}

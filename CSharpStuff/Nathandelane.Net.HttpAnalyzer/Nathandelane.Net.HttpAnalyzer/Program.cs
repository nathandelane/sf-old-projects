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

		public Program()
		{
			using (Impersonator impersonator = new Impersonator(Domain, User, Password))
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
		}

		static void Main(string[] args)
		{
			CreateArgMap();
			_arguments = new LinuxArguments(args, _argMap);

			new Program();
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
	}
}

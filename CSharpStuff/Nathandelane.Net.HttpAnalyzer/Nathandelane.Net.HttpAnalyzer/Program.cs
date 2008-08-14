using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Xml;
using HtmlAgilityPack;

namespace Nathandelane.Net.HttpAnalyzer
{
	class Program
	{
		private static Dictionary<string, object> _parameters;

		public Program()
		{
			string domainName = "";
			string userAccount = "";
			string password = "";
			string proxyServer = "";
			int portNumber = 80;

			if (_parameters.ContainsKey("user"))
			{
				if ((_parameters["user"] as String).Contains("\\"))
				{
					domainName = (_parameters["user"] as String).Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[0];
					userAccount = (_parameters["user"] as String).Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1];
				}
				else
				{
					domainName = "";
					userAccount = (_parameters["user"] as String);
				}
			}

			if (_parameters.ContainsKey("password"))
			{
				password = _parameters["password"] as String;
			}

			if (_parameters.ContainsKey("proxy"))
			{
				proxyServer = (_parameters["proxy"] as String).Split(new string[] { ":"}, StringSplitOptions.RemoveEmptyEntries)[0];

				if ((_parameters["proxy"] as String).Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
				{
					portNumber = int.Parse((_parameters["proxy"] as String).Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1]);
				}
			}

			// Create request
			if (_parameters.ContainsKey("url"))
			{
				HttpWebRequest request = WebRequest.Create(_parameters["url"] as String) as HttpWebRequest;
				if (String.IsNullOrEmpty(domainName))
				{
					request.Credentials = new NetworkCredential(userAccount, password);
				}
				else
				{
					request.Credentials = new NetworkCredential(userAccount, password, domainName);
				}

				if (!String.IsNullOrEmpty(proxyServer))
				{
					request.Proxy = new WebProxy(proxyServer, portNumber);
				}

				if (_parameters.ContainsKey("post"))
				{
					request.Method = "POST";
					request.ContentType = "application/x-www-form-urlencoded";

					using (Stream dataStream = request.GetRequestStream())
					{
						string requestData = _parameters["post"] as String;
						string[] items = requestData.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

						foreach (string t in items)
						{
							ASCIIEncoding enc = new ASCIIEncoding();
							dataStream.Write(enc.GetBytes(t), 0, t.Length);
							dataStream.Write(new byte[] { (byte)'&' }, 0, 1);
						}
					}
				}

				foreach (string s in (_parameters["headers"] as string[]))
				{
					if (s.StartsWith("user-agent"))
					{
						request.UserAgent = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
					}
					else if (s.StartsWith("accept"))
					{
						request.Accept = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
					}
					else if (s.StartsWith("content-length"))
					{
						request.ContentLength = long.Parse(s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1]);
					}
					else if (s.StartsWith("content-type"))
					{
						request.ContentType = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
					}
					else if (s.StartsWith("expect"))
					{
						request.Expect = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
					}
					else if (s.StartsWith("keep-alive"))
					{
						request.KeepAlive = bool.Parse(s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1]);
					}
					else if (s.StartsWith("media-type"))
					{
						request.MediaType = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
					}
					else if (s.StartsWith("method"))
					{
						request.Method = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
					}
					else if (s.StartsWith("pre-authenticate"))
					{
						if(s.Contains("true"))
						{
						request.PreAuthenticate = true;
						}
						else
						{
							request.PreAuthenticate = false;
						}
					}
					else if (s.StartsWith("http-version"))
					{
						request.ProtocolVersion = new Version(s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1]);
					}
					else if (s.StartsWith("referer"))
					{
						request.Referer = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
					}
					else if (s.StartsWith("transfer-encoding"))
					{
						request.TransferEncoding = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1];
					}
					else
					{
						string[] hKeyValue = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
						request.Headers.Add(hKeyValue[0], hKeyValue[1]);
					}
				}

				// Make request
				MakeRequest(request);
			}
			else
			{
				throw new WebException();
			}
		}

		public void MakeRequest(HttpWebRequest request)
		{
			request.Timeout = (int)_parameters["timeout"];
			request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
			request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

			WebResponse response = request.GetResponse();

			if (!_parameters.ContainsKey("return"))
			{
				if (!_parameters.ContainsKey("suppress"))
				{
					ShowHeaders(response);
				}
			}
			else if (_parameters.ContainsKey("return"))
			{
				foreach (string r in (_parameters["return"] as string[]))
				{
					switch (r)
					{
						case "request":
							ShowHeaders(request);
							break;
						case "response":
							ShowHeaders(response);
							break;
						case "data":
							ShowContent(response);
							break;
					}
				}
			}

			if (_parameters.ContainsKey("xpath"))
			{
				ShowXPathedContent(response);
			}
		}

		public void ShowHeaders(WebRequest request)
		{
			Console.WriteLine("Headers\n-------");
			foreach (string header in request.Headers.AllKeys)
			{
				Console.WriteLine("{0}: {1}", header, request.Headers[header]);
			}
		}

		public void ShowHeaders(WebResponse response)
		{
			Console.WriteLine("Headers\n-------");
			Console.WriteLine("Response Uri: {0}", response.ResponseUri.AbsoluteUri);
			foreach (string header in response.Headers.AllKeys)
			{
				Console.WriteLine("{0}: {1}", header, response.Headers[header]);
			}
		}

		public void ShowContent(WebResponse response)
		{
			Console.WriteLine("\nContent\n-------");
			using (Stream stream = response.GetResponseStream())
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					string data = reader.ReadToEnd();

					Console.WriteLine(data);
				}
			}
		}

		public void ShowXPathedContent(WebResponse response)
		{
			using (Stream stream = response.GetResponseStream())
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					string data = reader.ReadToEnd();
					HtmlDocument doc = new HtmlDocument();
					doc.LoadHtml(data);
					HtmlAgilityPack.HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(_parameters["xpath"] as String);

					int i = 0;
					foreach (HtmlAgilityPack.HtmlNode node in nodes)
					{
						Console.WriteLine("{0}:", i);

						foreach (HtmlAttribute attr in node.Attributes)
						{
							Console.WriteLine("{0}: {1}", attr.Name, attr.Value);
						}

						if (!String.IsNullOrEmpty(node.InnerHtml))
						{
							Console.WriteLine("InnerHtml: {0}", node.InnerHtml);
						}

						i++;
					}
				}
			}
		}

		static void Main(string[] args)
		{
			_parameters = new Dictionary<string, object>();

			string arguments = String.Join(" ", args);

			_parameters.Add("timeout", 30000);
			_parameters.Add("headers", new string[0]);
			_parameters.Add("accept-cookies", false);

			try
			{
				if (args.Length == 0)
				{
					Help();
				}
				else if (arguments.Contains("-h") || arguments.Contains("--help"))
				{
					Help();
				}
				else if (arguments.Contains("-i"))
				{
					for (int i = 0; i < args.Length; i++)
					{
						if (args[i].Equals("-i") || args[i].Equals("--uri"))
						{
							i++;
							_parameters.Add("url", args[i]);
						}
						else if (args[i].Equals("-u") || args[i].Equals("--user"))
						{
							i++;
							_parameters.Add("user", args[i]);
						}
						else if (args[i].Equals("-p") || args[i].Equals("--password"))
						{
							i++;
							_parameters.Add("password", args[i]);
						}
						else if (args[i].Equals("-x") || args[i].Equals("--proxy"))
						{
							i++;
							_parameters.Add("proxy", args[i]);
						}
						else if (args[i].Equals("-e") || args[i].Equals("--headers"))
						{
							i++;
							_parameters["headers"] = args[i].Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
						}
						else if (args[i].Equals("-t") || args[i].Equals("--timeout"))
						{
							i++;
							_parameters["timeout"] = int.Parse(args[i]);
						}
						else if (args[i].Equals("-r") || args[i].Equals("--return"))
						{
							i++;
							_parameters.Add("return", args[i].Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries));
						}
						else if (args[i].Equals("-f") || args[i].Equals("--find"))
						{
							i++;
							_parameters.Add("xpath", args[i]);
						}
						else if (args[i].Equals("-s") || args[i].Equals("--suppress"))
						{
							_parameters.Add("suppress", null);
						}
						else if (args[i].Equals("-o") || args[i].Equals("--post"))
						{
							i++;
							_parameters.Add("post", args[i]);
						}
						else if (args[i].Equals("-c") || args[i].Equals("--accept-cookies"))
						{
							_parameters["accept-cookies"] = true;
						}
					}

					new Program();
				}
			}
			catch (UriFormatException)
			{
				Console.WriteLine("Urls must start with http://.");
			}
			catch (WebException e)
			{
				Console.WriteLine("{0}", e.Message);
			}
		}

		public static void Help()
		{
			Usage();

			// More content here
		}

		public static void Usage()
		{
			Console.WriteLine("Usage: HttpAnalyzer -i url [-u [domain\\]userAccount -p password] [-x proxy:port] [-e headers (header=value;...)] [-t timeoutInMillis] [-r request,data,response] [-f findXpath] [-o (postkey=value&...)] [-c] [-h]");
		}
	}
}

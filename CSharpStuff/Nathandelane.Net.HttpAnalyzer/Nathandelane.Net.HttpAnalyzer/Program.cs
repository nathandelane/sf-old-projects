using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

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

				WebHeaderCollection headerCollection = new WebHeaderCollection();
				foreach (string s in (_parameters["headers"] as string[]))
				{
					string[] hKeyValue = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
					headerCollection.Add(String.Format("{0}:{1}", hKeyValue[0], hKeyValue[1]));
				}
				request.Headers = headerCollection;

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

			ShowHeaders(response);
		}

		public void ShowHeaders(WebResponse response)
		{
			Console.WriteLine("Headers\n-------");
			foreach (string header in response.Headers.AllKeys)
			{
				Console.WriteLine("{0}: {1}", header, response.Headers[header]);
			}
		}

		public void ShowContent(WebResponse response)
		{
			Console.WriteLine("\nContent\n-------");
			Stream stream = response.GetResponseStream();
			StreamReader reader = new StreamReader(stream);
			string data = reader.ReadToEnd();

			Console.WriteLine(data);
		}

		static void Main(string[] args)
		{
			_parameters = new Dictionary<string, object>();

			string arguments = String.Join(" ", args);

			_parameters.Add("timeout", 30000);
			_parameters.Add("headers", new string[0]);

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
				else if (arguments.Contains("-i") || arguments.Contains("-u") || arguments.Contains("-p") || arguments.Contains("-x") || arguments.Contains("-a"))
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
							_parameters["headers"] = args[i].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
						}
						else if (args[i].Equals("-t") || args[i].Equals("--timeout"))
						{
							i++;
							_parameters["timeout"] = int.Parse(args[i]);
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
			Console.WriteLine("Usage: HttpAnalyzer -i url [-u [domain\\]userAccount -p password] [-x proxy:port] [-e headers (header=value;...)] [-t timeoutInMillis] [-h]");
		}
	}
}

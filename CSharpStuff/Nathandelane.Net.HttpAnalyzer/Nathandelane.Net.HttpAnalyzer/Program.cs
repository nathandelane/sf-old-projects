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
		private string _rssFeedAddress;
		private string _domainName;
		private string _userAccount;
		private string _password;
		private string _proxyServer;

		public Program(string url)
		{
			_rssFeedAddress = url;
			
			// Create request
			HttpWebRequest request = WebRequest.Create(_rssFeedAddress) as HttpWebRequest;

			// Make request
			MakeRequest(request);
		}

		public Program(string url, string user, string password)
		{
			_rssFeedAddress = url;
			if(user.Contains("\\"))
			{
				_domainName = user.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[0];
				_userAccount = user.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1];
			}
			else
			{
				_domainName = url.Split(new string[] { "/"}, StringSplitOptions.RemoveEmptyEntries)[1];
				_userAccount = user;
			}
			_password = password;

			// Create request
			HttpWebRequest request = WebRequest.Create(_rssFeedAddress) as HttpWebRequest;
			if(String.IsNullOrEmpty(_domainName))
			{
				request.Credentials = new NetworkCredential(_userAccount, _password);
			}
			else
			{
				request.Credentials = new NetworkCredential(_userAccount, _password, _domainName);
			}

			// Make request
			MakeRequest(request);
		}

		public Program(string url, string user, string password, string proxy)
		{
			_rssFeedAddress = url;
			if (user.Contains("\\"))
			{
				_domainName = user.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[0];
				_userAccount = user.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1];
			}
			else
			{
				_domainName = "";
				_userAccount = user;
			}
			_password = password;
			_proxyServer = proxy;

			// Create request
			HttpWebRequest request = WebRequest.Create(_rssFeedAddress) as HttpWebRequest;
			if (String.IsNullOrEmpty(_domainName))
			{
				request.Credentials = new NetworkCredential(_userAccount, _password);
			}
			else
			{
				request.Credentials = new NetworkCredential(_userAccount, _password, _domainName);
			}
			request.Proxy = new WebProxy(_proxyServer);

			// Make request
			MakeRequest(request);
		}

		public void MakeRequest(HttpWebRequest request)
		{
			request.Timeout = 30000;
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
			string arguments = String.Join(" ", args);
			string uri = "";
			string user = "";
			string password = "";
			string proxy = "";
			bool[] settings = new bool[] { true, false };

			if (arguments.Contains("-h") || arguments.Contains("--help"))
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
						uri = args[i];
					}
					else if (args[i].Equals("-u") || args[i].Equals("--user"))
					{
						i++;
						user = args[i];
					}
					else if (args[i].Equals("-p") || args[i].Equals("--password"))
					{
						i++;
						password = args[i];
					}
					else if (args[i].Equals("-x") || args[i].Equals("--proxy"))
					{
						i++;
						proxy = args[i];
					}
					else if (args[i].Equals("-a"))
					{
						//settings[0] = true;
						//settings[1] = true;
					}
				}
			}
			else
			{
				if (args.Length == 1)
				{
					uri = args[0];
				}
				else if (args.Length == 3)
				{
					uri = args[0];
					user = args[1];
					password = args[2];
				}
				else if (args.Length == 4)
				{
					uri = args[0];
					user = args[1];
					password = args[2];
					proxy = args[3];
				}
				else
				{
					Console.WriteLine("Invalid number of arguments.");
				}
			}

			try
			{
				if (String.IsNullOrEmpty(uri) || ((String.IsNullOrEmpty(user) && !String.IsNullOrEmpty(password)) || (!String.IsNullOrEmpty(user) && String.IsNullOrEmpty(password))))
				{
					Usage();
				}
				else if (String.IsNullOrEmpty(user) && String.IsNullOrEmpty(password) && String.IsNullOrEmpty(proxy))
				{
					new Program(uri);
				}
				else if (String.IsNullOrEmpty(proxy))
				{
					new Program(uri, user, password);
				}
				else
				{
					new Program(uri, user, password, proxy);
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
			Console.WriteLine("Usage: HttpAnalyzer [-i] url [[-u] [domain\\]userAccount [-p] password] [[-x] proxy] [-h]");
		}
	}
}

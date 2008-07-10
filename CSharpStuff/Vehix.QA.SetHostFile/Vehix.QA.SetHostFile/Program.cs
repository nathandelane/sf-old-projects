using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Vehix.QA.SetHostFile
{
	class Program
	{
		private Settings _settings;
		//private Logger _logger;

		[DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
		private static extern UInt32 DnsFlushResolverCache();

		public Program(string serverName)
		{
			_settings = new Settings();
			//_logger = new Logger(String.Format("{0}\\SetHostFile_{1}.log", Environment.CurrentDirectory, DateTime.Now.Ticks));

			//_logger.Log("Checking whether server is known.");
			if (_settings.ContainsKey(serverName))
			{
				Console.WriteLine("Setting host file for {0}...", serverName);

				//_logger.Log(String.Format("Server is known: {0}", serverName));
				StreamWriter writer = new StreamWriter(_settings["hostFileLocation"]);

				try
				{
					//_logger.Log(String.Format("Writing host file for {0}", serverName));
					writer.WriteLine("# Host file written by Vehix.QA.SetHostFile. Last updated 07/09/2008\n#\n#\n#\n# This is a {0} HOST FILE\n#\n#\n\n\n\n", serverName.ToUpper());

					string[] subdomainCollection = GetSubdomainsForServerName(serverName);
					string ipMask = _settings[serverName];

					//_logger.Log(String.Format("IP Mask is {0}", ipMask));

					foreach (string subdomain in subdomainCollection)
					{
						if (!String.IsNullOrEmpty(subdomain))
						{
							string fullIp = ipMask.Replace("n", _settings[String.Format("hostalias{0}", Capitalize(subdomain))]);
							string possibleAlias = String.Format("alias{0}", Capitalize(subdomain));

							if (_settings.ContainsKey(possibleAlias))
							{
								writer.WriteLine("{0}      {1}", fullIp, _settings[possibleAlias]);
								//_logger.Log(String.Format("Writing host alias {0}      {1}", fullIp, _settings[possibleAlias]));
							}
							else
							{
								writer.WriteLine("{0}      {1}.vehix.com", fullIp, subdomain);
								//_logger.Log(String.Format("Writing host alias {0}      {1}", fullIp, _settings[possibleAlias]));
							}
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception caught: {0}", e.Message);
					Console.WriteLine("Stack Trace: {0}", e.StackTrace);
					//_logger.Log(String.Format("Exception caught: {0}", e.Message));
					//_logger.Log(String.Format("Stack Trace: {0}", e.StackTrace));
				}
				finally
				{
					//_logger.Log("Closing writer");
					writer.Close();
					//_logger.Log("Closing logger");
					//_logger.Close();
				}
			}
			else
			{
				//_logger.Log(String.Format("Server is not known: {0}", serverName));
				Console.WriteLine("Server named {0} is invalid. Please check your server name as well as configuration.", serverName);
			}
		}

		public string[] GetSubdomainsForServerName(string serverName)
		{
			string[] subdomains = new string[0];
			string farmGroup = null;

			if (!String.IsNullOrEmpty(serverName))
			{
				//_logger.Log(String.Format("Getting subdomains for {0}", serverName));
				// Check each farmgroup to see if the server falls within it and set group
				foreach (string key in GetFarmGroups())
				{
					if (_settings[key].Contains(serverName))
					{
						// Get group aliases for the farm group
						//_logger.Log(String.Format("Farm Group Found: {0}", key));
						farmGroup = key;
						subdomains = GetGroupAliases(farmGroup);
					}
				}
			}
			else
			{
				//_logger.Log("farmGroup was null or empty");
			}

			return subdomains;
		}

		/// <summary>
		/// Splt up the group aliases
		/// </summary>
		/// <param name="group"></param>
		/// <returns></returns>
		public string[] GetGroupAliases(string farmGroup)
		{
			string[] groupAliases = new string[0];

			if (!String.IsNullOrEmpty(farmGroup))
			{
				//_logger.Log(String.Format("Getting group aliases for farm group {0}", farmGroup));
				string group = String.Format("group{0}", farmGroup.Substring(9));
				//_logger.Log(String.Format("Group name is {0}", group));

				groupAliases = _settings[group].Split(new char[] { ',' });
			}
			else
			{
				//_logger.Log("farmGroup was null or empty");
			}

			return groupAliases;
		}

		/// <summary>
		/// Get the farmgroup entries from the configuration.
		/// </summary>
		/// <returns></returns>
		public List<string> GetFarmGroups()
		{
			List<string> farmGroups = new List<string>();

			foreach (string key in _settings.Keys)
			{
				if (key.StartsWith("farmgroup"))
				{
					farmGroups.Add(key);
				}
			}

			return farmGroups;
		}

		public string Capitalize(string value)
		{
			string upcValue = value.ToUpper();
			string result = String.Format("{0}{1}", upcValue.Substring(0, 1), value.Substring(1));

			return result;
		}

		public static void FlushResolverCache()
		{
			Console.WriteLine("Flushing DNS...");
			DnsFlushResolverCache();
		}

		static void Main(string[] args)
		{
			if (args.Length < 1 || args.Length > 1)
			{
				Console.WriteLine("One argument expected: <servername>\nUsage: SetHostFile <serverName>\n\nExample: SetHostFile prod-web-01\n");
			}
			else if (args.Length == 1)
			{
				Console.WriteLine("");
				new Program(args[0].ToLower());
				FlushResolverCache();
				Console.WriteLine("Done setting host file.");
			}
		}
	}
}

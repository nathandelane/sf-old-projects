using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Configuration;
using Nathandelane.HostsFileSetter.Configuration;

namespace Nathandelane.HostsFileSetter.Models
{
	public class HostsFileCollectionModel : ObservableCollection<AbstractHostsFileConfiguration>
	{
		#region Fields

		private string _domain;
		private string _defaultIp;
		private bool _includeWww;

		#endregion

		#region Constructors

		public HostsFileCollectionModel()
		{
			Add(new BlankHostsFileConfiguration());

			LoadServerConfiguration();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Loads the server configuration.
		/// </summary>
		private void LoadServerConfiguration()
		{
			_domain = ConfigurationManager.AppSettings["domain"];
			_defaultIp = ConfigurationManager.AppSettings["defaultIp"];
			_includeWww = (ConfigurationManager.AppSettings["includeWww"].Equals("true", StringComparison.InvariantCultureIgnoreCase)) ? true : false;

			ServerConfigurationSection servers = (ServerConfigurationSection)ConfigurationManager.GetSection("serverConfigSection");

			if (servers != null)
			{
				foreach (ServerElement element in servers.Elements)
				{
					this.Add(new ServerHostsFileConfiguration(element.Name, GenerateDnsEntries(element.IpMask, element.Pool)));
				}
			}
		}

		/// <summary>
		/// Generates a list of DnsEntry objects based on an ip mask.
		/// </summary>
		/// <param name="ipMask"></param>
		/// <returns></returns>
		private IList<DnsEntry> GenerateDnsEntries(string ipMask, string pool)
		{
			IList<DnsEntry> dnsEntries = new List<DnsEntry>();

			if (pool.Contains("consumer"))
			{
				HatConfigurationSection hats = (HatConfigurationSection)ConfigurationManager.GetSection("hatConfigSection");

				foreach (HatElement hat in hats.Elements)
				{
					dnsEntries.Add(new DnsEntry() { Name = String.Format("{0}.{1}", hat.Name, _domain), IpAddress = ipMask.Replace("n", hat.Value) });
				}

				if (_includeWww)
				{
					dnsEntries.Add(new DnsEntry() { Name = String.Format("www.{0}", _domain), IpAddress = ipMask.Replace("n", _defaultIp) });
				}

				dnsEntries.Add(new DnsEntry() { Name = _domain, IpAddress = ipMask.Replace("n", _defaultIp) });
			}

			if (pool.Contains("admin"))
			{
				AdminConfigurationSection admin = (AdminConfigurationSection)ConfigurationManager.GetSection("adminConfigSection");

				foreach (AdminElement nextAdmin in admin.Elements)
				{
					dnsEntries.Add(new DnsEntry() { Name = String.Format("{0}.{1}", nextAdmin.Name, _domain), IpAddress = ipMask.Replace("n", nextAdmin.Value) });
				}
			}

			return dnsEntries;
		}

		#endregion
	}
}

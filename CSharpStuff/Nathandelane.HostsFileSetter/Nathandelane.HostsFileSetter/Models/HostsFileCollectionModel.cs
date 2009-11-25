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
			ServerConfigurationSection servers = (ServerConfigurationSection)ConfigurationManager.GetSection("serverConfigSection");

			if (servers != null)
			{
				foreach (ServerElement element in servers.Elements)
				{
					this.Add(new ServerHostsFileConfiguration(element.Name, GenerateDnsEntries(element.IpMask)));
				}
			}
		}

		/// <summary>
		/// Generates a list of DnsEntry objects based on an ip mask.
		/// </summary>
		/// <param name="ipMask"></param>
		/// <returns></returns>
		private IList<DnsEntry> GenerateDnsEntries(string ipMask)
		{
			IList<DnsEntry> dnsEntries = new List<DnsEntry>();
			HatConfigurationSection hats = (HatConfigurationSection)ConfigurationManager.GetSection("hatConfigSection");

			foreach (HatElement hat in hats.Elements)
			{
				dnsEntries.Add(new DnsEntry() { Name = hat.Name, IpAddress = ipMask.Replace("n", hat.Value) });
			}

			return dnsEntries;
		}

		#endregion
	}
}

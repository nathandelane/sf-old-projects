using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.HostsFileSetter.Models
{
	public class HostsFileCollectionModel : ObservableCollection<AbstractHostsFileConfiguration>
	{
		#region Constructors

		public HostsFileCollectionModel()
		{
		}

		#endregion

		#region Methods

		private void LoadServerConfiguration()
		{
			ServerCollection servers = (ServerCollection)ConfigurationManager.GetSection("servers");

			foreach (ServerElement element in servers)
			{
				Add(new ServerHostsFileConfiguration(element.Name, GenerateDnsEntries(element.IpMask)));
			}
		}

		private IList<DnsEntry> GenerateDnsEntries(string ipMask)
		{
			IList<DnsEntry> dnsEntries = new List<DnsEntry>();
			HatCollection hats = (HatCollection)ConfigurationManager.GetSection("hats");

			foreach (HatElement hat in hats)
			{
				dnsEntries.Add(new DnsEntry() { Name = hat.Name, IpAddress = ipMask.Replace("n", hat.Value) });
			}

			return dnsEntries;
		}

		#endregion
	}
}

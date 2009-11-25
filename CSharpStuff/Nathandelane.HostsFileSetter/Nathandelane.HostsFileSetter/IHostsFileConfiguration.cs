using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.HostsFileSetter
{
	/// <summary>
	/// All Hosts files configurations should implement this interface.
	/// </summary>
	public interface IHostsFileConfiguration
	{
		/// <summary>
		/// Gets any comments that will appear at the top of the hosts file configuration.
		/// </summary>
		string Comments { get; set; }

		/// <summary>
		/// Gets a list of Dns Entries that will appear in the hosts file configuration.
		/// </summary>
		IList<DnsEntry> Entries { get; set; }
	}
}

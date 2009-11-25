using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.HostsFileSetter
{
	public class ServerHostsFileConfiguration : AbstractHostsFileConfiguration
	{
		#region Constructors

		public ServerHostsFileConfiguration(string serverName, IEnumerable<DnsEntry> entries)
			: base(String.Format(StringTable.DefaultHostsFileHeader, String.Format(StringTable.ServerComments, serverName)), entries)
		{
		}

		#endregion
	}
}

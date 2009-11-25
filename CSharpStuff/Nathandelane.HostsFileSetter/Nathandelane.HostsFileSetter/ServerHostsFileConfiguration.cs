using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.HostsFileSetter
{
	public class ServerHostsFileConfiguration : AbstractHostsFileConfiguration
	{
		#region Fields

		private string _serverName;

		#endregion

		#region Properties

		public string ServerName
		{
			get { return _serverName; }
		}

		#endregion

		#region Constructors

		public ServerHostsFileConfiguration(string serverName, IEnumerable<DnsEntry> entries)
			: base(String.Format(StringTable.DefaultHostsFileHeader, String.Format(StringTable.ServerComments, serverName)), entries)
		{
			_serverName = serverName;
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			return _serverName;
		}

		#endregion
	}
}

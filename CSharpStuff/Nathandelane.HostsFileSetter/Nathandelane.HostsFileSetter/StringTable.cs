using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Nathandelane.HostsFileSetter
{
	public static class StringTable
	{
		public static readonly string ServerComments = "This is a {0} HOSTS FILE.";
		public static readonly string DefaultHostsFileHeader = String.Format("# Hosts file written by Nathandelane.HostsFileSetter. Last Updated on {0}\r\n#\r\n#\r\n#\r\n# {1}\r\n#\r\n#", DateTime.Today.ToString(CultureInfo.InvariantCulture), StringTable.ServerComments);
	}
}

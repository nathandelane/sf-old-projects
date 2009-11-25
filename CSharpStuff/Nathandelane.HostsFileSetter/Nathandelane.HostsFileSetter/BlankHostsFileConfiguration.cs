using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Nathandelane.HostsFileSetter
{
	public class BlankHostsFileConfiguration : AbstractHostsFileConfiguration
	{
		#region Fields

		private static readonly string __message = "This is a BLANK HOSTS FILE.";

		#endregion

		#region Constructors

		public BlankHostsFileConfiguration()
			: base(String.Format(StringTable.DefaultHostsFileHeader, BlankHostsFileConfiguration.__message))
		{
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			return "Blank";
		}

		#endregion
	}
}

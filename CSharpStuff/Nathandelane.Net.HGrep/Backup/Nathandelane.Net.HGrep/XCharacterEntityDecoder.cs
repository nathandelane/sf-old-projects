using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.HGrep
{
	public class XCharacterEntityDecoder
	{
		#region Fields

		private static readonly Dictionary<string, string> __entities = new Dictionary<string, string>()
		{
			{ "nbsp", " " },
			{ "amp", "&" },
			{ "brvbar", "¦" },
			{ "copy", "©" },
			{ "reg", "®" },
			{ "deg", "º" },
			{ "para", "¶" },
			{ "lt", "<" },
			{ "gt", ">" },
			{ "quot", "\"" },
			{ "trade", "™" }
		};

		#endregion

		#region Methods

		public static string Decode(string raw)
		{
			foreach (string key in __entities.Keys)
			{
				string value = String.Concat("&", key, ";");

				raw = raw.Replace(value, __entities[key]);
			}

			return raw;
		}

		#endregion
	}
}

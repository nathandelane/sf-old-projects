using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	internal class Settings
	{
		private Dictionary<string, string> _settings;
		private Dictionary<string, string> _zones;

		public string this[string key]
		{
			get { return _settings[key]; }
		}

		public Settings()
		{
			_settings = new Dictionary<string, string>();
			_zones = new Dictionary<string, string>();

			LoadSettings();
		}

		private void LoadSettings()
		{
			foreach (string key in ConfigurationManager.AppSettings.Keys)
			{
				if (key.StartsWith("zone"))
				{
					string[] pages = ConfigurationManager.AppSettings[key].Split(new char[] { ',' });

					foreach (string nextPage in pages)
					{
						if (!_zones.ContainsKey(nextPage))
						{
							_zones.Add(nextPage, key.Substring(4).ToLower());
						}
					}
				}
				else
				{
					_settings.Add(key, ConfigurationManager.AppSettings[key]);
				}
			}
		}

		internal string GetZoneFor(string pageName)
		{
			string zone = String.Empty;

			if (_zones.ContainsKey(pageName))
			{
				zone = _zones[pageName];
			}

			return zone;
		}

		internal bool ContainsKey(string key)
		{
			bool containsKey = false;

			if (_settings.ContainsKey(key))
			{
				containsKey = true;
			}

			return containsKey;
		}
	}
}

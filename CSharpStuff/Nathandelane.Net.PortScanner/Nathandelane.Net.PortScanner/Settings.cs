using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Nathandelane.Net.PortScanner
{
	public class Settings
	{
		private Dictionary<String, String> _settings;

		public Settings()
		{
			_settings = new Dictionary<string, string>();

			LoadSpiderSettings();
		}

		private void LoadSpiderSettings()
		{
			string[] keys = ConfigurationManager.AppSettings.AllKeys;

			for (int i = 0; i < keys.Length; i++)
			{
				_settings.Add(keys[i], ConfigurationManager.AppSettings[keys[i]]);
			}
		}

		public String this[string key]
		{
			get { return _settings[key]; }
		}
	}
}

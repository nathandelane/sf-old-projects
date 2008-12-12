using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Nathandelane.Net.Spider
{
	public class Settings
	{
		private Dictionary<string, string> _settings;

		public string this[string key]
		{
			get { return _settings[key]; }
		}

		public Settings()
		{
			_settings = new Dictionary<string, string>();

			LoadSettings();
		}

		private void LoadSettings()
		{
			foreach (string key in ConfigurationManager.AppSettings.Keys)
			{
				_settings.Add(key, ConfigurationManager.AppSettings[key]);
			}
		}
	}
}

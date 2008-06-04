using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Nathandelane.Net.Irc
{
	public class IrcSettings
	{
		private Dictionary<string, string> _settings;

		public IrcSettings()
		{
			_settings = new Dictionary<string, string>();

			LoadConfiguration();
		}

		private void LoadConfiguration()
		{
			string[] keys = ConfigurationManager.AppSettings.AllKeys;

			foreach (string key in keys)
			{
				_settings.Add(key, ConfigurationManager.AppSettings[key]);
			}
		}

		public string this[string key]
		{
			get
			{
				string retVal = null;

				if (_settings.ContainsKey(key))
				{
					retVal = _settings[key];
				}

				return retVal;
			}
		}
	}
}

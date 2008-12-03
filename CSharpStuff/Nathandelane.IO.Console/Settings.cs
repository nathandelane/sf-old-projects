using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.IO.Console
{
	public class Settings
	{
		private static Dictionary<string, string> _settings = new Dictionary<string, string>();

		public Settings()
		{
			LoadSettings();
			CurrentDirectory = _settings["defaultDirectory"];
		}

		public string CurrentDirectory
		{
			get { return _settings["currentDirectory"]; }
			set { _settings["currentDirectory"] = value; }
		}

		public string this[string key]
		{
			get { return _settings[key]; }
			set { _settings[key] = value; }
		}

		private void LoadSettings()
		{
			string[] keys = ConfigurationManager.AppSettings.AllKeys;

			foreach (string key in keys)
			{
				_settings.Add(key, ConfigurationManager.AppSettings[key]);
			}
		}
	}
}

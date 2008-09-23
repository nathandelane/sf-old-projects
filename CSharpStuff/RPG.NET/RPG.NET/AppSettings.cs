using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Nathandelane.Security.Rpg
{
	public class AppSettings
	{
		private Dictionary<String, String> _settings;

		public AppSettings()
        {
            _settings = new Dictionary<string, string>();

            LoadSettings();
        }

        private void LoadSettings()
        {
            string[] keys = ConfigurationManager.AppSettings.AllKeys;

            for(int i = 0; i < keys.Length; i++)
            {
                _settings.Add(keys[i], ConfigurationManager.AppSettings[keys[i]]);
            }
        }

        public String this[string key]
        {
            get
			{
				string retVal = null;

				if (ContainsKey(key))
				{
					retVal = _settings[key];
				}

				return retVal; 
			}
        }

		public bool ContainsKey(string key)
		{
			return _settings.ContainsKey(key);
		}

		public Dictionary<string, string>.KeyCollection Keys
		{
			get { return _settings.Keys; }
		}
	}
}

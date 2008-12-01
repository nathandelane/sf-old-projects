using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nathandelane.IO.Console
{
	public class Settings
	{
		private Dictionary<string, string> _properties;

		public Settings()
		{
			_properties = new Dictionary<string, string>();

			LoadProperties();
		}

		public string this[string key]
		{
			get { return _properties[key]; }
		}

		private void LoadProperties()
		{
			string[] keys = ConfigurationManager.AppSettings.AllKeys;

			foreach (string key in keys)
			{
				_properties.Add(key, ConfigurationManager.AppSettings[key]);
			}
		}
	}
}

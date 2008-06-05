using System;
using System.Collections.Generic;
using System.Text;

namespace Vehix.Net.Irc
{
	public class UserSettings
	{
		public static readonly string Channel = "channel";
		public static readonly string Server = "server";
		public static readonly string RealName = "realname";
		public static readonly string NickName = "nickname";
		public static readonly string Password = "password";

		private static Dictionary<string, string> _settings;

		public UserSettings()
		{
			if (UserSettings._settings == null)
			{
				UserSettings._settings = new Dictionary<string, string>();
			}

			_settings[UserSettings.Channel] = "vehix";
			_settings[UserSettings.Server] = "irc.freenode.net";
		}

		public string this[string key]
		{
			get { return UserSettings._settings[key]; }
			set { UserSettings._settings[key] = value; }
		}
	}
}

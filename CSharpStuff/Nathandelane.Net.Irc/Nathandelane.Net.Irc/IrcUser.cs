using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Net.Irc
{
	public class IrcUser
	{
		private string _nick;
		private string _password;
		private string _realName;
		private bool _isVisible;

		public IrcUser(string nick, string realName, bool isVisible)
		{
			if (String.IsNullOrEmpty(nick))
			{
				throw new ArgumentException("nick may not be null.");
			}

			_realName = realName;
			_nick = nick;
			_password = "";
			_isVisible = isVisible;
		}

		public IrcUser(string nick, string realName, string password, bool isVisible)
		{
			if (String.IsNullOrEmpty(nick))
			{
				throw new ArgumentException("nick may not be null.");
			}

			if (String.IsNullOrEmpty(password))
			{
				throw new ArgumentException("password may not be null.");
			}

			_realName = realName;
			_nick = nick;
			_password = password;
			_isVisible = isVisible;
		}

		public string RealName
		{
			get { return _realName; }
		}

		public string Nick
		{
			get { return _nick; }
		}

		public string Password
		{
			get { return _password; }
		}

		public string IsVisible
		{
			get
			{
				string retVal = _isVisible ? "8" : "0";
				return retVal;
			}
		}
	}
}

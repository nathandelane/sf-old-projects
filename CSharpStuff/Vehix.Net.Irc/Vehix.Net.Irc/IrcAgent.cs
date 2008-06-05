using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Vehix.Net.Irc
{
	public class IrcAgent
	{
		private UserSettings _settings;
		private TcpClient _client;
		private StreamReader _reader;
		private StreamWriter _writer;

		public IrcAgent()
		{
			_settings = new UserSettings();
			_client = new TcpClient(_settings["server"], Convert.ToInt32(_settings["port"]));

			NetworkStream stream = _client.GetStream();

			_reader = new StreamReader(stream);
			_writer = new StreamWriter(stream);
		}

		public void WriteCommand(string command)
		{
			_writer.WriteLine(command);
			_writer.Flush();
		}

		public string ReadCommand()
		{
			return _reader.ReadLine();
		}

		public string Pass(string password)
		{
			return String.Format("PASS {0}", password);
		}

		public string Nick(string nickName)
		{
			return String.Format("NICK {0}", nickName);
		}

		public string User(string nickName, string realName, bool isVisible)
		{
			if(isVisible)
			{
				_settings["isVisible"] = "8"; // True
			}
			else
			{
				_settings["isVisible"] = "0"; // False
			}

			return String.Format("USER {0} {1} * :{2}", nickName, _settings["isVisible"], realName);
		}

		public string Join(string channel)
		{
			string retVal = String.Format("JOIN {0}", channel);

			if (!channel.StartsWith("#"))
			{
				retVal = String.Format("JOIN #{0}", channel);
			}

			return retVal;
		}

		public string Quit()
		{
			return "QUIT";
		}

		public string Quit(string message)
		{
			return String.Format("QUIT {0}", message);
		}

		public string Topic(string topic)
		{
			return String.Format("TOPIC {0} {1}", _settings["channel"], topic);
		}

		public string Names()
		{
			return "NAMES";
		}

		public string Names(string channel)
		{
			return String.Format("NAMES {0}", channel);
		}

		public string List()
		{
			return "LIST";
		}

		public string Invite(string nickName, string channel)
		{
			string retVal = String.Format("INVITE {0} {1}", nickName, channel);

			if (!channel.StartsWith("#"))
			{
				retVal = String.Format("INVITE {0} #{1}", nickName, channel);
			}

			return retVal;
		}

		public string Kick(string channel, string user)
		{
			string retVal = String.Format("KICK {0} {1}", channel, user);

			if (!channel.StartsWith("#"))
			{
				retVal = String.Format("KICK #{0} {1}", channel, user);
			}

			return retVal;
		}

		public string Time()
		{
			return "TIME";
		}

		public string PrivMsg(string[] users, string message)
		{
			string userList = String.Join(", ", users);

			return String.Format("PRIVMSG {0} {1}", userList, message);
		}

		public void Initialize()
		{
			// PASS, NICK, USER
			WriteCommand(Pass(_settings[UserSettings.Password]));
			WriteCommand(Nick(_settings[UserSettings.NickName]));
			WriteCommand(User(_settings[UserSettings.NickName], _settings[UserSettings.RealName], true));
		}
	}
}

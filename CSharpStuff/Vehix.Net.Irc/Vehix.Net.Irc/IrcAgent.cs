using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Vehix.Net.Irc
{
	public static class IrcCommand
	{
		public static readonly string Topic = "332";
		public static readonly string TopicOwner = "333";
		public static readonly string NamesList = "353";
		public static readonly string EndOfNamesList = "366";
		public static readonly string MOTD = "372";
		public static readonly string EndOfMOTD = "376";
		public static readonly string JoinChannel = "JOIN";
		public static readonly string Quit = "QUIT";
		public static readonly string ChangeTopic = "TOPIC";
		public static readonly string Nick = "NICK";
		public static readonly string GetNames = "NAMES";
		public static readonly string ListChannels = "LIST";
		public static readonly string InviteUser = "INVITE";
		public static readonly string KickUser = "KICK";
	}

	public class IrcAgent
	{
		private UserSettings _settings;
		private TcpClient _client;
		private StreamReader _reader;
		private StreamWriter _writer;
		private string _motd;
		private string _topic;
		private string _topicOwner;
		private string _channelMode;
		private List<string> _userList;
		private List<string> _serverResponseQueue;

		public IrcAgent()
		{
			_settings = new UserSettings();
			_client = new TcpClient(_settings["server"], Convert.ToInt32(_settings["port"]));

			NetworkStream stream = _client.GetStream();

			_reader = new StreamReader(stream);
			_writer = new StreamWriter(stream);
			_userList = new List<string>();
		}

		public string MOTD
		{
			get { return _motd; }
		}

		public string Topic
		{
			get { return _topic; }
		}

		public string TopicOwner
		{
			get { return _topicOwner; }
		}

		public string ServerResponse
		{
			get
			{
				string retVal = "";

				if (_serverResponseQueue.Count > 0)
				{
					string response = _serverResponseQueue[0];
					_serverResponseQueue.RemoveAt(0);
					retVal = response;
				}

				return retVal;
			}

			set
			{
				if (_serverResponseQueue == null)
				{
					_serverResponseQueue = new List<string>();
				}

				_serverResponseQueue.Add(value);
			}
		}

		public List<string> UserList
		{
			get { return _userList; }
		}

		public string ChannelMode
		{
			get { return _channelMode; }
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

		public string JoinChannel(string channel)
		{
			string retVal = String.Format("JOIN {0}", channel);

			if (!channel.StartsWith("#"))
			{
				retVal = String.Format("JOIN #{0}", channel);
			}

			return retVal;
		}

		public string QuitIrc()
		{
			return "QUIT";
		}

		public string QuitIrc(string message)
		{
			return String.Format("QUIT {0}", message);
		}

		public string ChangeTopic(string topic)
		{
			return String.Format("TOPIC {0} {1}", _settings["channel"], topic);
		}

		public string GetNames()
		{
			return "NAMES";
		}

		public string GetNames(string channel)
		{
			return String.Format("NAMES {0}", channel);
		}

		public string ListChannels()
		{
			return "LIST";
		}

		public string InviteUser(string nickName, string channel)
		{
			string retVal = String.Format("INVITE {0} {1}", nickName, channel);

			if (!channel.StartsWith("#"))
			{
				retVal = String.Format("INVITE {0} #{1}", nickName, channel);
			}

			return retVal;
		}

		public string KickUser(string channel, string user)
		{
			string retVal = String.Format("KICK {0} {1}", channel, user);

			if (!channel.StartsWith("#"))
			{
				retVal = String.Format("KICK #{0} {1}", channel, user);
			}

			return retVal;
		}

		public string GetTime()
		{
			return "TIME";
		}

		public string SendPrivMsg(string[] users, string message)
		{
			string userList = String.Join(", ", users);

			return String.Format("PRIVMSG {0} {1}", userList, message);
		}

		public string SendPrivMsg(string channel, string message)
		{
			string retVal = String.Format("PRIVMSG {0} {1}", channel, message);

			if (!channel.StartsWith("#"))
			{
				retVal = String.Format("PRIVMSG #{0} {1}", channel, message);
			}

			return retVal;
		}

		public void Initialize()
		{
			// PASS, NICK, USER
			if (!String.IsNullOrEmpty(_settings[UserSettings.Password]))
			{
				WriteCommand(Pass(_settings[UserSettings.Password]));
			}

			WriteCommand(Nick(_settings[UserSettings.NickName]));
			WriteCommand(User(_settings[UserSettings.NickName], _settings[UserSettings.RealName], true));

			string ircCommandString;
			while (!GetCommand((ircCommandString = ReadCommand())).Equals(IrcCommand.EndOfMOTD))
			{
				switch (GetCommand(ircCommandString))
				{
					case "332": // Topic
						ReadTopic(ircCommandString);
						break;
					case "333": // Topic owner
						ReadTopicOwner(ircCommandString);
						break;
					case "353": // Names list
						//NamesList(commandParts);
						break;
					case "366":	// End of names list
						//EndOfNamesList(commandParts);
						break;
					case "372": // Irc MOTD
						ReadMOTD(ircCommandString);
						break;
					case "376": // Irc End MOTD
						break;
					default: // Irc Server Message
						//Console.WriteLine("{0}", ircCommand);
						break;
				}
			}
		}

		private string GetCommand(string ircCommandString)
		{
			string retVal = "";

			if (!String.IsNullOrEmpty(ircCommandString))
			{
				string[] commandParts = ircCommandString.Split(' ');

				retVal = commandParts[1];
			}

			return retVal;
		}

		private void ReadTopic(string ircCommandString)
		{
			_topic = ircCommandString.Split(' ')[2];
		}

		private void ReadTopicOwner(string ircCommandString)
		{
			_topicOwner = ircCommandString.Split(' ')[2];
		}

		private void ReadMOTD(string ircCommandString)
		{
			if (_motd == null)
			{
				_motd = "";
			}

			_motd += ircCommandString.Split('-')[1];
		}

		internal void SendCommand(string command)
		{
			string[] ircCommandString = command.Split(' ');
			string cmd = ircCommandString[0];

			if (ircCommandString[0].StartsWith("/"))
			{
				cmd = ircCommandString[0].Substring(1).ToUpper();
			}

			switch (cmd)
			{
				case "TOPIC":
					WriteCommand(ChangeTopic(ircCommandString[1]));
					break;
				case "NAMES":
					WriteCommand(GetNames());
					
					string commandResult = "";
					while (!GetCommand((commandResult = ServerResponse)).Equals(IrcCommand.NamesList)) ;

					string[] users = commandResult.Split(':')[2].Split(' ');
					_userList.Clear();
					foreach (string user in users)
					{
						_userList.Add(user);
					}

					break;
				case "INVITE":
					WriteCommand(InviteUser(ircCommandString[1], ircCommandString[2]));
					break;
				case "JOIN":
					WriteCommand(JoinChannel(ircCommandString[1]));
					_settings[UserSettings.Channel] = ircCommandString[1];
					break;
				case "KICK":
					WriteCommand(KickUser(ircCommandString[1], ircCommandString[2]));
					break;
				case "LIST":
					WriteCommand(ListChannels());
					break;
				case "NICK":
					WriteCommand(Nick(ircCommandString[1]));
					break;
				case "QUIT":
					if (ircCommandString.Length > 1)
					{
						WriteCommand(QuitIrc(ircCommandString[1]));
					}
					else
					{
						WriteCommand(QuitIrc());
					}
					
					break;
				default:
					if (!String.IsNullOrEmpty(command))
					{
						WriteCommand(SendPrivMsg(_settings[UserSettings.Channel], command));
					}
					break;
			}

			string result = "";
			while (!GetCommand((result = ServerResponse)).Equals(IrcCommand.EndOfNamesList))
			{
				if (String.IsNullOrEmpty(result))
				{
					break;
				}

				if (GetCommand(result).Equals(IrcCommand.NamesList))
				{
					string[] users = result.Split(':')[2].Split(' ');
					_userList.Clear();
					foreach (string user in users)
					{
						_userList.Add(user);
					}
				}
				else if (GetCommand(result).ToUpper().Equals("MODE"))
				{
					string[] modeParts = result.Split(' ');
					_channelMode = String.Join(" ", modeParts, 1, (modeParts.Length - 1));
				}
			}
		}

		public void GetCommandResult()
		{
			string response = ReadCommand();
			ServerResponse = response;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Nathandelane.Net.Irc
{
	public class IrcConnection
	{
		private int _port;
		private string _host;
		private string _channel;
		private string _motd;
		private int _motdLineCount;
		private IrcUser _user;
		private TcpClient _clientSocket;
		private StreamWriter _ircWriter;
		private StreamReader _ircReader;
		private IPEndPoint _ipEndPoint;

		public delegate void CommandReceived(string ircCommand);
		public delegate void TopicSet(string channel, string topic);
		public delegate void TopicOwner(string channel, string user, string date);
		public delegate void NamesList(string ircCommand);
		public delegate void ServerMessage(string ircCommand);
		public delegate void Join(string channel, string user);
		public delegate void Part(string channel, string user);
		public delegate void Mode(string channel, string user, string mode);
		public delegate void NickChange(string oldNick, string newNick);
		public delegate void Kick(string channel, string userKicker, string userKicked, string message);
		public delegate void Quit(string user, string quitMessage);

		public event CommandReceived eventReceiving;
		public event TopicSet eventTopicSet;
		public event TopicOwner eventTopicOwner;
		public event NamesList eventNamesList;
		public event ServerMessage eventServerMessage;
		public event Join eventJoin;
		public event Part eventPart;
		public event Mode eventMode;
		public event NickChange eventNickChange;
		public event Kick eventKick;
		public event Quit eventQuit;

		public IrcConnection(string server, int port, IrcUser user)
		{
			_motdLineCount = 0;
			_motd = "";
			_user = user;
			_port = port;
			_host = server;
			_ipEndPoint = new IPEndPoint(IPAddress.Parse("10.1.101.20"), 0);
		}

		public int Port
		{
			get { return _port; }
			set { _port = value; }
		}

		public string Server
		{
			get { return _host; }
			set { _host = value; }
		}

		public string Channel
		{
			get { return _channel; }
			set { _channel = value; }
		}

		public bool Connected
		{
			get
			{
				bool retVal = false;

				if (_clientSocket != null)
				{
					retVal = _clientSocket.Connected;
				}

				return retVal;
			}
		}

		public NetworkStream Stream
		{
			get {
				NetworkStream stream = null;

				if (_clientSocket != null)
				{
					stream = _clientSocket.GetStream();
				}

				return stream;
			}
		}

		public void Connect()
		{
			try
			{
				_clientSocket = new TcpClient();
				_clientSocket.Connect(_host, _port);

				Attach();

				if (_clientSocket.Connected)
				{
					Console.WriteLine("You are now connected to {0} on port {1}.", _host, _port);
				}
				else
				{
					Console.WriteLine("Could not connect to {0}.", _host);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: {0}", e.Message);
			}
		}

		public void Connect(IrcUser ircUser)
		{
			try
			{
				_clientSocket = new TcpClient();
				_clientSocket.Connect(_host, _port);

				Attach();

				if (_clientSocket.Connected)
				{
					Console.WriteLine("You are now connected to {0} on port {1}.", _host, _port);
				}
				else
				{
					Console.WriteLine("Could not connect to {0}.", _host);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception caught: {0}", e.Message);
			}
		}

		public void Close()
		{
			if (_clientSocket != null)
			{
				Console.WriteLine("Closing connection to {0}.", _host);
				_clientSocket.Close();
			}

			if(_ircReader != null)
			{
				_ircReader.Close();
			}

			if (_ircWriter != null)
			{
				_ircWriter.Close();
			}
		}

		public void SendCommand(string command)
		{

		}

		public void WriteLine(string value)
		{
			_ircWriter.WriteLine(value);
		}

		public string ReadLine()
		{
			string retVal = "";
			
			retVal = _ircReader.ReadLine();

			return retVal;
		}

		public void UserCommand(string nick, string isVisible, string realName)
		{
			_ircWriter.WriteLine(String.Format("USER {0} {1} * :{2}", nick, isVisible, realName));
			_ircWriter.Flush();
		}

		public void NickCommand(string nick)
		{
			_ircWriter.WriteLine(String.Format("NICK {0}", nick));
			_ircWriter.Flush();
		}

		public void JoinCommand(string channel)
		{
			_ircWriter.WriteLine(String.Format("JOIN {0}", channel));
			_ircWriter.Flush();
		}

		private void IrcTopic(string[] ircCommand)
		{
			string channel = ircCommand[3];
			string topic = "";
			for (int intI = 4; intI < ircCommand.Length; intI++)
			{
				topic += ircCommand[intI] + " ";
			}
			if (eventTopicSet != null) { this.eventTopicSet(channel, topic.Remove(0, 1).Trim()); }
		}

		private void IrcTopicOwner(string[] ircCommand)
		{
			string channel = ircCommand[3];
			string user = ircCommand[4].Split('!')[0];
			string topicDate = ircCommand[5];
			if (eventTopicOwner != null) { this.eventTopicOwner(channel, user, topicDate); }
		}

		private void IrcNamesList(string[] ircCommand)
		{
			string userNames = "";
			for (int intI = 5; intI < ircCommand.Length; intI++)
			{
				userNames += ircCommand[intI] + " ";
			}
			if (eventNamesList != null) { this.eventNamesList(userNames.Remove(0, 1).Trim()); }
		}

		private void IrcServerMessage(string[] ircCommand)
		{
			string serverMessage = "";
			for (int intI = 1; intI < ircCommand.Length; intI++)
			{
				serverMessage += ircCommand[intI] + " ";
			}
			if (eventServerMessage != null) { this.eventServerMessage(serverMessage.Trim()); }
		}

		private void IrcPing(string[] ircCommand)
		{
			string pingHash = "";
			for (int intI = 1; intI < ircCommand.Length; intI++)
			{
				pingHash += ircCommand[intI] + " ";
			}
			_ircWriter.WriteLine("PONG " + pingHash);
		}

		private void IrcJoin(string[] ircCommand)
		{
			string channel = ircCommand[2];
			string user = ircCommand[0].Split('!')[0];
			if (eventJoin != null) { this.eventJoin(channel.Remove(0, 1), user); }
		}

		private void IrcPart(string[] ircCommand)
		{
			string channel = ircCommand[2];
			string user = ircCommand[0].Split('!')[0];
			if (eventPart != null) { this.eventPart(channel, user); }
		}

		private void IrcMode(string[] ircCommand)
		{
			string channel = ircCommand[2];
			string user = ircCommand[0].Split('!')[0];
			string mode = "";
			for (int intI = 3; intI < ircCommand.Length; intI++)
			{
				mode += ircCommand[intI] + " ";
			}
			if (mode.Substring(0, 1) == ":")
			{
				mode = mode.Remove(0, 1);
			}
			if (eventMode != null) { this.eventMode(channel, user, mode.Trim()); }
		}

		private void IrcNickChange(string[] ircCommand)
		{
			string oldNick = ircCommand[0].Split('!')[0];
			string newNick = ircCommand[2].Remove(0, 1);
			if (eventNickChange != null) { this.eventNickChange(oldNick, newNick); }
		}

		private void IrcKick(string[] ircCommand)
		{
			string kickerUser = ircCommand[0].Split('!')[0];
			string kickedUser = ircCommand[3];
			string channel = ircCommand[2];
			string message = "";
			for (int intI = 4; intI < ircCommand.Length; intI++)
			{
				message += ircCommand[intI] + " ";
			}
			if (eventKick != null) { this.eventKick(channel, kickerUser, kickedUser, message.Remove(0, 1).Trim()); }
		}

		private void IrcQuit(string[] ircCommand)
		{
			string quit = ircCommand[0].Split('!')[0];
			string message = "";
			for (int intI = 2; intI < ircCommand.Length; intI++)
			{
				message += ircCommand[intI] + " ";
			}
			if (eventQuit != null) { this.eventQuit(quit, message.Remove(0, 1).Trim()); }
		}

		private void Attach()
		{
			_ircReader = new StreamReader(Stream);
			_ircWriter = new StreamWriter(Stream);

			UserCommand(_user.Nick, _user.IsVisible, _user.RealName);
			NickCommand(_user.Nick);

			if (!String.IsNullOrEmpty(_channel))
			{
				JoinCommand(_channel);
			}
		}

		public void Run()
		{
			while (true)
			{
				string ircCommand;

				while ((ircCommand = ReadLine()) != null)
				{
					if (eventReceiving != null)
					{
						eventReceiving(ircCommand);
					}

					string[] commandParts = ircCommand.Split(' ');

					if (commandParts[0].Substring(0, 1).Equals(":"))
					{
						commandParts[0] = commandParts[0].Remove(0, 1);
					}

					Regex pattern = new Regex("[\\dA-Za-z]*(.){1}(freenode){1}(.){1}(net){1}");
					if (pattern.IsMatch(commandParts[0]))
					{
						switch (commandParts[1])
						{
							case "332": // Topic
								IrcTopic(commandParts);
								break;
							case "333": // Topic owner
								IrcTopicOwner(commandParts);
								break;
							case "353": // Names list
								IrcNamesList(commandParts);
								break;
							case "366":	// End of names list
								IrcEndNamesList(commandParts);
								break;
							case "372": // Irc MOTD
								IrcMOTD(ircCommand);
								break;
							case "376": // Irc End MOTD
								IrcEndMOTD(commandParts);
								break;
							default: // Irc Server Message
								//Console.WriteLine("{0}", ircCommand);
								break;
						}
					}
					else if (commandParts[0].Equals("PING"))
					{
						// Server PING, sen PONG back
						IrcPing(commandParts);
					}
					else
					{
						string commandAction = commandParts[1];

						switch (commandAction)
						{
							case "JOIN":
								IrcJoin(commandParts);
								break;
							case "PART":
								IrcPart(commandParts);
								break;
							case "MODE":
								IrcMode(commandParts);
								break;
							case "NICK":
								IrcNickChange(commandParts);
								break;
							case "KICK":
								IrcKick(commandParts);
								break;
							case "QUIT":
								IrcQuit(commandParts);
								break;
						}
					}
				}

				Close();
			}
		}

		private void IrcMOTD(string ircCommand)
		{
			string message = "";

			if (_motdLineCount == 0)
			{
				message = String.Format("\n\r{0}", (ircCommand.Split('-')[1]));
			}
			else
			{
				message = String.Format("{0}", (ircCommand.Split('-')[1]));
			}

			_motdLineCount++;
			_motd += message;
		}

		private void IrcEndMOTD(string[] commandParts)
		{
			// TODO: End of MOTD.
			string command = String.Format("\n\r{0}\n\r\n\r", _motd);
			Console.WriteLine(command);
			eventReceiving(command);
		}

		private void IrcEndNamesList(string[] commandParts)
		{
			// TODO: End of Names List.
		}
	}
}

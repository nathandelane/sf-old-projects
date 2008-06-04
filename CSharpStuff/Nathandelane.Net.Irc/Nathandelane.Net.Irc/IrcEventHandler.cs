using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Net.Irc
{
	public class IrcEventHandler
	{
		private IrcConnection _connection;
		private IrcLogger _logger;

		public IrcEventHandler(IrcConnection connection, IrcLogger logger)
		{
			_connection = connection;
			_logger = logger;

			_connection.eventReceiving += new IrcConnection.CommandReceived(IrcCommandReceived);
			_connection.eventTopicSet += new IrcConnection.TopicSet(IrcTopicSet);
			_connection.eventTopicOwner += new IrcConnection.TopicOwner(IrcTopicOwner);
			_connection.eventNamesList += new IrcConnection.NamesList(IrcNamesList);
			_connection.eventServerMessage += new IrcConnection.ServerMessage(IrcServerMessage);
			_connection.eventJoin += new IrcConnection.Join(IrcJoin);
			_connection.eventPart += new IrcConnection.Part(IrcPart);
			_connection.eventMode += new IrcConnection.Mode(IrcMode);
			_connection.eventNickChange += new IrcConnection.NickChange(IrcNickChange);
			_connection.eventKick += new IrcConnection.Kick(IrcKick);
			_connection.eventQuit += new IrcConnection.Quit(IrcQuit);
		}

		public void IrcCommandReceived(string ircCommand)
		{
			_logger.WriteLine(ircCommand);
		}

		public void IrcTopicSet(string channel, string topic)
		{
			string command = String.Format("Topic of {0} is: {1}", channel, topic);
			Console.WriteLine(command);
			_logger.WriteLine(command);
		}

		public void IrcTopicOwner(string channel, string user, string topicDate)
		{
			string command = String.Format("Topic of {0} set by {1} on {2} (unixtime)", channel, user, topicDate);
			Console.WriteLine(command);
			_logger.WriteLine(command);
		}

		public void IrcNamesList(string userNames)
		{
			string command = String.Format("Names List: {0}", userNames);
			Console.WriteLine(command);
			_logger.WriteLine(command);
		}

		public void IrcServerMessage(string serverMessage)
		{
			string command = String.Format("Server Message: {0}", serverMessage);
			Console.WriteLine(command);
			_logger.WriteLine(command);
		}

		public void IrcJoin(string channel, string user)
		{
			string command = String.Format("{0} joins {1}", user, channel);
			Console.WriteLine();
			_logger.WriteLine(command);
			command = String.Format("NOTICE {0} :Hello {0}, welcome to {1}!", user, channel);
			_connection.WriteLine(command);
			_logger.WriteLine(command);
		}

		public void IrcPart(string channel, string user)
		{
			string command = String.Format("{0} parts {1}", user, channel);
			Console.WriteLine(command);
			_logger.WriteLine(command);
		}

		public void IrcMode(string channel, string user, string mode)
		{
			if (user != channel)
			{
				string command = String.Format("{0} sets {1} in {2}", user, mode, channel);
				Console.WriteLine(command);
				_logger.WriteLine(command);
			}
		}

		public void IrcNickChange(string oldNick, string newNick)
		{
			string command = String.Format("{0} changes nick to {1}", oldNick, newNick);
			Console.WriteLine(command);
			_logger.WriteLine(command);
		}

		public void IrcKick(string channel, string userKicker, string userKicked, string message)
		{
			string command = String.Format("{0} kicks {1} out {2} ({3})", userKicker, userKicked, channel, message);
			Console.WriteLine(command);
			_logger.WriteLine(command);
		}

		public void IrcQuit(string user, string quitMessage)
		{
			string command = String.Format("{0} has quit IRC ({1})", user, quitMessage);
			Console.WriteLine(command);
			_logger.WriteLine(command);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace Nathandelane.Net.Irc
{
	public class Program
	{
		private IrcConnection _ircConnection;
		private IrcEventHandler _eventHandler;
		private IrcSettings _settings;
		private IrcUser _user;
		private IrcLogger _logger;

		private Program()
		{
			_settings = new IrcSettings();
			_user = new IrcUser(_settings["Nick"], _settings["RealName"], true);
			_logger = new IrcLogger(_settings["LogFilePath"], _settings["LogFileName"]);
			_ircConnection = new IrcConnection(_settings["Server"], Convert.ToInt32(_settings["Port"]), _user);
			_eventHandler = new IrcEventHandler(_ircConnection, _logger);

			_ircConnection.Connect();

			if (_ircConnection.Connected)
			{
				_ircConnection.Run();
				_logger.Close();
			}
		}

		static void Main()
		{
			new Program();
			Console.ReadLine();
		}
	}
}
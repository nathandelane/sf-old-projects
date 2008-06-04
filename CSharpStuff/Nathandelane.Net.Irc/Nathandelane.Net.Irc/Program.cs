using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace Nathandelane.Net.Irc
{
	public class Program
	{
		private IrcSettings _settings;
		private IrcUser _user;
		private IrcLogger _logger;

		private Program()
		{
			_settings = new IrcSettings();
			_user = new IrcUser(_settings["Nick"], _settings["RealName"], true);
			_logger = new IrcLogger();
			
			IrcConnection ircConnection = new IrcConnection(_user);
			ircConnection.Channel = "#null";
			string contents = "";
			string input = "";

			ircConnection.Connect();

			if (ircConnection.Connected)
			{
				// Get all of the response info
				while (!String.IsNullOrEmpty(contents = ircConnection.Read()))
				{
					Console.WriteLine("{0}", contents);
					_logger.WriteLine(contents);
					Thread.Sleep(250);
				}

				Console.WriteLine("Status: Connected = {0}.", Convert.ToString(ircConnection.Connected));

				while (!String.IsNullOrEmpty((input = Console.ReadLine())))
				{
					ircConnection.Write(input);

					while (!String.IsNullOrEmpty(contents = ircConnection.Read()))
					{
						Console.WriteLine("{0}", contents);
						Thread.Sleep(250);
					}
				}

				ircConnection.Close();
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
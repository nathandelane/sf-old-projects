using System;
using System.Collections.Generic;
using System.Text;
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
		private IrcUser _user;
		private TcpClient _clientSocket;
		private StreamWriter _ircWriter;
		private StreamReader _ircReader;
		private IPEndPoint _ipEndPoint;

		public IrcConnection(IrcUser user)
		{
			_user = user;
			_port = 6667;
			_host = "irc.freenode.net";
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

				ShakeHands();

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

				ShakeHands();

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
		}

		public void Write(string value)
		{
			_ircWriter.WriteLine(value);
		}

		public string Read()
		{
			string retVal = "";
			
			retVal = _ircReader.ReadLine();

			return retVal;
		}

		private void ShakeHands()
		{
			_ircReader = new StreamReader(Stream);
			_ircWriter = new StreamWriter(Stream);

			_ircWriter.WriteLine(String.Format("USER {0} {1} * :{2}", _user.Nick, _user.IsVisible, _user.RealName));
			_ircWriter.Flush();
			_ircWriter.WriteLine(String.Format("NICK {0}", _user.Nick));
			_ircWriter.Flush();
			_ircWriter.WriteLine(String.Format("JOIN {0}", _channel));
			_ircWriter.Flush();
		}
	}
}

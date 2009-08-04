using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Configuration;

namespace Nathandelane.Net.Catcher
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Int32 portNumber = Int32.Parse(ConfigurationManager.AppSettings["ListeningPort"]);
				IPAddress localAddress = IPAddress.Parse(ConfigurationManager.AppSettings["ListeningHost"]);

				TcpListener server = new TcpListener(localAddress, portNumber);
				server.Start();

				Byte[] bytes = new Byte[256];
				String data = null;

				while (true)
				{
					Console.WriteLine("Waiting for a connection on port {0}...", portNumber);

					TcpClient client = server.AcceptTcpClient();
					Console.WriteLine("Connected.");

					data = null;

					using (NetworkStream stream = client.GetStream())
					{
						int i;

						while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
						{
							data = ASCIIEncoding.ASCII.GetString(bytes, 0, i);
							Console.WriteLine("Received:\n\n{0}", data);
							/*
							data.ToUpper();

							byte[] message = ASCIIEncoding.ASCII.GetBytes(data);

							stream.Write(message, 0, message.Length);
							Console.WriteLine("Sent:\n\n{0}", data);
							 */
						}
					}
				}
			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: {0}", e);
			}

			Console.ReadLine();
		}
	}
}

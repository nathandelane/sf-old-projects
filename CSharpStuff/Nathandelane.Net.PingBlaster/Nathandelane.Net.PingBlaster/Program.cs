using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Nathandelane.Net.PingBlaster
{
	class Program
	{
		static void Main(string[] args)
		{
			byte first = 0, second = 0, third = 0, fourth = 0;
			Socket icmpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);

			while (first < 1)
			{
				IPAddress address = new IPAddress(new byte[] { first, second, third, fourth });

				try
				{
					IPHostEntry ipHostEntry = Dns.GetHostEntry(address);
					Console.WriteLine("{0} is valid as {1}.", address.ToString(), ipHostEntry.HostName);
				}
				catch (Exception e)
				{
					Console.WriteLine("{0} is NOT valid.", address.ToString());
				}

				fourth++;

				if (fourth == 255)
				{
					fourth = 0;
					third++;

					if (third == 255)
					{
						third = 0;
						second++;

						if (second == 255)
						{
							first++;
						}
					}
				}
			}

			Console.ReadLine();
		}
	}
}

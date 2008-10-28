using System;
using System.Collections.Generic;
using System.Text;
using LumiSoft.Net.IMAP.Client;

namespace Nathandelane.Net.Imap
{
	class Program
	{
		static void Main(string[] args)
		{
			IMAP_Client client = new IMAP_Client();
			client.Connect("vhx-mail-01", 143);
			Console.WriteLine("{0}", client.MessagesCount);
			client.Disconnect();
			Console.ReadKey();
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Obsidian
{
	/// <summary>
	/// Summary description for mcNetwork.
	/// </summary>
	public class mcNetwork
	{
		public string NetworkName;
		public string Nickname;
		public string Username;
		public string Realname;

		/* these all really need to be marked private. */
		public List<string> Buddies = new List<string>();
		public List<string> Perform = new List<string>();

		//these are stored like serverdns:port
		public List<string> Servers = new List<string>();

		public bool ConnectOnStartup = false;
		

		public mcNetwork()
		{
		}

		/*
		 * saves a network object to disk.
		 * really i could have implemented this as object serialisation,
		 * but i can learn that later and do it then.
		 */
		public static int SaveNetwork(mcNetwork aNetwork)
		{
			System.IO.StreamWriter fd;
			// string line; // Commented so C# will stfu.

			try
			{
				fd = new System.IO.StreamWriter("networks\\" + aNetwork.NetworkName + "\\network.dat");
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("An exception occured while trying to write network file " + aNetwork.NetworkName + ": " + ex.ToString(), "Error!");
				return 0;
			}
			
			try
			{
				fd.WriteLine("N {0}", aNetwork.Nickname);
				fd.WriteLine("U {0}", aNetwork.Username);
				fd.WriteLine("R {0}", aNetwork.Realname);
				if (aNetwork.ConnectOnStartup) fd.WriteLine("s");
				foreach (string server in aNetwork.Servers)
				{
					fd.WriteLine("S {0}", server);
				}
				foreach (string line in aNetwork.Perform)
				{
					if (line[0] == '#')
					{
						// Just as C programmers never printf(string), we never
						// write the string directly either.
						fd.WriteLine("{0}", line);
					}
					else
					{
						fd.WriteLine("P {0}", line);
					}
				}
				foreach (string buddy in aNetwork.Buddies)
				{
					fd.Write("B {0}", buddy);
				}
			}
			finally
			{
				fd.Close();
			}

			return 0;
		}

		/* Parses a network file, and returns a mcNetwork network object. */
		public static mcNetwork GetNetwork(string NetworkName)
		{
			mcNetwork NewNetwork = new mcNetwork();
			System.IO.StreamReader fd;
			string line;
			string temp;
			string[] parts;

			try
			{
				fd = new System.IO.StreamReader("networks\\" + NetworkName + "\\network.dat");
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show("An exception occured while trying to read network " + NetworkName + ": " + ex.ToString(), "Error!");
				return null;
			}
			
			NewNetwork.NetworkName = NetworkName;
			
			try
			{
				while (true)
				{
					/* no, this does actually end ;) */
					line = fd.ReadLine();
					if (line == null)
						break;
	
					parts = line.Split(' ');
	
					switch (line[0])
					{
						case 'N':
							/* nickname token */
							if (parts.Length < 2)
							{
								System.Windows.Forms.MessageBox.Show("Malformed 'N' token in network file " + NetworkName + ", aborting read effort.", "Error!");
								return null;
							}
							NewNetwork.Nickname = parts[1];
							break;
						case 'U':
							/* username token */
							if (parts.Length < 2)
							{
								System.Windows.Forms.MessageBox.Show("Malformed 'U' token in network file " + NetworkName + ", aborting read effort.", "Error!");
								return null;
							}
							NewNetwork.Username = parts[1];
							break;
						case 'R':
							/* realname token */
							if (parts.Length < 2)
							{
								System.Windows.Forms.MessageBox.Show("Malformed 'R' token in network file " + NetworkName + ", aborting read effort.", "Error!");
								return null;
							}
							NewNetwork.Realname = String.Join(" ", parts, 1, parts.Length - 1);
							break;
						case 's':
							/* connect on startup */
							/* optional token- if it's here, we're supposed to connect on startup. */
							NewNetwork.ConnectOnStartup = true;
							break;
						case 'S':
							/* a server/port combination */
							NewNetwork.Servers.Add(parts[1]);
							break;
						case '#':
							/* perform comment */
							NewNetwork.Perform.Add(line);
							break;
						case 'P':
							/* perform line */
							temp = String.Join(" ", parts, 1, parts.Length - 1);
							NewNetwork.Perform.Add(temp);
							break;
						case 'B':
							/* buddy line */
							if (parts.Length < 2)
							{
								System.Windows.Forms.MessageBox.Show("Malformed 'U' token in network file " + NetworkName + ", aborting read effort.", "Error!");
								return null;
							}
							NewNetwork.Buddies.Add(parts[1]);
							break;
					}
				}
				return NewNetwork;
			}
			finally
			{
				fd.Close();
			}
		}
	}
}

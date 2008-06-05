using System;
using System.Text;
using System.Reflection;

/*
 * XXX - Numerics to Add (when w00t gets time. :P):
 *  :stitch.chatspike.net 378  w00t w00t is connecting from *@CPE-61-9-200-140.nsw.bigpond.net.au 61.9.200.140
 *  :crew.irc.barafranca.com 338  Calis l33t`out Midnight@irc.barafranca.com 67.19.22.226 Actual user@host, Actual IP
 *  :stitch.chatspike.net 482  Guest465136598 #chatspike You must be at least a half-operator to change modes on this channel
 *  :stitch.chatspike.net 401  Guest465136598 moo No such nick/channel
 *  :irc.barafranca.com 381  Calis You are now an IRC Operator
 *  :irc.barafranca.com 330  Calis Kithy_Febril kythana is authed as
 */
namespace Obsidian
{
	/// <summary>
	/// New incoming data processer.
	/// </summary>
	sealed public class mcInbound
	{

		private static string tempBuffer = "";

		public static void Parse(string data, mcPage page)
		{
			/* used to tokenise a single message */
			System.Text.StringBuilder part = new System.Text.StringBuilder();
			//char prevchar;
			string[] parts; /* todo: can we ever recieve > 100 params? */
			int i;
			//int pcount;

			/*
			 *  what do you get when you look at a tired australian
			 * programmer?.. one who makes mistakes.. oops.
			 */
			string prefix, command;

			/* filled by splitting \n */
			string[] messages;

			/* make \r into \n and split into commands */
			data = data.Replace('\r', '\n');
			messages = data.Split('\n');

			/* If we have any tempBuffer from the last parse, and more than one element,
			 * then we have the end of the incomplete buffer from the last parse. */
			if (tempBuffer.Length > 0 && messages.Length > 1)
			{
				messages[0] = tempBuffer + messages[0];
				tempBuffer = "";
			}

			/* The last element should be empty. If it isn't we have an incomplete buffer. */
			if (messages[messages.Length - 1] != "")
			{
				tempBuffer = messages[messages.Length - 1];
				messages[messages.Length - 1] = "";
			}

			foreach (string message in messages)
			{
				/* 
				 * we might get CMD param\r\n which gets translated to
				 * CMD param\n\n above, giving us empty entries.
				 * If we don't ignore them here, we get problems :p
				 */
				if (message == "")
					continue;

				//page.MessageInfo("Processing " + message);

				string[] temp;
				string workstr = message;

				// Scrub off leading spaces. Not really RFC valid but...
				workstr = message.TrimStart(' ');

				// My parsing code :P !
				if (workstr[0] == ':')
				{
					// First character is : - we have a prefix. The command comes second.
					temp = message.Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
					prefix = temp[0].Substring(1);
					command = temp[1];
					workstr = temp[2];
				}
				else
				{
					// First character is not a : - there is no prefix (assume server is the sender). The command comes first.
					temp = message.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
					prefix = page.Server.ServerName;
					command = temp[0];
					workstr = temp[1];
				}

				/*				// Get rid of multispaces.
				while (workstr.IndexOf("  ") >= 0)
					workstr = workstr.Replace("  ", " ");
				 */
				
				// Now the parameters. Check if we have a starting : ... AGAIN.
				if (workstr[0] == ':')
				{
					// We do. It's the one and only param no matter how many spaces it has.
					parts = new string[1] { workstr.Substring(1) };
				}
				else if ((i = workstr.IndexOf(" :")) >= 0)
				{
					// We don't, but we have a space-colon elsewhere.
					// i is the space before the :
					string[] tmp = workstr.Substring(0, i).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					parts = new string[tmp.Length + 1];
					tmp.CopyTo(parts, 0);
					parts[parts.Length - 1] = workstr.Substring(i + 2);
				}
				else
				{
					// No space-colon either.
					parts = workstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				}

				
				Type t = typeof(mcInbound);
				MethodInfo m;
				
				/* now lookup and execute the given command */
				m = t.GetMethod("Cmd" + command, BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { typeof(string), typeof(string), typeof(string[]), typeof(mcPage) }, null);
				if (m == null)
					DefaultCommand(prefix, command, parts, page);
				else
				{
					try
					{
						m.Invoke(null, new object[] { prefix, command, parts, page });
					}
					catch (Exception ex)
					{
						//System.Diagnostics.
						System.Windows.Forms.MessageBox.Show("Method Cmd" + command + " threw an exception while being invoked: " + ex.ToString() + " - stack trace is: \n\n" + ex.StackTrace);
					}
				}
			}
		}

		// Format for command procedures:
		// private static void Cmd<cmdname>(string sender, string command, string[] parameters, mcPage page) { code... }

		#region "Numerics"
		#region "000 Series"
		private static void Cmd001(string prefix, string command, string[] parameters, mcPage page)
		{
			string[] userhost;
			//<- :devel.rburchell.org 001 w-mirc :Welcome to the Symmetic IRC Network w-mirc!w00t__@127.0.0.1
			page.Server.ServerName = prefix;	/* this is a good time to find the real name */
			page.Server.ServerPage.MyNode.Text = page.Server.ServerName;
			page.Server.ServerPage.Text = page.Server.ServerName;	/* and make sure the node matches 8) */
			//split param 3 to get network name..
			userhost = parameters[1].Split(' ');

			if (System.IO.File.Exists(".\\networks\\" + userhost[3] + "\\network.dat"))
			{
				page.MessageInfo("Network " + userhost[3] + " is a known network.");
				/* We have a networks file. */
				mcNetwork NewNetwork = mcNetwork.GetNetwork(userhost[3]);
				page.Server.IRCSend("NICK " + NewNetwork.Nickname);
				/* assume that user/real is already valid. */
				//todo: add this to the list of servers?
				//page.Server.ServerSocket.RemoteHost
				foreach (string str in NewNetwork.Perform)
				{
					if (str[0] != '#')
					{
						/* the "P" is stripped automagically */
						page.MessageInfo("Doing PERFORM " + str);
						page.Server.IRCSend(str);
					}
				}
			}
			else
			{
				page.MessageInfo("Network " + userhost[3] + " is not a known network.");
			}
		}

		private static void Cmd002(string prefix, string command, string[] parameters, mcPage page)
		{
			//<- :devel.rburchell.org 002 w-mirc :Your host is devel.rburchell.org, running version Unreal3.2.3
			page.MessageInfo(parameters[1]);
		}

		private static void Cmd003(string prefix, string command, string[] parameters, mcPage page)
		{
			//<- :devel.rburchell.org 003 w-mirc :This server was created Sat Apr 16 23:22:52 2005
			page.MessageInfo(parameters[1]);
		}

		private static void Cmd004(string prefix, string command, string[] parameters, mcPage page)
		{
			//<- :devel.rburchell.org 004 w-mirc devel.rburchell.org Unreal3.2.3 iowghraAsORTVSxNCWqBzvdHtGpE lvhopsmntikrRcaqOALQbSeIKVfMCuzNTGj
			page.MessageInfo(parameters[1] + " " + parameters[2] + " " + parameters[3] + " " + parameters[4]);
		}

		private static void Cmd005(string prefix, string command, string[] parameters, mcPage page)
		{
			int i = 0;
			string todisplay = "Options: ";
			
			/* todo: do we need to support RPL_BOUNCE too? */
			//<- :devel.rburchell.org 005 w00t SAFELIST HCN MAXCHANNELS=10 CHANLIMIT=#:10 MAXLIST=b:60,e:60,I:60 NICKLEN=30 CHANNELLEN=32 TOPICLEN=307 KICKLEN=307 AWAYLEN=307 MAXTARGETS=20 WALLCHOPS WATCH=128 :are supported by this Server
			//<- :devel.rburchell.org 005 w00t SILENCE=15 MODES=12 CHANTYPES=# PREFIX=(qaohv)~&@%+ CHANMODES=beI,kfL,lj,psmntirRcOAQKVGCuzNSMTG NETWORK=Symmetic CASEMAPPING=ascii EXTBAN=~,cqnr ELIST=MNUCT STATUSMSG=~&@%+ EXCEPTS INVEX CMDS=KNOCK,MAP,DCCALLOW,USERIP :are supported by this Server
			
			for (i = 1; i < parameters.Length - 1; i++)
			{
				string str = parameters[i];
				string[] tokens;

				if (str == null)
					break;
				
				todisplay = todisplay + " " + parameters[i];
				tokens = str.Split("=".ToCharArray());
				
				switch (tokens[0])
				{
					case "CHANMODES":
						page.Server.ISupport.CHANMODES = tokens[1].Split(',');
						break;
					case "PREFIX":
						/* tokens[1] is something like (ohv)@%+ */
						tokens[1] = tokens[1].Substring(1, tokens[1].Length - 1);
						/* now ohv)@%+ - find the ) in the middle */
						int a = tokens[1].IndexOf(")");
						/* split string into prefixchars and equivilant modes */
						string modechars = tokens[1].Substring(0, a);
						string prefixchars = tokens[1].Substring(a + 1);
						if (modechars.Length != prefixchars.Length)
						{
							/* o_O I want to hear if this ever happens :P */
							page.MessageInfo("Remote Server sent malformed 005 PREFIX token, ignoring. Please report this.");
							continue;
						}
						page.Server.ISupport.PREFIX_Characters = prefixchars;
						page.Server.ISupport.PREFIX_Modes = modechars;
						break;
					default:
						if (page.Server.ISupport.Other.ContainsKey(tokens[0]))
						{
							if (tokens.Length > 1)
							{
								page.Server.ISupport.Other[tokens[0]] = tokens[1];
							}
						}
						else
						{
							if (tokens.Length > 1)
							{
								// Has a value.
								page.Server.ISupport.Other.Add(tokens[0], tokens[1]);
							}
							else
							{
								// No value.
								page.Server.ISupport.Other.Add(tokens[0], null);
							}
						}
						break;
				}
			}
			todisplay = todisplay + " " + parameters[i++];
			page.MessageInfo(todisplay);
		}
		#endregion
		#region "200 Series"
		// RPL_LUSERCLIENT
		private static void Cmd251(string sender, string command, string[] parameters, mcPage page)
		{
			// ":There are <integer> users and <integer> services on <integer> servers"
			page.MessageInfo(parameters[1]);
		}

		// RPL_LUSEROP
		private static void Cmd252(string sender, string command, string[] parameters, mcPage page)
		{
			// "<integer> :operator(s) online"
			page.MessageInfo(parameters[1] + " IRC operator(s) online");
		}

		private static void Cmd253(string sender, string command, string[] parameters, mcPage page)
		{
			// "<integer> :unknown connection(s)"
			page.MessageInfo(parameters[1] + " unknown connection(s)");
		}

		private static void Cmd254(string sender, string command, string[] parameters, mcPage page)
		{
			//:devel.rburchell.org 254 w00t_ 1 :channels formed
			page.MessageInfo(parameters[1] + " channel(s) formed");
		}

		private static void Cmd255(string sender, string command, string[] parameters, mcPage page)
		{
			//:devel.rburchell.org 255 w00t_ :I have 3 clients and 0 servers
			page.MessageInfo(parameters[1]);
		}

		private static void Cmd265(string sender, string command, string[] parameters, mcPage page)
		{
			//:devel.rburchell.org 265 w00t_ :Current Local Users: 3  Max: 500
			page.MessageInfo(parameters[1]);
		}

		private static void Cmd266(string sender, string command, string[] parameters, mcPage page)
		{
			//:devel.rburchell.org 266 w00t_ :Current Global Users: 3  Max: 3
			page.MessageInfo(parameters[1]);
		}
		#endregion
		#region "300 Series"
		#region "WHOIS NUMERICS"
		private static void Cmd301(string prefix, string command, string[] parameters, mcPage page)
		{
			string todisplay = null; /* join parts of parameters together to display */
			for (int i = 2; i < parameters.Length; i++)
				todisplay = todisplay + " " + parameters[i];

			page.Server.CurrentPage.MessageInfo("Away:" + todisplay);
		}
		
		private static void Cmd307(string prefix, string command, string[] parameters, mcPage page)
		{
			page.Server.CurrentPage.MessageInfo("Status: " + parameters[1] + " is a Registered Nickname");
		}
		
		private static void Cmd310(string prefix, string command, string[] parameters, mcPage page)
		{
			string todisplay = null; /* join parts of parameters together to display */
			for (int i = 2; i < parameters.Length; i++)
				todisplay = todisplay + " " + parameters[i];

			page.Server.CurrentPage.MessageInfo("Status:" + todisplay);
		}
		
		private static void Cmd311(string prefix, string command, string[] parameters, mcPage page)
		{
			string todisplay = null; /* join parts of parameters together to display */
			page.Server.CurrentPage.MessageInfo("-------------[WHOIS: " + parameters[1] + "]------------");
			page.Server.CurrentPage.MessageInfo("NUH: " + parameters[1] + "!" + parameters[2] + "@" + parameters[3]);

			for (int i = 5; i < parameters.Length; i++)
				todisplay = todisplay + " " + parameters[i];

			page.Server.CurrentPage.MessageInfo("Real Name:" + todisplay);
		}
		
		private static void Cmd313(string prefix, string command, string[] parameters, mcPage page)
		{
			string todisplay = null; /* join parts of parameters together to display */
			for (int i = 2; i < parameters.Length; i++)
				todisplay = todisplay + " " + parameters[i];

			page.Server.CurrentPage.MessageInfo("Status:" + todisplay);
		}

		private static void Cmd312(string prefix, string command, string[] parameters, mcPage page)
		{
			string todisplay = null; /* join parts of parameters together to display */
			for (int i = 2; i < parameters.Length; i++)
				todisplay = todisplay + " " + parameters[i];

			page.Server.CurrentPage.MessageInfo("Server:" + todisplay);
		}
		
		//:stitch.chatspike.net 317  w00t Brik 2496 1145297959 seconds idle, signon time
		private static void Cmd317(string prefix, string command, string[] parameters, mcPage page)
		{
			int idle = Int32.Parse(parameters[2]);
			//string idleprefix = "second(s)";
			System.DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(double.Parse(parameters[3])).ToLocalTime();
			
			TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
			int timeonline  = (int)t.TotalSeconds - int.Parse(parameters[3]);
			//string timeonlineprefix = "second(s)";
			
			/*
			 * avoid negative online time if their local clock isn't quite right --
			 * minor cosmetic detail, but I think it looks good. --w00t
			 */
			if (idle > timeonline)
				timeonline = idle;
			
			/*
			 * XXX - this could be improved on by using 'x hours, y mins' or something.
			 * I'm unsure exactly how this would be done, I assume by using the modulus
			 * or something. aquanight? --w00t
			 */
			#if DeadCode
			if (idle > 60)
			{
				/* get minutes */
				idle = idle / 60;
				idleprefix = "minute(s)";
				
				if (idle > 60)
				{
					/* get hours */
					idle = idle / 60;
					idleprefix = "hours(s)";
					
					if (idle > 24)
					{
						/* days */
						idle = idle / 24;
						idleprefix = "day(s)";
					}
				}
			}

			
			if (timeonline > 60)
			{
				/* get minutes */
				timeonline = timeonline / 60;
				timeonlineprefix = "minute(s)";
				if (timeonline > 60)
				{
					/* get hours */
					timeonline = timeonline / 60;
					timeonlineprefix = "hours(s)";
					
					if (timeonline > 24)
					{
						/* days */
						timeonline = timeonline / 24;
						timeonlineprefix = "day(s)";
					}
				}
			}
			page.Server.CurrentPage.MessageInfo("Idle: " + idle.ToString() + " " + idleprefix + " online for " + timeonline.ToString() + " " + timeonlineprefix + ", signon: " + time.ToString("ddd, MMM d, yyyy HH:mm:ss"));
			#endif
			page.Server.CurrentPage.MessageInfo("Idle: " + Obsidian.FormatTime(idle) + " online for " + Obsidian.FormatTime(timeonline) + ", signon: " + time.ToString("ddd, MMM d, yyyy HH:mm:ss"));
		}

		private static void Cmd318(string prefix, string command, string[] parameters, mcPage page)
		{
			page.Server.CurrentPage.MessageInfo("-------------[END OF WHOIS]------------");
		}

		private static void Cmd319(string prefix, string command, string[] parameters, mcPage page)
		{
			string todisplay = null; /* join parts of parameters together to display */
			for (int i = 2; i < parameters.Length; i++)
				todisplay = todisplay + " " + parameters[i];

			page.Server.CurrentPage.MessageInfo("Channels:" + todisplay);
		}

		private static void Cmd320(string prefix, string command, string[] parameters, mcPage page)
		{
			string todisplay = null; /* join parts of parameters together to display */
			for (int i = 1; i < parameters.Length; i++)
				todisplay = todisplay + " " + parameters[i];

			page.Server.CurrentPage.MessageInfo("Description:" + todisplay);
		}
		#endregion

		private static void Cmd332(string prefix, string command, string[] parameters, mcPage page)
		{
			//<- :devel.rburchell.org 332 w00t #test :ggagagagaqg
			mcPage target;
			target = page.Server.FindPage(parameters[1]);
			if (target == null)
				return;
			target.MessageTopic(parameters[2]);
		}

		private static void Cmd333(string prefix, string command, string[] parameters, mcPage page)
		{
			//<- :devel.rburchell.org 333 w00t #test w-mirc 1125627086
			mcPage target;
			target = page.Server.FindPage(parameters[1]);
			if (target == null)
				return;
			target.MessageTopicTime(parameters[2], parameters[3]);
		}

		private static void Cmd353(string prefix, string command, string[] parameters, mcPage page)
		{
			/*
			 *  RPL_NAMREPLY
			 * <- :devel.rburchell.org 353 w-mirc = #test :@w-mirc
			 * <- :devel.rburchell.org 366 w-mirc #test :End of /NAMES list.
			 */
			string[] userlist;
			mcPage target;

			/* pull channel out of info */
			target = page.Server.FindPage(parameters[2]);

			if (target == null)
				return;

			/* populate user list */
			/* TODO: clear the list first? */
			userlist = parameters[3].Split(' ');
			target.lstUsers.BeginUpdate();
			foreach (string name in userlist)
			{
				StringBuilder thenick = new StringBuilder();
				StringBuilder theprefix = new StringBuilder();

				if (name.Length < 1)
					continue;

				foreach (char nickchar in name)
				{
					bool isprefix = false;

					foreach (char prefixchar in target.Server.ISupport.PREFIX_Characters)
					{
						if (prefixchar == nickchar)
						{
							/* it's a prefix */
							isprefix = true;
							break;
						}
					}

					if (isprefix == false)
						thenick.Append(nickchar);
					else
						theprefix.Append(nickchar);
				}

				target.AddUserToChannel(thenick.ToString(), null);
				foreach (char c in theprefix.ToString())
					target.AddPrefix(thenick.ToString(), c);
			}
			target.lstUsers.EndUpdate();
		}

		private static void Cmd366(string prefix, string command, string[] parameters, mcPage page)
		{
			// RPL_ENDOFNAMES
			// do nothing.
		}

		// RPL_MOTD
		private static void Cmd372(string prefix, string command, string[] parameters, mcPage page)
		{
			// ":- <text>"
			page.MessageInfo(parameters[1]);
		}

		// RPL_MOTDSTART
		private static void Cmd375(string prefix, string command, string[] parameters, mcPage page)
		{
			// ":- <Server> Message of the day - "
			page.MessageInfo(parameters[1]);
		}

		private static void Cmd376(string prefix, string command, string[] parameters, mcPage page)
		{
			// RPL_ENDOFMOTD
			page.MessageInfo(parameters[1]);
		}
		#endregion
		#region "400 Series"
		private static void Cmd422(string prefix, string command, string[] parameters, mcPage page)
		{
			/* no motd */
			page.MessageInfo(parameters[1]);
		}

		private static void Cmd433(string prefix, string command, string[] parameters, mcPage page)
		{
			//<- :devel.rburchell.org 433 * w-mirc :Nickname is already in use.
			page.Server.ServerPage.MessageInfo(parameters[1] + " is already in use. Retrying with " + parameters[1] + "_");
			page.Server.MyNickname = parameters[1] + "_";
			mcCommands.MainParser(page, "/nick " + parameters[1] + "_");
		}

		private static void Cmd421(string prefix, string command, string[] parameters, mcPage page)
		{
			//:devel.rburchell.org 421 w00t me :Unknown command
			page.Server.CurrentPage.MessageInfo(parameters[1] + ": " + parameters[2]);
		}
		#endregion
		#region "600 Series"
		private static void Cmd671(string prefix, string command, string[] parameters, mcPage page)
		{
			string todisplay = null; /* join parts of parameters together to display */
			for (int i = 2; i < parameters.Length; i++)
				todisplay = todisplay + " " + parameters[i];

			page.Server.CurrentPage.MessageInfo("Status:" + todisplay);
		}
		#endregion

		#endregion

		#region "Commands"
		private static void CmdERROR(string prefix, string command, string[] parameters, mcPage page)
		{
			// Command: ERROR   Parameters: <error message>
			page.Server.ServerPage.MessageInfo("ERROR " + parameters[0]);
			/* this is being a bit cheeky ;) */
			page.Server.Disconnect("ERROR " + parameters[0]);
		}

		private static void CmdMODE(string prefix, string command, string[] parameters, mcPage page)
		{
			//:w00t!u@h MODE #test +oi Brik
			//:w00t!u@h MODE w00t +h
			mcPage target;
			string[] userhost;
			
			target = page.Server.FindPage(parameters[0]);
			userhost = prefix.Split('!');
			if (target == null)
			{
				/* setting modes on us, handle later */
				page.MessageInfo(userhost[0] + " sets mode " + parameters[1] + " on you");
				page.Server.ParseModes(parameters[1]);
				return;
			}

			bool adding = true;
			int j = 2;
			bool prefixmode = false;
			int n = 0;
			bool requiresparam;
			string myparams = null;

			for (n = 1; n < parameters.Length; n++)
				myparams = myparams + " " + parameters[n];

			target.MessageMode(userhost[0], userhost[1], myparams.Substring(1));

			foreach (char modechar in parameters[1])
			{
				switch (modechar)
				{
					case '-':
						adding = false;
						break;
					case '+':
						adding = true;
						break;
					default:
						/* first, determine if it's a prefix mode.. treat them differently. */
						prefixmode = false;
						for (n = 0; n < target.Server.ISupport.PREFIX_Modes.Length; n++)
						{
							if (modechar == target.Server.ISupport.PREFIX_Modes[n])
							{
								/* We are dealing with a prefix. */
								prefixmode = true;
								break;
							}
						}

						if (prefixmode)
						{
							if (adding)
							{
								target.AddPrefix(parameters[j], target.Server.ISupport.PREFIX_Characters[n]);
								j++;
							}
							else
							{
								target.RemovePrefix(parameters[j], target.Server.ISupport.PREFIX_Characters[n]);
								j++;
							}
						}
						else
						{
							if (target.Server.ISupport.CHANMODES[0].IndexOf(modechar) >= 0 || target.Server.ISupport.CHANMODES[1].IndexOf(modechar) >= 0 || (target.Server.ISupport.CHANMODES[2].IndexOf(modechar) >= 0 && adding)) {
								requiresparam = true;
							}
							else
							{
								requiresparam = false;
							}

							if (adding)
							{
								if (requiresparam)
								{
									target.AddMode(modechar, parameters[j], requiresparam);
									j++;
								}
								else
								{
									target.AddMode(modechar, null, requiresparam);
								}
							}
							else
							{
								if (requiresparam)
								{
									target.RemoveMode(modechar, parameters[j]);
									j++;
								}
								else
								{
									target.RemoveMode(modechar, null);
								}
							}
						}
						break;
				}

			}
		}

		private static void CmdINVITE(string prefix, string command, string[] parameters, mcPage page)
		{
			// Do something with this...
		}

		private static void CmdJOIN(string prefix, string command, string[] parameters, mcPage page)
		{
			//:w00t!u@h JOIN :#test
			mcPage target;
			string[] userhost;
			
			target = page.Server.FindPage(parameters[0]);
			userhost = prefix.Split('!');
			
			if (target == null)
			{
				/* Joining a new channel. */
				target = page.Server.AddPage(parameters[0], mcServer.PageType.Channel);
				target.IsChannel = true;
			}
			
			target.MessageJoin(userhost[0], userhost[1]);
		}

		private static void CmdKICK(string prefix, string command, string[] parameters, mcPage page)
		{
			/* :aggressor!u@h KICK #somechan target :reason */
			string[] userhost;
			mcPage target;
			
			userhost = prefix.Split('!');
			target = page.Server.FindPage(parameters[0].ToLower());
			
			if (target == null)
				return; /* probably server lag. */

			target.MessageKick(parameters[1], userhost[0], parameters[1]);
			/* TODO: Clear the nicklist? */
		}

		private static void CmdNICK(string prefix, string command, string[] parameters, mcPage page)
		{
			// Command: NICK    Parameters: :<new>
			string[] userhost;
			userhost = prefix.Split('!');
			
			if (userhost[0] == page.Server.MyNickname)
			{
				page.Server.MyNickname = parameters[0];
			}
			
			page.Server.ChangeNick(userhost[0], parameters[0]);
		}

		private static void CmdNOTICE(string prefix, string command, string[] parameters, mcPage page)
		{
			// Command: NOTICE  Parameters: <msgtarget> :<text>
			string source;
			string[] userhost;
			
			mcPage target;
			if (prefix != null)
			{
				userhost = prefix.Split('!');
				source = userhost[0];
			}
			else
			{
				source = page.Server.ServerName;
			}

			target = page.Server.FindPage(parameters[0]);
			
			if (target != null)
				target.MessageNotice(source, parameters[1]);
			else
			{
				if (source.IndexOf('.') == -1)
					page.Server.CurrentPage.MessageNotice(source, parameters[1]);
				else
					page.Server.ServerPage.MessageNotice(source, parameters[1]);
			}
		}

		private static void CmdPART(string prefix, string command, string[] parameters, mcPage page)
		{
			//:n!u@h PART #chan :message
			string[] userhost;
			mcPage target;
			
			userhost = prefix.Split('!');
			target = page.Server.FindPage(parameters[0]);
			
			if (target == null)
				return;

			target.MessagePart(userhost[0], userhost[1], (parameters.Length >= 2 ? parameters[1] : ""));

			if (userhost[0] == page.Server.MyNickname)
			{
				page.Server.DeletePage(target);
				Obsidian.mainForm.tvcWindows.Nodes.Remove(target.MyNode);
			}
		}

		private static void CmdPING(string prefix, string command, string[] parameters, mcPage page)
		{
			//PING :something.goes.here
			page.Server.IRCSend("PONG " + parameters[0]);
		}

		private static void CmdPRIVMSG(string prefix, string command, string[] parameters, mcPage page)
		{
			//:n!u@h PRIVMSG target :message here!
			string[] userhost;
			mcPage target;
			
			userhost = prefix.Split('!');
			
			if (parameters[1][0] == '\u0001')
			{
				//todo: redo CTCP support (:
				CTCP(prefix, parameters, page);
				return;
			}
			
			target = page.Server.FindPage(parameters[0]);
			
			if (target != null)
			{
				target.MessageUser(userhost[0], parameters[1]);
			}
			else
			{
				target = page.Server.FindPage(userhost[0]);
				
				if (target == null)
				{
					page.Server.AddPage(userhost[0], mcServer.PageType.Message);
					target = page.Server.FindPage(userhost[0]);
				}
				
				target.MessageUser(userhost[0], parameters[1]);
			}
		}

		private static void CmdQUIT(string prefix, string command, string[] parameters, mcPage page)
		{
			//:prefix!u@h QUIT :message :o
			string[] userhost;
			
			userhost = prefix.Split('!');
			page.Server.QuitNick(userhost[0], parameters[0]);
		}

		private static void CmdTOPIC(string prefix, string command, string[] parameters, mcPage page)
		{
			//:n!u@h TOPIC #chan :newtopic zomg!
			string[] userhost;
			mcPage target;
			
			userhost = prefix.Split('!');
			target = page.Server.FindPage(parameters[0]);
			
			if (target == null)
				return;

			target.Topic = parameters[1];
			target.MessageInfo(userhost[0] + " has changed the topic to: " + target.Topic);
			if (target.Equals(page.Server.CurrentPage))
				page.Server.CurrentPage.Topic = target.Topic;
		}
		#endregion

		private static void DefaultCommand(string prefix, string command, string[] parameters, mcPage page)
		{
			/* unknown numeric/command */
			string todisplay;
			
			todisplay = "UNKNOWN: :" + prefix + " " + command + " ";
			
			foreach (string str in parameters)
			{
				todisplay = todisplay + " " + str;
			}
			
			page.MessageInfo(todisplay);
		}

		private static void CTCP(string prefix, string[] parameters, mcPage page)
		{
			//:n!u@h PRIVMSG #blah :VERSION
			mcPage target;
			string[] userhost;
			string[] ctcpparams;

			parameters[1] = parameters[1].Replace("", "");

			userhost = prefix.Split('!');
			ctcpparams = parameters[1].Split(' ');

			target = page.Server.FindPage(parameters[0]);
			if (target == null)
			{
				/* no channel page, try find a private one.. */
				target = page.Server.FindPage(userhost[0]);
			}

			switch (ctcpparams[0])
			{
				case "ACTION":
					/* if we didn't find a page previously, create one (presume private ;)) */
					if (target == null)
					{
						target = page.Server.AddPage(userhost[0], mcServer.PageType.Message);
					}
					
					int i = 0;
					string action = null;
					
					foreach (string str in ctcpparams)
					{
						if (i == 0) /* ugly :p */
						{
							i++;
							continue;
						}
						action = action + " " + str;
					}
					
					target.MessageAction(userhost[0], action);
					break;
				case "VERSION":
					if (target == null)
					{
						target = page.Server.ServerPage;
					}
					
					target.MessageInfo("[" + userhost[0] + " VERSION]");
					page.Server.IRCSend("NOTICE " + userhost[0] + " :VERSION  " + Obsidian.APP_NAME + " " + Obsidian.APP_VER + " - Got it yet?\r\n");
					break;
			}
		}
	}
}

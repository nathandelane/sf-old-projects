using System;
using System.Reflection;

namespace Obsidian
{
	/// <summary>
	/// mcCommands is the definition of internal commands and aliases.
	/// </summary>
	sealed public class mcCommands
	{
		public static void cmdServer(mcPage aPage, string Parameters)
		{
			string[] parts;
			if (Parameters == null)
			{
				return;
			}
			parts = Parameters.Split(null);
			if(parts.Length < 1 || parts.Length > 3) 
			{
				aPage.MessageInfo("Usage: /server <server.name> [port]");
				return;
			} 

			if(aPage.Server.Connected) 
				aPage.Server.Disconnect("Changing servers.");

			aPage.Server.ServerName = parts[1];
			aPage.Server.ServerPort = (parts.Length == 3) ? System.Int32.Parse(parts[2]) : 6667;
			aPage.Server.Connect();
		}

		public static void cmdReconnect(mcPage aPage, string Parameters)
		{
			aPage.Server.Disconnect("Reconnecting.");
			aPage.Server.Connect();
		}

		public static void cmdDisconnect(mcPage aPage, string Parameters)
		{
			if (Parameters == null)
			{
				/* todo: this needs to be configurable */
				aPage.Server.Disconnect("Departing.");
			}
			else
			{
				aPage.Server.Disconnect(Parameters);
			} 
		}
		
		public static void cmdNames(mcPage aPage, string Parameters)
		{
			if (Parameters == null)
				return;
			
			mcPage target = aPage.Server.FindPage(Parameters);
			
			if (target != null)
				target.RemoveAllUsersFromChannel();
		}

		public static void cmdNick(mcPage aPage, string Parameters)
		{
			if (Parameters == null)
			{
				aPage.MessageInfo("Usage: /nick <new_name>");
				return;
			}
			aPage.Server.IRCSend("NICK " + Parameters);
		}

		public static void cmdJoin(mcPage aPage, string Parameters)
		{
			if (Parameters == null) 
			{
				aPage.MessageInfo("Usage: /join <channel>");
				return;
			} 
			aPage.Server.IRCSend("JOIN " + Parameters);
		}

		public static void cmdCycle(mcPage aPage, string Parameters)
		{
			if (Parameters == null)
			{
				aPage.MessageInfo("Usage: /cycle <channel>");
				return;
			}
			
			//hmm, shouldn't we be waiting for reciept of PART/JOIN
			//to display messages?
			aPage.Server.IRCSend("PART " + aPage.Text + " :Cycling channel.");
			aPage.MessageDisplay("<-- " + aPage.Server.MyNickname+" has left " + aPage.Text);
			aPage.Server.IRCSend("JOIN " + aPage.Text);
			aPage.MessageDisplay("--> " + aPage.Server.MyNickname+" has joined "+aPage.Text);
		}

		public static void cmdPart(mcPage aPage, string Parameters)
		{
			string[] parts;
			if (Parameters == null)
			{
				aPage.MessageInfo("Usage: /part <channel> [reason]");
				return;
			}

			parts = Parameters.Split(null);
	
			if(parts.Length < 2) 
			{
				aPage.MessageInfo("Usage: /part <channel> [reason]");
				return;
			}
						
			if(parts.Length == 2)
			{
				aPage.Server.IRCSend("PART " + parts[1]);
			} 
			else
			{
				aPage.Server.IRCSend("PART " + parts[1] + " :" + Parameters.Substring(parts[1].Length + 2));
			}
		}

		public static void cmdExit(mcPage aPage, string Parameters)
		{
			Obsidian.mainForm.Exit(null, null);
		}

		public static void cmdCTCP(mcPage aPage, string Parameters)
		{
			string[] parts;
			if (Parameters == null)
			{
				aPage.MessageInfo("Usage: /ctcp <target> <ctcp>]");
				return;
			}

			parts = Parameters.Split(null);
	
			if(parts.Length < 3)
			{
				aPage.MessageInfo("Usage: /ctcp <target> <ctcp>");
				return;
			}

			aPage.Server.IRCSend("PRIVMSG " + parts[1] + " :" + Parameters.Substring(parts[1].Length + 2) + "");
			aPage.MessageNotice(aPage.Server.MyNickname, "CTCP " + parts[1] + " " + Parameters.Substring(parts[1].Length + 2));
		}

		public static void cmdCTCPReply(mcPage aPage, string Parameters)
		{
			string[] parts;
			if (Parameters == null)
			{
				aPage.MessageInfo("Usage: /ctcpreply <target> <ctcp>]");
				return;
			}

			parts = Parameters.Split(null);
	
			if(parts.Length < 3)
			{
				aPage.MessageInfo("Usage: /ctcpreply <target> <ctcp>");
				return;
			}

			aPage.Server.IRCSend("NOTICE " + parts[1] + " :" + Parameters.Substring(parts[1].Length + 2) + "");
			aPage.MessageNotice(aPage.Server.MyNickname, "CTCPREPLY " + parts[1] + " " + Parameters.Substring(parts[1].Length + 2));
		}

		public static void cmdMsg(mcPage aPage, string Parameters)
		{
			string[] parts;

			if (Parameters == null)
			{
				aPage.MessageInfo("Usage: /msg <nickname> <message>");
				return;
			}

			parts = Parameters.Split(null);

			if(parts.Length < 3)
			{
				aPage.MessageInfo("Usage: /msg <nickname> <message>");
				return;
			}

			aPage.Server.IRCSend("PRIVMSG " + parts[1] + " :" + Parameters.Substring(parts[1].Length + 2));
			aPage.MessagePrivate(parts[1], Parameters.Substring(parts[1].Length + 2));
		}
		
		public static void cmdQuery(mcPage aPage, string Parameters)
		{
			mcPage target;
			string[] parts;
			
			if (Parameters == null)
			{
				aPage.MessageInfo("Usage: /query <nickname>");
				return;
			}

			parts = Parameters.Split(null);

			target = aPage.Server.FindPage(parts[1]);
			if (target == null)
			{
				target = aPage.Server.AddPage(parts[1], mcServer.PageType.Message);
				target.IsChannel = false;
			}
			target.DoFocus();
		}

		public static void cmdMe(mcPage aPage, string Parameters)
		{
			if (Parameters == null)
			{
				aPage.MessageInfo("Usage: /me <does stuff>");
				return;
			}

			aPage.Server.IRCSend("PRIVMSG " + aPage.Text + " :ACTION" + Parameters + "");
			aPage.MessageAction(aPage.Server.MyNickname, Parameters);
		}
		
		public static void cmdSay(mcPage aPage, string Parameters)
		{
			if (Parameters == null)
			{
				aPage.MessageInfo("Usage: /say <text>");
				return;
			}
			aPage.Server.IRCSend("PRIVMSG " + aPage.Text + " :" + Parameters);
			aPage.MessageUser(aPage.Server.MyNickname, Parameters);
		}
		
		public static void cmdClear(mcPage aPage, string Parameters)
		{
			aPage.Clear();
		}

		public mcCommands()
		{
		}

		public static void MainParser(mcPage aPage, string CommandString)
		{
			System.Reflection.MethodInfo m;
			Type t;
			string CurrentLine;
			string[] Lines;
			string MyCmd;
			string Parameters;
			int i;
			int a;

			CommandString = CommandString.Replace('\r','\n');
			Lines = CommandString.Split('\n');

			for(i = 0; i < Lines.Length; i++) 
			{
				CurrentLine = Lines[i];
				if(CurrentLine.Length < 1)
					continue;
				if(CurrentLine[0] == '/')
				{
					a = CurrentLine.IndexOf(" ");
					if (a == -1)
					{
						//one-word command, no params
						MyCmd = CurrentLine.Substring(1);
						Parameters = null;
					}
					else
					{
						//more complex :/
						MyCmd = CurrentLine.Substring(1, a - 1);
						Parameters = CurrentLine.Substring(a);
					}
					/* Try and invoke it now. */
					mcCommands MyScript = new mcCommands();
					t = MyScript.GetType();
					m = t.GetMethod("cmd" + MyCmd, BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase | BindingFlags.Public);
					if (m == null)
					{
						/* no command, send it raw. */
						aPage.Server.IRCSend(MyCmd + " " + Parameters);
					}
					else
					{
						try
						{
							object[] anObj = new object[] {aPage, Parameters};
							m.Invoke(null, anObj);
							anObj = null;
						}
						catch (TargetInvocationException ex)
						{
							aPage.MessageInfo("Scripting error, error dump follows:");
							aPage.MessageInfo(ex.ToString());
						}
					}
				}
				else
				{
					cmdSay(aPage, CurrentLine);
				}
			}
		}
	}
}

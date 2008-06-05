using System;
using System.Reflection;
using System.Windows.Forms;

namespace Obsidian
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited =false)]
	public class Args : System.Attribute
	{
		private readonly int _Args;
		public Args(int MinimumArguments)
		{
			this._Args = MinimumArguments;
		}
	}
	
	public static class GlobalConfig
	{
		public const string FileName = "obsidian.conf";
		// Configuration settings.
		public static string DefaultNickname = SystemInformation.UserName;
		public static string DefaultUsername = SystemInformation.UserName;
		public static string DefaultRealname = SystemInformation.UserName;
		internal static string DefaultQuitMsg = "";
		public static string GetDefaultQuitMsg
		{
			get
			{
				return DefaultQuitMsg;
			}
		}
		
		public static bool LoadConfig()
		{
			// Try to open the config file.
			System.IO.StreamReader fd;
			try
			{
				fd = new System.IO.StreamReader(FileName);
			}
			catch (System.IO.FileNotFoundException)
			{
				// Ignore - Use default config!
				return true;
			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show("Cannot read main config file: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			FieldInfo fi;
			MethodInfo mi;
			BindingFlags bf = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			Type gc = typeof(GlobalConfig);
			string line;
			while (!fd.EndOfStream)
			{
				line = fd.ReadLine();
				string[] tmp = line.Split(" ".ToCharArray(), 2);
				string option, value;
				option = tmp[0];
				value = tmp[1];
				mi = gc.GetMethod("Parse" + option, bf, System.Type.DefaultBinder, new Type[] { typeof(string) }, null);
				if (mi == null)
				{
					// No Parse method, so we need a field that is static and string.
					// Since this is a static class, everything HAS to be static!
					fi = gc.GetField(option, bf);
					if (fi == null || fi.IsInitOnly || fi.IsLiteral || !fi.IsStatic)
					{
						System.Windows.Forms.MessageBox.Show("Unknown configuration option: " + option, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					else
					{
						Type fieldtype = fi.FieldType;
						// It exists. It's static. It's not readonly, and it's not const. SET IT!
						if (!fieldtype.Equals(typeof(string)))
						{
							// Not a string, but we don't have a Parse method. If the type itself has a static Parse method taking a string,
							// use that. Examples include: all numeric types, System.Net.IPAddress.
							mi = fieldtype.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, System.Type.DefaultBinder, new Type[] { typeof(string) }, null);
							try
							{
								fi.SetValue(null, mi.Invoke(null, new object[] { value }));
							}
							catch (Exception e)
							{
								System.Windows.Forms.MessageBox.Show("Failed reading configuration option " + option + ": " + e.InnerException.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
						else
						{
							fi.SetValue(null, value);
						}
					}
				}
				else
				{
					// We have a Parse method - call it.
					try
					{
						mi.Invoke(null, new object[] { value });
					}
					catch (TargetInvocationException e)
					{
						System.Windows.Forms.MessageBox.Show("Failed reading configuration option " + option + ": " + e.InnerException.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
			return true;
		}
		
		public static bool SaveConfig()
		{
			/*
			 * Type gc = typeof(GlobalConfig);
			 * FieldInfo fi;
			 * MethodInfo mi;
			 * BindingFlags bf = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			 */
			return false;
		}
	}

	public class Obsidian
	{
		public const string APP_NAME = "Obsidian";
		public const string APP_VER = "0.0.1-Pie-Alpha1";

		public static mcMainForm mainForm = new mcMainForm();

		private static System.Collections.Generic.List<NetworkThread> mNetThreads = new System.Collections.Generic.List<NetworkThread>();

		public static NetworkThread[] IOThreads
		{
			get
			{
				return (NetworkThread[])(mNetThreads.ToArray());
				//typeof(NetworkThread)
			}
		}

		public static void DoConnect(string address, int port, NetworkThread.ConnectCallback cb)
		{
			foreach (NetworkThread nt in mNetThreads)
			{
				if (nt.AvailableSlot() >= 1)
				{
					nt.AddSocket(address, port, cb);
					return;
				}
			}
			mNetThreads.Add(new NetworkThread(address, port, cb));
		}
		
		public static string FormatTime(TimeSpan ts)
		{
			string txt = "";
			if (ts.Equals(TimeSpan.Zero))
			{
				return "0 seconds";
			}
			if (ts.Days > 0)
			{
				txt += ts.Days.ToString() + " day" + (ts.Days == 1 ? "" : "s");
			}
			if (ts.Hours > 0)
			{
				if (txt.Length > 0)
				{
					txt += " ";
				}
				txt += ts.Hours.ToString() + " hour" + (ts.Hours == 1 ? "" : "s");
			}
			if (ts.Minutes > 0)
			{
				if (txt.Length > 0)
				{
					txt += " ";
				}
				txt += ts.Minutes.ToString() + " minute" + (ts.Minutes == 1 ? "" : "s");
			}
			if (ts.Seconds > 0)
			{
				if (txt.Length > 0)
				{
					txt += " ";
				}
				txt += ts.Seconds.ToString() + " second" + (ts.Seconds == 1 ? "" : "s");
			}
			return txt;
		}
		
		public static string FormatTime(int ts)
		{
			return FormatTime(new TimeSpan(0, 0, ts));
		}

		[STAThread]
		public static void Main()
		{
			/* make sure the networks directory exists here.. */
			if (!System.IO.Directory.Exists("networks"))
				System.IO.Directory.CreateDirectory("networks");

			/* allocate our first Server instance. */
			mcServer aServer;
			aServer = Obsidian.mainForm.AddServer();
			aServer.ServerPage.MessageInfo("Welcome to " + APP_NAME + " v" + APP_VER);

			/* here goes nothing.. */
			System.Windows.Forms.Application.Run(mainForm);
		}
	}
}

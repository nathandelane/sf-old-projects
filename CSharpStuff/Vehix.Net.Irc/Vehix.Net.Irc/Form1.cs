using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Vehix.Net.Irc
{
	public enum Status
	{
		GetRealName,
		GetNickName,
		GetPassword,
		Continue
	}

	public partial class ircForm : Form
	{
		private Status _formStatus;
		private UserSettings _settings;
		private IrcAgent _agent;

		public ircForm()
		{
			InitializeComponent();

			_settings = new UserSettings();
			_formStatus = Status.GetRealName;
		}

		private void SendMessage(object sender, EventArgs e)
		{
			if (_formStatus == Status.GetRealName)
			{
				_settings[UserSettings.RealName] = messageEntryTextBox.Text.Split(' ')[0];
				_formStatus = Status.GetNickName;

				PostMessage();
				Init(sender, e);
			}
			else if (_formStatus == Status.GetNickName)
			{
				_settings[UserSettings.NickName] = messageEntryTextBox.Text.Split(' ')[0];
				_formStatus = Status.GetPassword;

				PostMessage();
				Init(sender, e);
			}
			else if (_formStatus == Status.GetPassword)
			{
				_settings[UserSettings.Password] = messageEntryTextBox.Text.Split(' ')[0];
				_formStatus = Status.Continue;

				PostMessage("");

				Cursor = Cursors.WaitCursor;
				messageEntryTextBox.Enabled = false;
				sendButton.Enabled = false;

				_agent = new IrcAgent();
				_agent.Initialize();

				messageViewingTextBox.Clear();
				PostMessage("Message Of The Day (MOTD):");
				PostMessage(_agent.MOTD);

				ThreadStart threadStart0 = new ThreadStart(_agent.GetCommandResult);
				Thread thread = new Thread(threadStart0);
				thread.Start();

				Cursor = Cursors.Default;
				messageEntryTextBox.Enabled = true;
				sendButton.Enabled = true;
				messageEntryTextBox.Focus();
			}
			else
			{
				string commandMsg = messageEntryTextBox.Text;
				if (commandMsg.StartsWith("/"))
				{
					_agent.SendCommand(messageEntryTextBox.Text);

					//PostMessage(_agent.CommandResult);

					messageEntryTextBox.Clear();

					if (commandMsg.ToUpper().Contains("JOIN"))
					{
						userListBox.Items.Clear();
						foreach (string user in _agent.UserList)
						{
							userListBox.Items.Add(user);
						}

						PostMessage(_agent.ChannelMode);
						PostMessage(_settings[UserSettings.Channel]);
					}
				}
				else
				{
					_agent.SendCommand(messageEntryTextBox.Text);

					PostMessage();
				}
			}
		}

		private void Init(object sender, EventArgs e)
		{
			if (_formStatus == Status.GetRealName)
			{
				messageViewingTextBox.AppendText("Please enter your real name: ");
			}
			else if (_formStatus == Status.GetNickName)
			{
				messageViewingTextBox.AppendText("Please enter a nick name: ");
			}
			else if (_formStatus == Status.GetPassword)
			{
				messageViewingTextBox.AppendText("Please enter your password: ");
			}
		}

		private void PostMessage()
		{
			messageViewingTextBox.AppendText(String.Format("{0}{1}", messageEntryTextBox.Text, Environment.NewLine));
			messageEntryTextBox.Clear();
		}

		private void PostMessage(string message)
		{
			messageViewingTextBox.AppendText(String.Format("{0}{1}", message, Environment.NewLine));
		}

		private void RequestFocus(object sender, EventArgs e)
		{
			messageEntryTextBox.Focus();
		}

		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
			{
				SendMessage(sender, e);
			}
			else
			{
				base.OnKeyPress(e);
			}
		}
	}
}
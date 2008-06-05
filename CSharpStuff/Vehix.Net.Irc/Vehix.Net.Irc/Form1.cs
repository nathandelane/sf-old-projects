using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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

		public ircForm()
		{
			InitializeComponent();

			_settings = new UserSettings();
			_formStatus = Status.GetNickName;
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

				PostMessage();
			}
			else
			{
				PostMessage();
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
	}
}
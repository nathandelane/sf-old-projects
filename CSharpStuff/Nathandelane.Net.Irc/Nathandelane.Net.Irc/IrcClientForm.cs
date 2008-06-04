using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.Net.Irc
{
	public partial class IrcClientForm : Form
	{
		public IrcClientForm()
		{
			InitializeComponent();
		}

		public delegate void SendMessageToServerDelegate(string message);

		public void SendMessageToServer(string message)
		{
			if (messageTextBox.InvokeRequired)
			{
				SendMessageToServerDelegate d = new SendMessageToServerDelegate(SendMessageToServer);
				messageTextBox.Invoke(d, new object[] { message });
			}
			else
			{
				messageTextBox.AppendText(message);
			}
		}

		private void SendMessage(object sender, EventArgs e)
		{

		}
	}
}
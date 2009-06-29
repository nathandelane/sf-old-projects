using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.Vehix.MonitorServers
{
	public partial class NewMonitorForm : Form
	{
		#region Fields

		private Uri _monitorUri;

		#endregion

		#region Properties

		public string MonitorName { get; set; }

		public Uri MonitorURI
		{
			get
			{
				return _monitorUri;
			}

			set
			{
				_monitorUri = value;
			}
		}

		#endregion

		#region Constructors

		public NewMonitorForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void ResetForm(object sender, EventArgs e)
		{
			_serverNameTextBox.Text = String.Empty;
			_serverURITextBox.Text = String.Empty;

			_serverNameTextBox.Focus();
		}

		private void AddMonitor(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(_serverNameTextBox.Text))
			{
				if (!String.IsNullOrEmpty(_serverURITextBox.Text))
				{
					if (Uri.TryCreate(_serverURITextBox.Text, UriKind.Absolute, out _monitorUri))
					{
						MonitorName = _serverNameTextBox.Text;
					}
					else
					{
						MessageBox.Show(this, "URI was malformed. Please fix it and try adding the monitor again.");
					}
				}
				else
				{
					MessageBox.Show(this, "Server URI may not be null.");
				}
			}
			else
			{
				MessageBox.Show(this, "Server Name may not be null.");
			}
		}

		#endregion
	}
}

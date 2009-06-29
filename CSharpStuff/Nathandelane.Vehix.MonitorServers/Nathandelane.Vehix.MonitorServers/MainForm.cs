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
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void OpenNewMonitorForm(object sender, EventArgs e)
		{
			using (NewMonitorForm newMonitorForm = new NewMonitorForm())
			{
				if (!String.IsNullOrEmpty(newMonitorForm.MonitorName) && newMonitorForm.MonitorURI != null)
				{
					_monitorListBox.Items.Add(new Agent(newMonitorForm.MonitorName, newMonitorForm.MonitorURI));
				}
			}
		}
	}
}

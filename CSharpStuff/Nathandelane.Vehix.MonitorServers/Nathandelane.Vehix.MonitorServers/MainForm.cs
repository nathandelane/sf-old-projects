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
		private List<System.Timers.Timer> _timers;

		public MainForm()
		{
			InitializeComponent();

			_timers = new List<System.Timers.Timer>();
		}

		private void OpenNewMonitorForm(object sender, EventArgs e)
		{
			NewMonitorForm newMonitorForm = new NewMonitorForm();
			newMonitorForm.ShowDialog(this);

			if (!String.IsNullOrEmpty(newMonitorForm.MonitorName) && newMonitorForm.MonitorURI != null)
			{
				Agent newAgent = new Agent(newMonitorForm.MonitorName, newMonitorForm.MonitorURI);

				System.Timers.Timer timer = new System.Timers.Timer(15000);
				timer.Enabled = true;
				timer.Elapsed += new System.Timers.ElapsedEventHandler(newAgent.Update);

				_monitorListBox.Items.Add(newAgent);
				_timers.Add(timer);
			}
		}
	}
}

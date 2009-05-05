using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.ManagementUtility
{
	public partial class MainForm : Form
	{
		#region Fields

		private ManagerCollection _managerCollection;

		#endregion

		#region Constructors

		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Event Handlers

		private void ExitManagementUtility(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		#endregion

		private void DisplayNewFileWatcherForm(object sender, EventArgs e)
		{

		}
	}
}

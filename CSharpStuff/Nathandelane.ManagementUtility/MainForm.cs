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

		private NewFileWatcherForm _newFileWatcherForm;

		#endregion

		#region Constructors

		public MainForm()
		{
			InitializeComponent();

			_newFileWatcherForm = new NewFileWatcherForm(_managerListBox);
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
			_newFileWatcherForm.ShowDialog(this);
		}

		private void RemoveManager(object sender, EventArgs e)
		{
			int index = _managerListBox.SelectedIndex;

			_managerListBox.Items.RemoveAt(index);
		}

		private void ValidateItem(object sender, EventArgs e)
		{
			if (_managerListBox.SelectedItem is Manager)
			{
				_removeButton.Enabled = true;
			}
			else
			{
				_removeButton.Enabled = false;
			}
		}
	}
}

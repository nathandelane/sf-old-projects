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
	public partial class NewFileWatcherForm : Form
	{
		private ListBox _managerCollection;

		public NewFileWatcherForm(ListBox managerCollection)
		{
			InitializeComponent();

			_managerCollection = managerCollection;
		}

		private void CloseNewFileWatcherDialog(object sender, FormClosingEventArgs e)
		{
			this.Hide();
		}

		private void HideDialog(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void BrowseForFileToWatch(object sender, EventArgs e)
		{
			DialogResult result = _openFileDialog.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				_fileNameTextBox.Text = _openFileDialog.FileName;
			}
		}

		private void ValidateAndCreateManager(object sender, EventArgs e)
		{
			if (FormIsValid())
			{
				FileManager manager = new FileManager(_nameTextBox.Text, _fileNameTextBox.Text);

				if (_modifiedDateCheckBox.Checked)
				{
					manager.CheckModifiedDate = true;
				}

				manager.MonitorInterval = new TimeSpan(0, 0, _intervalComboBox.SelectedIndex * 5 * 1000);

				int index = _managerCollection.Items.Add(manager);

				((Manager)_managerCollection.Items[index]).Run();

				this.Hide();
			}
			else
			{
				MessageBox.Show("A new File Watcher must have a name, a file associated with it, and a watch interval.");
			}
		}

		private bool FormIsValid()
		{
			bool result = true;

			if (String.IsNullOrEmpty(_nameTextBox.Text))
			{
				result = false;
			}
			else if (String.IsNullOrEmpty(_fileNameTextBox.Text))
			{
				result = false;
			}
			else if (_intervalComboBox.SelectedIndex == 0)
			{
				result = false;
			}

			return result;
		}
	}
}

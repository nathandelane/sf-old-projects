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
		private ManagerCollection _managerCollection;

		public NewFileWatcherForm(ManagerCollection managerCollection)
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

				_managerCollection.Add(_nameTextBox.Text, manager);
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

			return result;
		}
	}
}

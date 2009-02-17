using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.IO.Monitor
{
	public partial class MonitorForm : Form
	{
		private MonitorCollection _monitors;

		public MonitorForm()
		{
			InitializeComponent();

			_monitors = new MonitorCollection();

			UpdateListBoxes();
		}

		private void UpdateListBoxes()
		{
			fileSystemMonitorListBox.Items.Clear();

			var fileSystemMonitors = from fsm in _monitors
									 where fsm is FileSystemMonitor
									 select fsm;

			fileSystemMonitorListBox.Items.AddRange(fileSystemMonitors.ToArray());
		}

		private void IfNameEmptyDuplicate(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(nameTextBox.Text))
			{
				nameTextBox.Text = pathTextBox.Text;
			}
		}

		private void OnEnterSelectAll(object sender, EventArgs e)
		{
			if (sender is TextBox)
			{
				((TextBox)sender).SelectAll();
			}
			else if (sender is ComboBox)
			{
				((ComboBox)sender).SelectAll();
			}
		}

		private void ValidateAndAddItem(object sender, EventArgs e)
		{
			if (!(String.IsNullOrEmpty(nameTextBox.Text) || String.IsNullOrEmpty(pathTextBox.Text) || String.IsNullOrEmpty(filterTextBox.Text)))
			{
				FileSystemMonitorType fsmType = (monitorTypeComboBox.SelectedIndex == 0) ? FileSystemMonitorType.Generic : ((monitorTypeComboBox.SelectedIndex) == 1 ? FileSystemMonitorType.Directory : FileSystemMonitorType.File);
				FileSystemMonitor monitor = new FileSystemMonitor(nameTextBox.Text, pathTextBox.Text, fsmType, filterTextBox.Text);

				monitor.Watcher.Changed += new FileSystemEventHandler(Watcher_Changed);

				_monitors.Add(monitor);

				nameTextBox.Text = String.Empty;
				pathTextBox.Text = String.Empty;
				filterTextBox.Text = "*.*";
				monitorTypeComboBox.Text = String.Empty;

				UpdateListBoxes();
			}
		}

		private void ActivateFileSystemMonitorTab(object sender, EventArgs e)
		{
			tabControl.SelectedIndex = 1;
		}

		private void RemoveFileSystemMonitor(object sender, EventArgs e)
		{
			FileSystemMonitor monitor = (FileSystemMonitor)fileSystemMonitorListBox.SelectedItem;

			_monitors.Remove(monitor);

			UpdateListBoxes();
		}

		private void OnFileSystemMonitorItemSelected(object sender, EventArgs e)
		{
			if (sender is ListBox && fileSystemMonitorListBox.Items.Count > 0)
			{
				removeButton.Enabled = true;
			}
		}

		private void HideForm(object sender, FormClosingEventArgs e)
		{
			this.Visible = false;

			e.Cancel = true;
		}

		private void RestoreMonitorDialog(object sender, EventArgs e)
		{
			this.Visible = true;
		}

		private void CloseMonitorDialog(object sender, EventArgs e)
		{
			this.Close();
			notifyIcon.Visible = false;
			Environment.Exit(0);
		}

		private void ActivateMenu(object sender, EventArgs e)
		{
			if (((MouseEventArgs)e).Button == MouseButtons.Right)
			{
				notificationIconContextMenuStrip.Show();
			}
		}

		private void Watcher_Changed(object sender, FileSystemEventArgs e)
		{
			notifyIcon.BalloonTipTitle = String.Format("{0} changed.", ((FileSystemWatcher)sender).Path);
			notifyIcon.ShowBalloonTip(15000);
		}

		private void OpenFileAndDirectoryChooser(object sender, EventArgs e)
		{
			fileAndDirectoryBrowseContextMenuStrip.Show();
		}

		private void OpenDirectoryChooser(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowseDialog = new FolderBrowserDialog();
			folderBrowseDialog.ShowDialog();

			if (!String.IsNullOrEmpty(folderBrowseDialog.SelectedPath))
			{
				pathTextBox.Text = folderBrowseDialog.SelectedPath;
			}
		}
	}
}

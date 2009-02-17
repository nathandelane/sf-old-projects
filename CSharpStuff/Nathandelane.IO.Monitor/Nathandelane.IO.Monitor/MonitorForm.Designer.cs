namespace Nathandelane.IO.Monitor
{
	partial class MonitorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorForm));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.NewTab = new System.Windows.Forms.TabPage();
			this.newFileSystemMonitorButton = new System.Windows.Forms.Button();
			this.FileSystemMonitorTab = new System.Windows.Forms.TabPage();
			this.removeButton = new System.Windows.Forms.Button();
			this.createNewFileSystemMonitorButton = new System.Windows.Forms.Button();
			this.fileSystemMonitorListBox = new System.Windows.Forms.ListBox();
			this.monitorTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.filterTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.pathTextBox = new System.Windows.Forms.TextBox();
			this.fileAndDirectoryBrowseContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.directoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.notificationIconContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.exitMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pathToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl.SuspendLayout();
			this.NewTab.SuspendLayout();
			this.FileSystemMonitorTab.SuspendLayout();
			this.fileAndDirectoryBrowseContextMenuStrip.SuspendLayout();
			this.notificationIconContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.NewTab);
			this.tabControl.Controls.Add(this.FileSystemMonitorTab);
			this.tabControl.Location = new System.Drawing.Point(12, 12);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(221, 593);
			this.tabControl.TabIndex = 0;
			// 
			// NewTab
			// 
			this.NewTab.Controls.Add(this.newFileSystemMonitorButton);
			this.NewTab.Location = new System.Drawing.Point(4, 22);
			this.NewTab.Name = "NewTab";
			this.NewTab.Size = new System.Drawing.Size(213, 567);
			this.NewTab.TabIndex = 1;
			this.NewTab.Text = "New";
			this.NewTab.UseVisualStyleBackColor = true;
			// 
			// newFileSystemMonitorButton
			// 
			this.newFileSystemMonitorButton.Location = new System.Drawing.Point(12, 13);
			this.newFileSystemMonitorButton.Name = "newFileSystemMonitorButton";
			this.newFileSystemMonitorButton.Size = new System.Drawing.Size(190, 23);
			this.newFileSystemMonitorButton.TabIndex = 0;
			this.newFileSystemMonitorButton.Text = "New File System Monitor";
			this.newFileSystemMonitorButton.UseVisualStyleBackColor = true;
			this.newFileSystemMonitorButton.Click += new System.EventHandler(this.ActivateFileSystemMonitorTab);
			// 
			// FileSystemMonitorTab
			// 
			this.FileSystemMonitorTab.Controls.Add(this.removeButton);
			this.FileSystemMonitorTab.Controls.Add(this.createNewFileSystemMonitorButton);
			this.FileSystemMonitorTab.Controls.Add(this.fileSystemMonitorListBox);
			this.FileSystemMonitorTab.Controls.Add(this.monitorTypeComboBox);
			this.FileSystemMonitorTab.Controls.Add(this.label4);
			this.FileSystemMonitorTab.Controls.Add(this.nameTextBox);
			this.FileSystemMonitorTab.Controls.Add(this.label3);
			this.FileSystemMonitorTab.Controls.Add(this.filterTextBox);
			this.FileSystemMonitorTab.Controls.Add(this.label2);
			this.FileSystemMonitorTab.Controls.Add(this.pathTextBox);
			this.FileSystemMonitorTab.Controls.Add(this.label1);
			this.FileSystemMonitorTab.Location = new System.Drawing.Point(4, 22);
			this.FileSystemMonitorTab.Name = "FileSystemMonitorTab";
			this.FileSystemMonitorTab.Padding = new System.Windows.Forms.Padding(3);
			this.FileSystemMonitorTab.Size = new System.Drawing.Size(213, 567);
			this.FileSystemMonitorTab.TabIndex = 0;
			this.FileSystemMonitorTab.Text = "File System";
			this.FileSystemMonitorTab.UseVisualStyleBackColor = true;
			// 
			// removeButton
			// 
			this.removeButton.Enabled = false;
			this.removeButton.Location = new System.Drawing.Point(132, 538);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(75, 23);
			this.removeButton.TabIndex = 7;
			this.removeButton.Text = "Remove";
			this.removeButton.UseVisualStyleBackColor = true;
			this.removeButton.Click += new System.EventHandler(this.RemoveFileSystemMonitor);
			// 
			// createNewFileSystemMonitorButton
			// 
			this.createNewFileSystemMonitorButton.Location = new System.Drawing.Point(132, 111);
			this.createNewFileSystemMonitorButton.Name = "createNewFileSystemMonitorButton";
			this.createNewFileSystemMonitorButton.Size = new System.Drawing.Size(75, 23);
			this.createNewFileSystemMonitorButton.TabIndex = 5;
			this.createNewFileSystemMonitorButton.Text = "Create";
			this.createNewFileSystemMonitorButton.UseVisualStyleBackColor = true;
			this.createNewFileSystemMonitorButton.Click += new System.EventHandler(this.ValidateAndAddItem);
			// 
			// fileSystemMonitorListBox
			// 
			this.fileSystemMonitorListBox.FormattingEnabled = true;
			this.fileSystemMonitorListBox.Location = new System.Drawing.Point(6, 141);
			this.fileSystemMonitorListBox.Name = "fileSystemMonitorListBox";
			this.fileSystemMonitorListBox.Size = new System.Drawing.Size(201, 394);
			this.fileSystemMonitorListBox.TabIndex = 6;
			this.fileSystemMonitorListBox.Enter += new System.EventHandler(this.OnFileSystemMonitorItemSelected);
			// 
			// monitorTypeComboBox
			// 
			this.monitorTypeComboBox.FormattingEnabled = true;
			this.monitorTypeComboBox.Items.AddRange(new object[] {
            "Directory",
            "File"});
			this.monitorTypeComboBox.Location = new System.Drawing.Point(57, 84);
			this.monitorTypeComboBox.Name = "monitorTypeComboBox";
			this.monitorTypeComboBox.Size = new System.Drawing.Size(150, 21);
			this.monitorTypeComboBox.TabIndex = 4;
			this.monitorTypeComboBox.Enter += new System.EventHandler(this.OnEnterSelectAll);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(20, 87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Type";
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(57, 6);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(150, 20);
			this.nameTextBox.TabIndex = 0;
			this.nameTextBox.Enter += new System.EventHandler(this.OnEnterSelectAll);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Name";
			// 
			// filterTextBox
			// 
			this.filterTextBox.Location = new System.Drawing.Point(57, 58);
			this.filterTextBox.Name = "filterTextBox";
			this.filterTextBox.Size = new System.Drawing.Size(150, 20);
			this.filterTextBox.TabIndex = 3;
			this.filterTextBox.Text = "*.*";
			this.filterTextBox.Enter += new System.EventHandler(this.OnEnterSelectAll);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Filter";
			// 
			// pathTextBox
			// 
			this.pathTextBox.ContextMenuStrip = this.fileAndDirectoryBrowseContextMenuStrip;
			this.pathTextBox.Location = new System.Drawing.Point(57, 32);
			this.pathTextBox.Name = "pathTextBox";
			this.pathTextBox.Size = new System.Drawing.Size(150, 20);
			this.pathTextBox.TabIndex = 1;
			this.pathToolTip.SetToolTip(this.pathTextBox, "Right-click to view a file-chooser.");
			this.pathTextBox.Leave += new System.EventHandler(this.IfNameEmptyDuplicate);
			this.pathTextBox.Enter += new System.EventHandler(this.OnEnterSelectAll);
			// 
			// fileAndDirectoryBrowseContextMenuStrip
			// 
			this.fileAndDirectoryBrowseContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.directoryToolStripMenuItem,
            this.fileToolStripMenuItem});
			this.fileAndDirectoryBrowseContextMenuStrip.Name = "fileAndDirectoryBrowseContextMenuStrip";
			this.fileAndDirectoryBrowseContextMenuStrip.Size = new System.Drawing.Size(153, 70);
			// 
			// directoryToolStripMenuItem
			// 
			this.directoryToolStripMenuItem.Name = "directoryToolStripMenuItem";
			this.directoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.directoryToolStripMenuItem.Text = "Directory";
			this.directoryToolStripMenuItem.Click += new System.EventHandler(this.OpenDirectoryChooser);
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Path";
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.notificationIconContextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Monitor";
			this.notifyIcon.Visible = true;
			this.notifyIcon.Click += new System.EventHandler(this.ActivateMenu);
			this.notifyIcon.DoubleClick += new System.EventHandler(this.RestoreMonitorDialog);
			// 
			// notificationIconContextMenuStrip
			// 
			this.notificationIconContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMonitorToolStripMenuItem});
			this.notificationIconContextMenuStrip.Name = "notificationIconContextMenuStrip";
			this.notificationIconContextMenuStrip.Size = new System.Drawing.Size(137, 26);
			// 
			// exitMonitorToolStripMenuItem
			// 
			this.exitMonitorToolStripMenuItem.Name = "exitMonitorToolStripMenuItem";
			this.exitMonitorToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.exitMonitorToolStripMenuItem.Text = "Exit Monitor";
			this.exitMonitorToolStripMenuItem.Click += new System.EventHandler(this.CloseMonitorDialog);
			// 
			// MonitorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(245, 617);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "MonitorForm";
			this.Text = "Nathandelane\'s Monitor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HideForm);
			this.tabControl.ResumeLayout(false);
			this.NewTab.ResumeLayout(false);
			this.FileSystemMonitorTab.ResumeLayout(false);
			this.FileSystemMonitorTab.PerformLayout();
			this.fileAndDirectoryBrowseContextMenuStrip.ResumeLayout(false);
			this.notificationIconContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage FileSystemMonitorTab;
		private System.Windows.Forms.TextBox filterTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox pathTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox monitorTypeComboBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabPage NewTab;
		private System.Windows.Forms.Button newFileSystemMonitorButton;
		private System.Windows.Forms.ListBox fileSystemMonitorListBox;
		private System.Windows.Forms.Button createNewFileSystemMonitorButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip notificationIconContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem exitMonitorToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip fileAndDirectoryBrowseContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem directoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolTip pathToolTip;
	}
}


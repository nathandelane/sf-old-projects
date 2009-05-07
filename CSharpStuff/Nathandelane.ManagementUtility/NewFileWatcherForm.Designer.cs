namespace Nathandelane.ManagementUtility
{
	partial class NewFileWatcherForm
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
			this.label1 = new System.Windows.Forms.Label();
			this._nameTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._modifiedDateCheckBox = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this._fileNameTextBox = new System.Windows.Forms.TextBox();
			this._browseButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this._intervalComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// _nameTextBox
			// 
			this._nameTextBox.Location = new System.Drawing.Point(57, 10);
			this._nameTextBox.Name = "_nameTextBox";
			this._nameTextBox.Size = new System.Drawing.Size(248, 20);
			this._nameTextBox.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._modifiedDateCheckBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 89);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(293, 48);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "What to Monitor";
			// 
			// _modifiedDateCheckBox
			// 
			this._modifiedDateCheckBox.AutoSize = true;
			this._modifiedDateCheckBox.Checked = true;
			this._modifiedDateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._modifiedDateCheckBox.Enabled = false;
			this._modifiedDateCheckBox.Location = new System.Drawing.Point(6, 19);
			this._modifiedDateCheckBox.Name = "_modifiedDateCheckBox";
			this._modifiedDateCheckBox.Size = new System.Drawing.Size(92, 17);
			this._modifiedDateCheckBox.TabIndex = 3;
			this._modifiedDateCheckBox.Text = "Modified Date";
			this._modifiedDateCheckBox.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "File Name:";
			// 
			// _fileNameTextBox
			// 
			this._fileNameTextBox.Location = new System.Drawing.Point(75, 36);
			this._fileNameTextBox.Name = "_fileNameTextBox";
			this._fileNameTextBox.Size = new System.Drawing.Size(160, 20);
			this._fileNameTextBox.TabIndex = 1;
			// 
			// _browseButton
			// 
			this._browseButton.Location = new System.Drawing.Point(241, 34);
			this._browseButton.Name = "_browseButton";
			this._browseButton.Size = new System.Drawing.Size(64, 23);
			this._browseButton.TabIndex = 2;
			this._browseButton.Text = "Browse...";
			this._browseButton.UseVisualStyleBackColor = true;
			this._browseButton.Click += new System.EventHandler(this.BrowseForFileToWatch);
			// 
			// _okButton
			// 
			this._okButton.Location = new System.Drawing.Point(179, 143);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(56, 23);
			this._okButton.TabIndex = 4;
			this._okButton.Text = "OK";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this.ValidateAndCreateManager);
			// 
			// _cancelButton
			// 
			this._cancelButton.Location = new System.Drawing.Point(241, 143);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(64, 23);
			this._cancelButton.TabIndex = 5;
			this._cancelButton.Text = "Cancel";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this.HideDialog);
			// 
			// _openFileDialog
			// 
			this._openFileDialog.FileName = "openFileDialog1";
			// 
			// _intervalComboBox
			// 
			this._intervalComboBox.FormattingEnabled = true;
			this._intervalComboBox.Items.AddRange(new object[] {
            "Select an interval to check the file by.",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60"});
			this._intervalComboBox.Location = new System.Drawing.Point(93, 62);
			this._intervalComboBox.Name = "_intervalComboBox";
			this._intervalComboBox.Size = new System.Drawing.Size(212, 21);
			this._intervalComboBox.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Timer Interval:";
			// 
			// NewFileWatcherForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(317, 178);
			this.Controls.Add(this.label3);
			this.Controls.Add(this._intervalComboBox);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._browseButton);
			this.Controls.Add(this._fileNameTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._nameTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(323, 175);
			this.Name = "NewFileWatcherForm";
			this.Text = "New File Watcher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseNewFileWatcherDialog);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _nameTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox _modifiedDateCheckBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _fileNameTextBox;
		private System.Windows.Forms.Button _browseButton;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.OpenFileDialog _openFileDialog;
		private System.Windows.Forms.ComboBox _intervalComboBox;
		private System.Windows.Forms.Label label3;
	}
}
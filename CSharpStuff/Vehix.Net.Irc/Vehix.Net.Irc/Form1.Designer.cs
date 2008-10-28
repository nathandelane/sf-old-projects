namespace Vehix.Net.Irc
{
	partial class ircForm
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
			this.messageEntryTextBox = new System.Windows.Forms.TextBox();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.messageViewingTextBox = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.sendButton = new System.Windows.Forms.Button();
			this.userListBox = new System.Windows.Forms.ListBox();
			this.tabPage1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// messageEntryTextBox
			// 
			this.messageEntryTextBox.Location = new System.Drawing.Point(4, 376);
			this.messageEntryTextBox.Name = "messageEntryTextBox";
			this.messageEntryTextBox.Size = new System.Drawing.Size(505, 20);
			this.messageEntryTextBox.TabIndex = 0;
			this.messageEntryTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.messageViewingTextBox);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(582, 343);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "irc.freenode.net";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// messageViewingTextBox
			// 
			this.messageViewingTextBox.Location = new System.Drawing.Point(6, 6);
			this.messageViewingTextBox.Multiline = true;
			this.messageViewingTextBox.Name = "messageViewingTextBox";
			this.messageViewingTextBox.ReadOnly = true;
			this.messageViewingTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.messageViewingTextBox.Size = new System.Drawing.Size(570, 331);
			this.messageViewingTextBox.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(0, 1);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(590, 369);
			this.tabControl1.TabIndex = 1;
			// 
			// sendButton
			// 
			this.sendButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.sendButton.Location = new System.Drawing.Point(515, 376);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(75, 20);
			this.sendButton.TabIndex = 3;
			this.sendButton.Text = "Send";
			this.sendButton.UseVisualStyleBackColor = true;
			this.sendButton.Click += new System.EventHandler(this.SendMessage);
			// 
			// userListBox
			// 
			this.userListBox.FormattingEnabled = true;
			this.userListBox.Location = new System.Drawing.Point(596, 12);
			this.userListBox.Name = "userListBox";
			this.userListBox.Size = new System.Drawing.Size(184, 381);
			this.userListBox.TabIndex = 4;
			// 
			// ircForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 401);
			this.Controls.Add(this.userListBox);
			this.Controls.Add(this.sendButton);
			this.Controls.Add(this.messageEntryTextBox);
			this.Controls.Add(this.tabControl1);
			this.Name = "ircForm";
			this.Text = "Vehix Irc Client";
			this.Load += new System.EventHandler(this.Init);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox messageEntryTextBox;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TextBox messageViewingTextBox;
		private System.Windows.Forms.Button sendButton;
		private System.Windows.Forms.ListBox userListBox;
	}
}


namespace Nathandelane.Net.Irc
{
	partial class IrcClientForm
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
			this.messageTextBox = new System.Windows.Forms.TextBox();
			this.commandTextBox = new System.Windows.Forms.TextBox();
			this.acceptButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// messageTextBox
			// 
			this.messageTextBox.Location = new System.Drawing.Point(12, 12);
			this.messageTextBox.Multiline = true;
			this.messageTextBox.Name = "messageTextBox";
			this.messageTextBox.Size = new System.Drawing.Size(723, 408);
			this.messageTextBox.TabIndex = 0;
			// 
			// commandTextBox
			// 
			this.commandTextBox.Location = new System.Drawing.Point(12, 426);
			this.commandTextBox.Name = "commandTextBox";
			this.commandTextBox.Size = new System.Drawing.Size(642, 20);
			this.commandTextBox.TabIndex = 1;
			// 
			// acceptButton
			// 
			this.acceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.acceptButton.Location = new System.Drawing.Point(660, 424);
			this.acceptButton.Name = "acceptButton";
			this.acceptButton.Size = new System.Drawing.Size(75, 23);
			this.acceptButton.TabIndex = 2;
			this.acceptButton.Text = "Send";
			this.acceptButton.UseVisualStyleBackColor = true;
			this.acceptButton.Click += new System.EventHandler(this.SendMessage);
			// 
			// IrcClientForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(747, 458);
			this.Controls.Add(this.acceptButton);
			this.Controls.Add(this.commandTextBox);
			this.Controls.Add(this.messageTextBox);
			this.Name = "IrcClientForm";
			this.Text = "IrcClientForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox messageTextBox;
		private System.Windows.Forms.TextBox commandTextBox;
		private System.Windows.Forms.Button acceptButton;
	}
}
namespace TaskKiller
{
	partial class Form1
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
			this.taskTextField = new System.Windows.Forms.TextBox();
			this.killButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// taskTextField
			// 
			this.taskTextField.Location = new System.Drawing.Point(12, 3);
			this.taskTextField.Name = "taskTextField";
			this.taskTextField.Size = new System.Drawing.Size(100, 20);
			this.taskTextField.TabIndex = 0;
			this.taskTextField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CaptureKeys);
			// 
			// killButton
			// 
			this.killButton.Location = new System.Drawing.Point(118, 0);
			this.killButton.Name = "killButton";
			this.killButton.Size = new System.Drawing.Size(75, 23);
			this.killButton.TabIndex = 1;
			this.killButton.Text = "Kill";
			this.killButton.UseVisualStyleBackColor = true;
			this.killButton.Click += new System.EventHandler(this.KillTask);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(207, 26);
			this.Controls.Add(this.killButton);
			this.Controls.Add(this.taskTextField);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "TaskKiller";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox taskTextField;
		private System.Windows.Forms.Button killButton;
	}
}


namespace Nathandelane.Vehix.MonitorServers
{
	partial class NewMonitorForm
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
			this._serverNameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._serverURITextBox = new System.Windows.Forms.TextBox();
			this._formResetButton = new System.Windows.Forms.Button();
			this._addButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(51, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server Name:";
			// 
			// _serverNameTextBox
			// 
			this._serverNameTextBox.Location = new System.Drawing.Point(129, 12);
			this._serverNameTextBox.Name = "_serverNameTextBox";
			this._serverNameTextBox.Size = new System.Drawing.Size(314, 20);
			this._serverNameTextBox.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(60, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Server URI:";
			// 
			// _serverURITextBox
			// 
			this._serverURITextBox.Location = new System.Drawing.Point(129, 38);
			this._serverURITextBox.Name = "_serverURITextBox";
			this._serverURITextBox.Size = new System.Drawing.Size(314, 20);
			this._serverURITextBox.TabIndex = 3;
			// 
			// _formResetButton
			// 
			this._formResetButton.Location = new System.Drawing.Point(368, 64);
			this._formResetButton.Name = "_formResetButton";
			this._formResetButton.Size = new System.Drawing.Size(75, 23);
			this._formResetButton.TabIndex = 4;
			this._formResetButton.Text = "Reset";
			this._formResetButton.UseVisualStyleBackColor = true;
			this._formResetButton.Click += new System.EventHandler(this.ResetForm);
			// 
			// _addButton
			// 
			this._addButton.Location = new System.Drawing.Point(287, 64);
			this._addButton.Name = "_addButton";
			this._addButton.Size = new System.Drawing.Size(75, 23);
			this._addButton.TabIndex = 5;
			this._addButton.Text = "Add";
			this._addButton.UseVisualStyleBackColor = true;
			this._addButton.Click += new System.EventHandler(this.AddMonitor);
			// 
			// NewMonitorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 99);
			this.Controls.Add(this._addButton);
			this.Controls.Add(this._formResetButton);
			this.Controls.Add(this._serverURITextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._serverNameTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(461, 123);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(461, 123);
			this.Name = "NewMonitorForm";
			this.Text = "New Monitor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _serverNameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _serverURITextBox;
		private System.Windows.Forms.Button _formResetButton;
		private System.Windows.Forms.Button _addButton;
	}
}
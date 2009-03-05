namespace Nathandelane.Net.WebGet.GraphicalWebGet
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
			this._urlTextBox = new System.Windows.Forms.TextBox();
			this._clearButton = new System.Windows.Forms.Button();
			this._goButton = new System.Windows.Forms.Button();
			this._resourceListBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// _urlTextBox
			// 
			this._urlTextBox.Location = new System.Drawing.Point(12, 12);
			this._urlTextBox.Name = "_urlTextBox";
			this._urlTextBox.Size = new System.Drawing.Size(440, 20);
			this._urlTextBox.TabIndex = 0;
			// 
			// _clearButton
			// 
			this._clearButton.Location = new System.Drawing.Point(377, 38);
			this._clearButton.Name = "_clearButton";
			this._clearButton.Size = new System.Drawing.Size(75, 23);
			this._clearButton.TabIndex = 1;
			this._clearButton.Text = "Clear";
			this._clearButton.UseVisualStyleBackColor = true;
			this._clearButton.Click += new System.EventHandler(this.ClearUrlTextBox);
			// 
			// _goButton
			// 
			this._goButton.Location = new System.Drawing.Point(296, 38);
			this._goButton.Name = "_goButton";
			this._goButton.Size = new System.Drawing.Size(75, 23);
			this._goButton.TabIndex = 2;
			this._goButton.Text = "Go";
			this._goButton.UseVisualStyleBackColor = true;
			this._goButton.Click += new System.EventHandler(this.AddResource);
			// 
			// _resourceListBox
			// 
			this._resourceListBox.FormattingEnabled = true;
			this._resourceListBox.Location = new System.Drawing.Point(12, 67);
			this._resourceListBox.Name = "_resourceListBox";
			this._resourceListBox.Size = new System.Drawing.Size(440, 394);
			this._resourceListBox.TabIndex = 3;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(464, 469);
			this.Controls.Add(this._resourceListBox);
			this.Controls.Add(this._goButton);
			this.Controls.Add(this._clearButton);
			this.Controls.Add(this._urlTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Graphical WGet";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _urlTextBox;
		private System.Windows.Forms.Button _clearButton;
		private System.Windows.Forms.Button _goButton;
		private System.Windows.Forms.ListBox _resourceListBox;
	}
}


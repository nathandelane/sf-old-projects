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
			this._saveAsTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _urlTextBox
			// 
			this._urlTextBox.Location = new System.Drawing.Point(12, 12);
			this._urlTextBox.Name = "_urlTextBox";
			this._urlTextBox.Size = new System.Drawing.Size(658, 20);
			this._urlTextBox.TabIndex = 0;
			// 
			// _clearButton
			// 
			this._clearButton.Location = new System.Drawing.Point(595, 64);
			this._clearButton.Name = "_clearButton";
			this._clearButton.Size = new System.Drawing.Size(75, 23);
			this._clearButton.TabIndex = 1;
			this._clearButton.Text = "Clear";
			this._clearButton.UseVisualStyleBackColor = true;
			this._clearButton.Click += new System.EventHandler(this.ClearUrlTextBox);
			// 
			// _goButton
			// 
			this._goButton.Location = new System.Drawing.Point(514, 64);
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
			this._resourceListBox.Location = new System.Drawing.Point(12, 93);
			this._resourceListBox.Name = "_resourceListBox";
			this._resourceListBox.Size = new System.Drawing.Size(658, 368);
			this._resourceListBox.TabIndex = 3;
			// 
			// _saveAsTextBox
			// 
			this._saveAsTextBox.Location = new System.Drawing.Point(65, 38);
			this._saveAsTextBox.Name = "_saveAsTextBox";
			this._saveAsTextBox.Size = new System.Drawing.Size(605, 20);
			this._saveAsTextBox.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Save As";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(682, 469);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._saveAsTextBox);
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
		private System.Windows.Forms.TextBox _saveAsTextBox;
		private System.Windows.Forms.Label label1;
	}
}


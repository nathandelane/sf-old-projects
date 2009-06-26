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
			this._resourceListBox = new System.Windows.Forms.ListBox();
			this._resetButton = new System.Windows.Forms.Button();
			this._goButton = new System.Windows.Forms.Button();
			this._saveAsTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._urlClearButton = new System.Windows.Forms.Button();
			this._urlTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this._clearListButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _resourceListBox
			// 
			this._resourceListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._resourceListBox.Font = new System.Drawing.Font("Lucida Sans Unicode", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._resourceListBox.FormattingEnabled = true;
			this._resourceListBox.ItemHeight = 15;
			this._resourceListBox.Location = new System.Drawing.Point(15, 96);
			this._resourceListBox.Name = "_resourceListBox";
			this._resourceListBox.ScrollAlwaysVisible = true;
			this._resourceListBox.Size = new System.Drawing.Size(655, 364);
			this._resourceListBox.TabIndex = 2;
			// 
			// _resetButton
			// 
			this._resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._resetButton.Location = new System.Drawing.Point(595, 64);
			this._resetButton.Name = "_resetButton";
			this._resetButton.Size = new System.Drawing.Size(75, 23);
			this._resetButton.TabIndex = 13;
			this._resetButton.Text = "Reset";
			this._resetButton.UseVisualStyleBackColor = true;
			// 
			// _goButton
			// 
			this._goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._goButton.Location = new System.Drawing.Point(514, 64);
			this._goButton.Name = "_goButton";
			this._goButton.Size = new System.Drawing.Size(75, 23);
			this._goButton.TabIndex = 12;
			this._goButton.Text = "Go";
			this._goButton.UseVisualStyleBackColor = true;
			// 
			// _saveAsTextBox
			// 
			this._saveAsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._saveAsTextBox.Location = new System.Drawing.Point(68, 38);
			this._saveAsTextBox.Name = "_saveAsTextBox";
			this._saveAsTextBox.Size = new System.Drawing.Size(602, 20);
			this._saveAsTextBox.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Save As:";
			// 
			// _urlClearButton
			// 
			this._urlClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._urlClearButton.Location = new System.Drawing.Point(599, 10);
			this._urlClearButton.Name = "_urlClearButton";
			this._urlClearButton.Size = new System.Drawing.Size(71, 23);
			this._urlClearButton.TabIndex = 9;
			this._urlClearButton.Text = "Clear";
			this._urlClearButton.UseVisualStyleBackColor = true;
			// 
			// _urlTextBox
			// 
			this._urlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._urlTextBox.Location = new System.Drawing.Point(50, 12);
			this._urlTextBox.Name = "_urlTextBox";
			this._urlTextBox.Size = new System.Drawing.Size(543, 20);
			this._urlTextBox.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "URL:";
			// 
			// _clearListButton
			// 
			this._clearListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._clearListButton.Location = new System.Drawing.Point(595, 475);
			this._clearListButton.Name = "_clearListButton";
			this._clearListButton.Size = new System.Drawing.Size(75, 23);
			this._clearListButton.TabIndex = 14;
			this._clearListButton.Text = "Clear List";
			this._clearListButton.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(682, 510);
			this.Controls.Add(this._clearListButton);
			this.Controls.Add(this._resetButton);
			this.Controls.Add(this._goButton);
			this.Controls.Add(this._saveAsTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._urlClearButton);
			this.Controls.Add(this._urlTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._resourceListBox);
			this.MinimumSize = new System.Drawing.Size(698, 546);
			this.Name = "Form1";
			this.Text = "Graphical WGet";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox _resourceListBox;
		private System.Windows.Forms.Button _resetButton;
		private System.Windows.Forms.Button _goButton;
		private System.Windows.Forms.TextBox _saveAsTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button _urlClearButton;
		private System.Windows.Forms.TextBox _urlTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _clearListButton;

	}
}


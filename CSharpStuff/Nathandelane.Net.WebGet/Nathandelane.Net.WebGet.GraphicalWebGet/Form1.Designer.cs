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
			this._topPanel = new System.Windows.Forms.Panel();
			this._resetButton = new System.Windows.Forms.Button();
			this._goButton = new System.Windows.Forms.Button();
			this._saveAsTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this._urlClearButton = new System.Windows.Forms.Button();
			this._urlTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this._clearListButton = new System.Windows.Forms.Button();
			this._middlePanel = new System.Windows.Forms.Panel();
			this._resourceListBox = new System.Windows.Forms.ListBox();
			this._topPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this._middlePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _topPanel
			// 
			this._topPanel.Controls.Add(this._resetButton);
			this._topPanel.Controls.Add(this._goButton);
			this._topPanel.Controls.Add(this._saveAsTextBox);
			this._topPanel.Controls.Add(this.label2);
			this._topPanel.Controls.Add(this._urlClearButton);
			this._topPanel.Controls.Add(this._urlTextBox);
			this._topPanel.Controls.Add(this.label1);
			this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this._topPanel.Location = new System.Drawing.Point(0, 0);
			this._topPanel.Name = "_topPanel";
			this._topPanel.Size = new System.Drawing.Size(682, 90);
			this._topPanel.TabIndex = 0;
			// 
			// _resetButton
			// 
			this._resetButton.Location = new System.Drawing.Point(595, 58);
			this._resetButton.Name = "_resetButton";
			this._resetButton.Size = new System.Drawing.Size(75, 23);
			this._resetButton.TabIndex = 6;
			this._resetButton.Text = "Reset";
			this._resetButton.UseVisualStyleBackColor = true;
			this._resetButton.Click += new System.EventHandler(this.ResetForm);
			// 
			// _goButton
			// 
			this._goButton.Location = new System.Drawing.Point(514, 58);
			this._goButton.Name = "_goButton";
			this._goButton.Size = new System.Drawing.Size(75, 23);
			this._goButton.TabIndex = 5;
			this._goButton.Text = "Go";
			this._goButton.UseVisualStyleBackColor = true;
			this._goButton.Click += new System.EventHandler(this.StartWget);
			// 
			// _saveAsTextBox
			// 
			this._saveAsTextBox.Location = new System.Drawing.Point(68, 32);
			this._saveAsTextBox.Name = "_saveAsTextBox";
			this._saveAsTextBox.Size = new System.Drawing.Size(602, 20);
			this._saveAsTextBox.TabIndex = 4;
			this._saveAsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilterEnterKey);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Save As:";
			// 
			// _urlClearButton
			// 
			this._urlClearButton.Location = new System.Drawing.Point(599, 4);
			this._urlClearButton.Name = "_urlClearButton";
			this._urlClearButton.Size = new System.Drawing.Size(71, 23);
			this._urlClearButton.TabIndex = 2;
			this._urlClearButton.Text = "Clear";
			this._urlClearButton.UseVisualStyleBackColor = true;
			this._urlClearButton.Click += new System.EventHandler(this.ClearUrlTextBox);
			// 
			// _urlTextBox
			// 
			this._urlTextBox.Location = new System.Drawing.Point(50, 6);
			this._urlTextBox.Name = "_urlTextBox";
			this._urlTextBox.Size = new System.Drawing.Size(543, 20);
			this._urlTextBox.TabIndex = 1;
			this._urlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilterEnterKey);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "URL:";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this._clearListButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 469);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(682, 41);
			this.panel1.TabIndex = 1;
			// 
			// _clearListButton
			// 
			this._clearListButton.Location = new System.Drawing.Point(595, 6);
			this._clearListButton.Name = "_clearListButton";
			this._clearListButton.Size = new System.Drawing.Size(75, 23);
			this._clearListButton.TabIndex = 0;
			this._clearListButton.Text = "Clear List";
			this._clearListButton.UseVisualStyleBackColor = true;
			this._clearListButton.Click += new System.EventHandler(this.ClearList);
			// 
			// _middlePanel
			// 
			this._middlePanel.Controls.Add(this._resourceListBox);
			this._middlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._middlePanel.Location = new System.Drawing.Point(0, 90);
			this._middlePanel.Name = "_middlePanel";
			this._middlePanel.Size = new System.Drawing.Size(682, 379);
			this._middlePanel.TabIndex = 2;
			// 
			// _resourceListBox
			// 
			this._resourceListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._resourceListBox.FormattingEnabled = true;
			this._resourceListBox.Location = new System.Drawing.Point(0, 0);
			this._resourceListBox.Name = "_resourceListBox";
			this._resourceListBox.Size = new System.Drawing.Size(682, 368);
			this._resourceListBox.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(682, 510);
			this.Controls.Add(this._middlePanel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this._topPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Graphical WGet";
			this._topPanel.ResumeLayout(false);
			this._topPanel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this._middlePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel _topPanel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button _urlClearButton;
		private System.Windows.Forms.TextBox _urlTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _resetButton;
		private System.Windows.Forms.Button _goButton;
		private System.Windows.Forms.TextBox _saveAsTextBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button _clearListButton;
		private System.Windows.Forms.Panel _middlePanel;
		private System.Windows.Forms.ListBox _resourceListBox;

	}
}


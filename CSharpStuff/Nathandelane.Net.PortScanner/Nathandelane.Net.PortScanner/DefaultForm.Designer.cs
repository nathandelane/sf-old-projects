namespace Nathandelane.Net.PortScanner
{
	partial class DefaultForm
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
			this._commandTextBox = new System.Windows.Forms.TextBox();
			this._outputTextBox = new System.Windows.Forms.TextBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this._goButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _commandTextBox
			// 
			this._commandTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._commandTextBox.Location = new System.Drawing.Point(12, 12);
			this._commandTextBox.Name = "_commandTextBox";
			this._commandTextBox.Size = new System.Drawing.Size(700, 23);
			this._commandTextBox.TabIndex = 0;
			this._commandTextBox.Leave += new System.EventHandler(this.OnBlur);
			this._commandTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScanKeys);
			// 
			// _outputTextBox
			// 
			this._outputTextBox.BackColor = System.Drawing.Color.White;
			this._outputTextBox.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._outputTextBox.Location = new System.Drawing.Point(12, 38);
			this._outputTextBox.Multiline = true;
			this._outputTextBox.Name = "_outputTextBox";
			this._outputTextBox.ReadOnly = true;
			this._outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this._outputTextBox.Size = new System.Drawing.Size(737, 184);
			this._outputTextBox.TabIndex = 1;
			this._outputTextBox.WordWrap = false;
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(331, 237);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(80, 20);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Clear Text";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ClearOutputTextBox);
			// 
			// _goButton
			// 
			this._goButton.Location = new System.Drawing.Point(718, 12);
			this._goButton.Name = "_goButton";
			this._goButton.Size = new System.Drawing.Size(31, 23);
			this._goButton.TabIndex = 3;
			this._goButton.Text = ">>";
			this._goButton.UseVisualStyleBackColor = true;
			this._goButton.Click += new System.EventHandler(this.ExecuteCommand);
			// 
			// DefaultForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(761, 266);
			this.Controls.Add(this._goButton);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this._outputTextBox);
			this.Controls.Add(this._commandTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "DefaultForm";
			this.Text = "Text Container";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _commandTextBox;
		private System.Windows.Forms.TextBox _outputTextBox;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button _goButton;
	}
}


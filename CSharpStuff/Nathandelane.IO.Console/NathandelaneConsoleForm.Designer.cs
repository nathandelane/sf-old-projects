namespace Nathandelane.IO.Console
{
	partial class NathandelaneConsoleForm
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
			this.commandTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// commandTextBox
			// 
			this.commandTextBox.AcceptsReturn = true;
			this.commandTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.commandTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.commandTextBox.BackColor = System.Drawing.Color.Black;
			this.commandTextBox.Font = new System.Drawing.Font("ProggyTinyTTSZ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.commandTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.commandTextBox.Location = new System.Drawing.Point(0, -2);
			this.commandTextBox.Multiline = true;
			this.commandTextBox.Name = "commandTextBox";
			this.commandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.commandTextBox.Size = new System.Drawing.Size(1109, 639);
			this.commandTextBox.TabIndex = 0;
			this.commandTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CaptureKeys);
			// 
			// NathandelaneConsoleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1108, 634);
			this.Controls.Add(this.commandTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "NathandelaneConsoleForm";
			this.Opacity = 0.85;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Nathandelane Console";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox commandTextBox;

	}
}


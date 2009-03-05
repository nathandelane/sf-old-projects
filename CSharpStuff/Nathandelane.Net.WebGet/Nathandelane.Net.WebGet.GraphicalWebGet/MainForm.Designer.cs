/*
 * Created by SharpDevelop.
 * User: nathanl
 * Date: 3/5/2009
 * Time: 12:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Nathandelane.Net.WebGet.GraphicalWebGet
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.urlTextBox = new System.Windows.Forms.TextBox();
			this.goButton = new System.Windows.Forms.Button();
			this.resourceListBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
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
			// urlTextBox
			// 
			this.urlTextBox.Location = new System.Drawing.Point(50, 6);
			this.urlTextBox.Name = "urlTextBox";
			this.urlTextBox.Size = new System.Drawing.Size(290, 20);
			this.urlTextBox.TabIndex = 1;
			// 
			// goButton
			// 
			this.goButton.Location = new System.Drawing.Point(346, 4);
			this.goButton.Name = "goButton";
			this.goButton.Size = new System.Drawing.Size(54, 23);
			this.goButton.TabIndex = 2;
			this.goButton.Text = "Go";
			this.goButton.UseVisualStyleBackColor = true;
			this.goButton.Click += new System.EventHandler(this.AddResourceAndBeginDownload);
			// 
			// resourceListBox
			// 
			this.resourceListBox.FormattingEnabled = true;
			this.resourceListBox.Location = new System.Drawing.Point(12, 32);
			this.resourceListBox.Name = "resourceListBox";
			this.resourceListBox.Size = new System.Drawing.Size(388, 290);
			this.resourceListBox.TabIndex = 3;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(412, 331);
			this.Controls.Add(this.resourceListBox);
			this.Controls.Add(this.goButton);
			this.Controls.Add(this.urlTextBox);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "Graphical WGet";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox urlTextBox;
		private System.Windows.Forms.Button goButton;
		private System.Windows.Forms.ListBox resourceListBox;
	}
}

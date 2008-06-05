using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Obsidian
{
	/// <summary>
	/// Summary description for mcAbout.
	/// </summary>
	sealed public class mcAbout : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblRunningVersion;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblAbout;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public mcAbout()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(mcAbout));
			this.lblRunningVersion = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmdOk = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblAbout = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblRunningVersion
			// 
			this.lblRunningVersion.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblRunningVersion.Location = new System.Drawing.Point(0, 104);
			this.lblRunningVersion.Name = "lblRunningVersion";
			this.lblRunningVersion.Size = new System.Drawing.Size(304, 16);
			this.lblRunningVersion.TabIndex = 2;
			this.lblRunningVersion.Text = "#APPVERSION";
			this.lblRunningVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblCopyright
			// 
			this.lblCopyright.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCopyright.Location = new System.Drawing.Point(8, 200);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(304, 16);
			this.lblCopyright.TabIndex = 3;
			this.lblCopyright.Text = "Copyright © 2005 Robin Burchell";
			this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(8, 216);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(288, 8);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			// 
			// cmdOk
			// 
			this.cmdOk.Location = new System.Drawing.Point(240, 229);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(64, 24);
			this.cmdOk.TabIndex = 5;
			this.cmdOk.Text = "Ok";
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(294, 86);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			// 
			// lblAbout
			// 
			this.lblAbout.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblAbout.Location = new System.Drawing.Point(8, 128);
			this.lblAbout.Name = "lblAbout";
			this.lblAbout.Size = new System.Drawing.Size(296, 16);
			this.lblAbout.TabIndex = 9;
			this.lblAbout.Text = "A free, multi-platform IRC Client";
			this.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mcAbout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 259);
			this.Controls.Add(this.lblAbout);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblRunningVersion);
			this.Controls.Add(this.pictureBox1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(320, 292);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(320, 292);
			this.Name = "mcAbout";
			this.Text = "About...";
			this.Load += new System.EventHandler(this.mcAbout_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void mcAbout_Load(object sender, System.EventArgs e)
		{
			lblRunningVersion.Text = Obsidian.APP_VER;
		}

		private void cmdOk_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}

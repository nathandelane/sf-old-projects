using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Obsidian
{
	/// <summary>
	/// Summary description for mcOptions.
	/// </summary>
	sealed public class mcOptions : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		public System.Windows.Forms.TrackBar OpacityTrackBar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtNickname;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtRealname;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label label5;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public mcOptions()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label5 = new System.Windows.Forms.Label();
			this.txtRealname = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtNickname = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.OpacityTrackBar = new System.Windows.Forms.TrackBar();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.OpacityTrackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(400, 264);
			this.tabControl1.TabIndex = 5;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.txtRealname);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.txtUsername);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.txtNickname);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(392, 238);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "My Settings";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(48, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(160, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "These don\'t do anything yet.";
			// 
			// txtRealname
			// 
			this.txtRealname.Location = new System.Drawing.Point(208, 184);
			this.txtRealname.Name = "txtRealname";
			this.txtRealname.Size = new System.Drawing.Size(120, 20);
			this.txtRealname.TabIndex = 5;
			this.txtRealname.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(152, 184);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Realname:";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(208, 160);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(120, 20);
			this.txtUsername.TabIndex = 3;
			this.txtUsername.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(152, 168);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Username:";
			// 
			// txtNickname
			// 
			this.txtNickname.Location = new System.Drawing.Point(208, 144);
			this.txtNickname.Name = "txtNickname";
			this.txtNickname.Size = new System.Drawing.Size(120, 20);
			this.txtNickname.TabIndex = 1;
			this.txtNickname.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(152, 144);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Nickname:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.label1);
			this.tabPage2.Controls.Add(this.OpacityTrackBar);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(392, 238);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Interface";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Opacity:";
			// 
			// OpacityTrackBar
			// 
			this.OpacityTrackBar.AutoSize = false;
			this.OpacityTrackBar.Location = new System.Drawing.Point(16, 24);
			this.OpacityTrackBar.Maximum = 100;
			this.OpacityTrackBar.Minimum = 20;
			this.OpacityTrackBar.Name = "OpacityTrackBar";
			this.OpacityTrackBar.Size = new System.Drawing.Size(352, 16);
			this.OpacityTrackBar.TabIndex = 5;
			this.OpacityTrackBar.Value = 20;
			this.OpacityTrackBar.Scroll += new System.EventHandler(this.OpacityTrackBar_Scroll);
			// 
			// mcOptions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 267);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(408, 300);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(408, 300);
			this.Name = "mcOptions";
			this.Text = "Options...";
			this.Load += new System.EventHandler(this.mcOptions_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.OpacityTrackBar)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void OpacityTrackBar_Scroll(object sender, System.EventArgs e)
		{
			Obsidian.mainForm.Opacity = ((double)OpacityTrackBar.Value/100.0);

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.colorDialog1.FullOpen = true;
			this.colorDialog1.ShowDialog().ToString();
			MessageBox.Show(this.colorDialog1.Color.ToString());
		}

		private void mcOptions_Load(object sender, System.EventArgs e)
		{
			/*
			 * fucking ARSES...
			 * I'm not sure, but I think I found a FUCKING IRRITATING
			 * bug in the .net runtime-
			 * 
			 * If this is created as a normal form control
			 * (ie: this.mcPage1 = new mcPage(); in the
			 * constructor) then DESPITE what your brain
			 * would tell you, it USES THE WRONG FUCKING
			 * CONSTRUCTOR!
			 */
//			this.mcPage1 = new mcPage();
//			this.mcPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
//				| System.Windows.Forms.AnchorStyles.Left) 
//				| System.Windows.Forms.AnchorStyles.Right)));
//			this.mcPage1.Location = new System.Drawing.Point(5, 50);
//			this.mcPage1.Name = "mcPage1";
//			this.mcPage1.Size = new System.Drawing.Size(380, 160);
//			this.mcPage1.TabIndex = 6;
//			this.mcPage1.Tag = "ePAGE";
//			this.mcPage1.Topic = "";
//			this.mcPage1.Parent = this.tabPage2;
//
//			this.mcPage1.MessageAction("w00t", "flails about");
//			this.mcPage1.MessageDisplay("I am displaying :o");
//			this.mcPage1.MessageInfo("For your information :o");
//			this.mcPage1.MessageJoin("w00t", "viroteck@staff.chatspike.net");
//			this.mcPage1.MessageKick("w00t", "Craig", "sp0rk this");
//			this.mcPage1.MessageNotice("w00t", "a notice!");
//			this.mcPage1.MessagePart("w00t", "viroteck@mesh.inspircd.org", "Departing.");
//			this.mcPage1.MessagePrivate("Om", "I be messaging you :o");
//			this.mcPage1.MessageQuit("Brik", "Ping timeout");
//			this.mcPage1.MessageTopic("HAI FREN");
//			this.mcPage1.MessageTopicTime("Brain", "123456");
//			this.mcPage1.MessageUser("Peng", "[:]");
		}
	}
}

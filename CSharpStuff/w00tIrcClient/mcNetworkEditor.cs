using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Obsidian
{
	/// <summary>
	/// Summary description for mcNetworkEditor.
	/// </summary>
	public class mcNetworkEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.TextBox txtPerform;
		private System.Windows.Forms.CheckBox chkConnectOnStart;
		private System.Windows.Forms.TextBox txtNetworkName;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbServers;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.Button cmdDelete;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtNickname;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtRealname;
		private System.Windows.Forms.ComboBox cmbNetworks;
		private System.Windows.Forms.Button cmdConnect;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public mcNetworkEditor()
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtNetworkName = new System.Windows.Forms.TextBox();
			this.chkConnectOnStart = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtRealname = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.txtNickname = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtPerform = new System.Windows.Forms.TextBox();
			this.cmdSave = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.cmdDelete = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.cmbServers = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbNetworks = new System.Windows.Forms.ComboBox();
			this.cmdConnect = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtNetworkName);
			this.groupBox1.Controls.Add(this.chkConnectOnStart);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(0, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(224, 64);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection";
			// 
			// txtNetworkName
			// 
			this.txtNetworkName.Location = new System.Drawing.Point(96, 16);
			this.txtNetworkName.Name = "txtNetworkName";
			this.txtNetworkName.Size = new System.Drawing.Size(120, 20);
			this.txtNetworkName.TabIndex = 4;
			this.txtNetworkName.Text = "";
			// 
			// chkConnectOnStart
			// 
			this.chkConnectOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkConnectOnStart.Location = new System.Drawing.Point(8, 40);
			this.chkConnectOnStart.Name = "chkConnectOnStart";
			this.chkConnectOnStart.Size = new System.Drawing.Size(208, 16);
			this.chkConnectOnStart.TabIndex = 3;
			this.chkConnectOnStart.Text = "Connect On Startup:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Network Name:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtRealname);
			this.groupBox2.Controls.Add(this.txtUsername);
			this.groupBox2.Controls.Add(this.txtNickname);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Location = new System.Drawing.Point(0, 168);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(224, 96);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "My Settings";
			// 
			// txtRealname
			// 
			this.txtRealname.Location = new System.Drawing.Point(72, 64);
			this.txtRealname.Name = "txtRealname";
			this.txtRealname.Size = new System.Drawing.Size(144, 20);
			this.txtRealname.TabIndex = 5;
			this.txtRealname.Text = "";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(72, 40);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(144, 20);
			this.txtUsername.TabIndex = 4;
			this.txtUsername.Text = "";
			// 
			// txtNickname
			// 
			this.txtNickname.Location = new System.Drawing.Point(72, 16);
			this.txtNickname.Name = "txtNickname";
			this.txtNickname.Size = new System.Drawing.Size(144, 20);
			this.txtNickname.TabIndex = 3;
			this.txtNickname.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 66);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 2;
			this.label5.Text = "Realname:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 42);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 16);
			this.label4.TabIndex = 1;
			this.label4.Text = "Username:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 18);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Nickname:";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtPerform);
			this.groupBox3.Location = new System.Drawing.Point(232, 32);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(312, 232);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Perform";
			// 
			// txtPerform
			// 
			this.txtPerform.Location = new System.Drawing.Point(8, 16);
			this.txtPerform.Multiline = true;
			this.txtPerform.Name = "txtPerform";
			this.txtPerform.Size = new System.Drawing.Size(295, 208);
			this.txtPerform.TabIndex = 0;
			this.txtPerform.Text = "# Enter some commands to happen on startup.\r\n# Lines with a # at the start are co" +
				"mments\r\n# and are ignored.\r\n#\r\n# An example might be:\r\n#\r\n# nick blarp\r\n# msg ni" +
				"ckserv identify blarpspass\r\n# join #chatspike\r\n# msg loveserv radish w00t";
			// 
			// cmdSave
			// 
			this.cmdSave.Location = new System.Drawing.Point(8, 272);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Size = new System.Drawing.Size(128, 24);
			this.cmdSave.TabIndex = 3;
			this.cmdSave.Text = "Save";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.cmdDelete);
			this.groupBox4.Controls.Add(this.cmdAdd);
			this.groupBox4.Controls.Add(this.cmbServers);
			this.groupBox4.Controls.Add(this.label2);
			this.groupBox4.Location = new System.Drawing.Point(0, 96);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(224, 72);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Servers";
			// 
			// cmdDelete
			// 
			this.cmdDelete.Location = new System.Drawing.Point(120, 42);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Size = new System.Drawing.Size(96, 24);
			this.cmdDelete.TabIndex = 5;
			this.cmdDelete.Text = "Delete";
			// 
			// cmdAdd
			// 
			this.cmdAdd.Location = new System.Drawing.Point(8, 42);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(104, 24);
			this.cmdAdd.TabIndex = 4;
			this.cmdAdd.Text = "Add";
			// 
			// cmbServers
			// 
			this.cmbServers.Location = new System.Drawing.Point(88, 16);
			this.cmbServers.Name = "cmbServers";
			this.cmbServers.Size = new System.Drawing.Size(128, 21);
			this.cmbServers.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 18);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Server Address:";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(280, 272);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(128, 24);
			this.button2.TabIndex = 6;
			this.button2.Text = "Delete";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(144, 272);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(128, 24);
			this.button3.TabIndex = 7;
			this.button3.Text = "New";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 16);
			this.label6.TabIndex = 8;
			this.label6.Text = "Network to Edit:";
			// 
			// cmbNetworks
			// 
			this.cmbNetworks.Location = new System.Drawing.Point(88, 4);
			this.cmbNetworks.Name = "cmbNetworks";
			this.cmbNetworks.Size = new System.Drawing.Size(456, 21);
			this.cmbNetworks.TabIndex = 9;
			this.cmbNetworks.SelectedIndexChanged += new System.EventHandler(this.cmbNetworks_SelectedIndexChanged);
			// 
			// cmdConnect
			// 
			this.cmdConnect.Location = new System.Drawing.Point(416, 272);
			this.cmdConnect.Name = "cmdConnect";
			this.cmdConnect.Size = new System.Drawing.Size(128, 24);
			this.cmdConnect.TabIndex = 10;
			this.cmdConnect.Text = "Connect";
			this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
			// 
			// mcNetworkEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 301);
			this.Controls.Add(this.cmdConnect);
			this.Controls.Add(this.cmbNetworks);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.cmdSave);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(560, 332);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(560, 332);
			this.Name = "mcNetworkEditor";
			this.Text = "Network Editor";
			this.Load += new System.EventHandler(this.mcNetworkEditor_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void mcNetworkEditor_Load(object sender, System.EventArgs e)
		{
			foreach (string dir in System.IO.Directory.GetDirectories("networks"))
			{
				string[] namecomponents;
				if (System.IO.File.Exists(dir + "\\network.dat"))
				{
					/* we don't want "networks\blah" - only retrieve the last part. */
					namecomponents = dir.Split('\\');
					cmbNetworks.Items.Add(namecomponents[1]);
				}
			}
			if (cmbNetworks.Items.Count > 0)
			{
				cmbNetworks.Text = "Please select a network to edit.";
			}
			else
			{
				cmbNetworks.Text = "Welcome to the network editor. Please make your choice using the buttons below.";
			}
		}

		private void cmbNetworks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mcNetwork aNetwork;
			
			aNetwork = mcNetwork.GetNetwork(cmbNetworks.SelectedItem.ToString());

			if (aNetwork == null)
				return;

			cmbServers.Items.Clear();
			txtPerform.Text = null;


			txtNickname.Text = aNetwork.Nickname;
			txtUsername.Text = aNetwork.Username;
			txtRealname.Text = aNetwork.Realname;
			txtNetworkName.Text = cmbNetworks.SelectedItem.ToString();
			chkConnectOnStart.Checked = aNetwork.ConnectOnStartup;

			foreach (string str in aNetwork.Servers)
				cmbServers.Items.Add(str);

			if (aNetwork.Perform.Count > 0)
			{
				for (int i = 0; i < aNetwork.Perform.Count; i++)
				{
					txtPerform.Text = txtPerform.Text + "\r\n" + aNetwork.Perform[i];
				}

				/* remove extra /r/n at the start */
				txtPerform.Text = txtPerform.Text.Substring(2);
			}
		}

		private void cmdConnect_Click(object sender, System.EventArgs e)
		{
			string[] tmp;
			if (this.txtNickname.Text == null ||
				this.txtUsername.Text == null ||
				this.txtRealname.Text == null ||
				this.cmbServers.Items.Count == 0)
			{
				MessageBox.Show("Some information is missing. Please double check and try again.");
				return;
			}

			System.Random rnd = new System.Random();
			mcServer NewServer = Obsidian.mainForm.AddServer();
			NewServer.MyNickname = this.txtNickname.Text;
			NewServer.MyUsername = this.txtUsername.Text;
			NewServer.MyRealname = this.txtRealname.Text;
			/*
			 * pick a random server, they're stored as either
			 * servername, or servername:serverport - so split ;)
			 */
			tmp = this.cmbServers.Items[rnd.Next(cmbServers.Items.Count)].ToString().Split(':');
			NewServer.ServerName = tmp[0];
			if (tmp[1] != null)
				NewServer.ServerPort = System.Int32.Parse(tmp[1]);
			/* todo: remove unconnectable instances from the list? */
			NewServer.Connect();
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}

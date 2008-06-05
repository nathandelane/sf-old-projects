using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Obsidian
{
	/// <summary>
	/// Summary description for mcIRCForm.
	/// </summary>
	sealed public class mcMainForm	: System.Windows.Forms.Form
	{
		/* XXX - should probably be private. or something. */
		public mcPage CurrentPage;
		public System.Collections.SortedList Servers = new System.Collections.SortedList(5);
		private System.Windows.Forms.Splitter MySplitter;
		/* XXX - should probably be private. used for nickcomplete, etc. */
		public System.Windows.Forms.TreeView tvcWindows;
		private System.Windows.Forms.Timer tmrParseStuff;
		private System.Windows.Forms.ToolBar tbLauncher;
		private System.Windows.Forms.ToolBarButton tbbtnNewServer;
		private System.Windows.Forms.ImageList tbImages;
		private System.Windows.Forms.ToolBarButton tbbtnConnectDisconnect;
		private System.ComponentModel.IContainer components;

		public mcMainForm()
		{
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mcMainForm));
			this.MySplitter = new System.Windows.Forms.Splitter();
			this.tvcWindows = new System.Windows.Forms.TreeView();
			this.tmrParseStuff = new System.Windows.Forms.Timer(this.components);
			this.tbLauncher = new System.Windows.Forms.ToolBar();
			this.tbbtnNewServer = new System.Windows.Forms.ToolBarButton();
			this.tbbtnConnectDisconnect = new System.Windows.Forms.ToolBarButton();
			this.tbImages = new System.Windows.Forms.ImageList(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.networkEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MySplitter
			// 
			this.MySplitter.Location = new System.Drawing.Point(144, 24);
			this.MySplitter.Name = "MySplitter";
			this.MySplitter.Size = new System.Drawing.Size(3, 314);
			this.MySplitter.TabIndex = 5;
			this.MySplitter.TabStop = false;
			// 
			// tvcWindows
			// 
			this.tvcWindows.BackColor = System.Drawing.Color.Black;
			this.tvcWindows.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvcWindows.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tvcWindows.ForeColor = System.Drawing.Color.White;
			this.tvcWindows.ItemHeight = 16;
			this.tvcWindows.Location = new System.Drawing.Point(0, 24);
			this.tvcWindows.Name = "tvcWindows";
			this.tvcWindows.Size = new System.Drawing.Size(144, 314);
			this.tvcWindows.TabIndex = 4;
			this.tvcWindows.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvcWindows_AfterSelect);
			// 
			// tmrParseStuff
			// 
			this.tmrParseStuff.Enabled = true;
			this.tmrParseStuff.Tick += new System.EventHandler(this.tmrParseStuff_Tick);
			// 
			// tbLauncher
			// 
			this.tbLauncher.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tbLauncher.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
									this.tbbtnNewServer,
									this.tbbtnConnectDisconnect});
			this.tbLauncher.DropDownArrows = true;
			this.tbLauncher.ImageList = this.tbImages;
			this.tbLauncher.Location = new System.Drawing.Point(147, 24);
			this.tbLauncher.Name = "tbLauncher";
			this.tbLauncher.ShowToolTips = true;
			this.tbLauncher.Size = new System.Drawing.Size(501, 28);
			this.tbLauncher.TabIndex = 6;
			this.tbLauncher.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.tbLauncher.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbLauncher_ButtonClick);
			// 
			// tbbtnNewServer
			// 
			this.tbbtnNewServer.ImageIndex = 0;
			//this.tbbtnNewServer.Name = "tbbtnNewServer";
			this.tbbtnNewServer.Tag = "NEW_SERVER";
			this.tbbtnNewServer.ToolTipText = "Opens a new server window without disconnecting from the current one";
			// 
			// tbbtnConnectDisconnect
			// 
			this.tbbtnConnectDisconnect.ImageIndex = 1;
			//this.tbbtnConnectDisconnect.Name = "tbbtnConnectDisconnect";
			this.tbbtnConnectDisconnect.Tag = "CONNECT_DISCONNECT";
			this.tbbtnConnectDisconnect.ToolTipText = "Connects (or disconnects) the currently selected server instance";
			// 
			// tbImages
			// 
			this.tbImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbImages.ImageStream")));
			this.tbImages.TransparentColor = System.Drawing.Color.Transparent;
			this.tbImages.Images.SetKeyName(0, "");
			this.tbImages.Images.SetKeyName(1, "");
			this.tbImages.Images.SetKeyName(2, "");
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.viewToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(648, 24);
			this.menuStrip1.TabIndex = 7;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "&Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.networkEditorToolStripMenuItem,
									this.optionsToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// networkEditorToolStripMenuItem
			// 
			this.networkEditorToolStripMenuItem.Name = "networkEditorToolStripMenuItem";
			this.networkEditorToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.networkEditorToolStripMenuItem.Text = "&Network Editor";
			this.networkEditorToolStripMenuItem.Click += new System.EventHandler(this.NetworkEditorToolStripMenuItemClick);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// mcMainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(648, 338);
			this.Controls.Add(this.tbLauncher);
			this.Controls.Add(this.MySplitter);
			this.Controls.Add(this.tvcWindows);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "mcMainForm";
			this.Text = "Obsidian";
			this.Closed += new System.EventHandler(this.Exit);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem networkEditorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		#endregion

		public mcServer AddServer()
		{
			mcServer Server = new mcServer();
			System.Random Rnd = new Random();

			/*
			 * generate a random key.
			 * this should break out eventually.
			 */
			for (;;)
			{
				//100 chars should be sufficient entropy ;)...
				Server.HashKey = Server.HashKey + Rnd.Next(500000).ToString();
				if (!Servers.Contains(Server.HashKey))
				{
					//we have generated a unique hashkey
					Server.ServerPage.MyNode.Tag = Server.HashKey;
					TreeNode lvi = new	TreeNode("My Status");
					lvi.Tag = Server.HashKey;
					tvcWindows.Nodes.Add(lvi);
					Server.ServerPage.MyNode = lvi;

					lvi = new TreeNode("My Channels");
					lvi.Tag = Server.HashKey;
					Server.ServerPage.MyNode.Nodes.Add(lvi);
					Server.ServerPage.ChannelsNode = lvi;

					lvi = new TreeNode("My Messages");
					lvi.Tag = Server.HashKey;
					Server.ServerPage.MyNode.Nodes.Add(lvi);
					Server.ServerPage.MessagesNode = lvi;

					lvi = new TreeNode("My Buddies");
					lvi.Tag = Server.HashKey;
					Server.ServerPage.MyNode.Nodes.Add(lvi);
					Server.ServerPage.BuddiesNode = lvi;

					Servers.Add(Server.HashKey, Server);

					this.tvcWindows.ExpandAll();

					//fix: select this Server as active.
					this.tvcWindows_AfterSelect(this, new TreeViewEventArgs(Server.ServerPage.MyNode, TreeViewAction.ByMouse));
					return Server;
				}
			}
		}

		public void DeleteServer(mcServer Server)
		{
			Servers.Remove(Server.HashKey);
		}

		public void DeleteServer(string Key)
		{
			Servers.Remove(Key);
		}

		private void tvcWindows_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			mcPage aPage;
			mcServer aServer = (mcServer)Servers.GetByIndex(Servers.IndexOfKey(e.Node.Tag));

			/*
			 * Locate the server instance.
			 * aServer will never be null, as if the above fails
			 * the client will crash. This should be OK though
			 * s we should NOT be trying to select a non-existant window ;)
			 */

			/* find our page */
			aPage = aServer.FindPage(e.Node.Text);
			if (aPage == null)
			{
				/*
				 * all this generally means is that they selected, for example,
				 * a "messages" or "channels" node.
				 */
				aPage = aServer.CurrentPage;
			}
			/* now, we should have a page - focus on it */
			aPage.DoFocus();
		}

		public void Exit(object obj,System.EventArgs e)
		{
			foreach (mcServer aServer in Servers.Values)
			{
				aServer.Disconnect("Departing.");
			}
			Application.Exit();
		}

		private void tmrParseStuff_Tick(object sender, System.EventArgs e)
		{
			/* bit of an ugly way to handle things, but it works ;) */
			foreach (mcServer aServer in Servers.Values)
			{
				if (aServer.Connected)
				{
					if (aServer.ServerSocket.sck == null)
					{
						/* Remote server disconnected.*/
						aServer.Disconnect("Remote server closed socket.");
						/* fix: exception on remote server close */
						continue;
					}
					if (aServer.ServerSocket.Available() > 0)
					{
						//Data incoming...
						mcInbound.Parse(aServer.ServerSocket.GetData(), aServer.ServerPage);
					}
				}
			}
		}

		private void tbLauncher_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch (e.Button.Tag.ToString())
			{
				/*
				 * i'm aware that this is a totally FUGLY way of
				 * doing things - got any better ideas? :p
				 */
				case "NEW_SERVER":
					this.AddServer();
					break;
				case "CONNECT_DISCONNECT":
					if (this.CurrentPage.Server.Connected)
					{
						this.CurrentPage.Server.Disconnect("Departing.");
					}
					else
					{
						this.CurrentPage.Server.Connect();
					}
					break;
			}
		}
		
		void AboutToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			mcAbout AboutForm = new mcAbout();
			AboutForm.ShowDialog();
		}
		
		void NetworkEditorToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			mcNetworkEditor NetworkEditor = new mcNetworkEditor();
			NetworkEditor.ShowDialog();
		}
		
		void OptionsToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			mcOptions OptionsDialog = new mcOptions();
			OptionsDialog.ShowDialog();
		}
		
		void ExitToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			this.Exit(null, null);
		}
	}
}

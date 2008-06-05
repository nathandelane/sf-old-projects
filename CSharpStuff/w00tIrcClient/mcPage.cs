using System;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Obsidian
{
	/// <summary>
	/// Summary description for mcPage.
	/// </summary>
	sealed public class mcPage : System.Windows.Forms.UserControl
	{
		private string p_text;
		new public string Text
		{
			get
			{
				return p_text;
			}
			set
			{
				this.MyNode.Text = value;
				p_text = value;
			}
		}
		public string Topic
		{
			get
			{
				return txtTopic.Text;
			}
			set
			{
				this.txtTopic.Text = value;
			}
		}
		public bool IsChannel;
		public string HashKey; //how can i be identified?

		/* who owns me? */
		public mcServer Server;
		public TreeNode MyNode = new TreeNode();
		public TreeNode ChannelsNode = new TreeNode();
		public TreeNode MessagesNode = new TreeNode();
		public TreeNode BuddiesNode = new TreeNode();
		private System.Windows.Forms.Button cmdClosePage;
		private System.Windows.Forms.TextBox txtToSend;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RichTextBox txtData;
		private System.Windows.Forms.ContextMenu ctmNicklist;
		private System.Windows.Forms.MenuItem mnuNicklistWhois;
		private System.Windows.Forms.TextBox txtTopic;
		private System.Windows.Forms.Panel panel2;
		public System.Windows.Forms.ListBox lstUsers;
		private ArrayList History = new ArrayList();
		private int HistoryIndex;
		
		/*
		 * This is to hold any mode (beI on most IRCds) with a parameter - for example:
		 *  b w00t!*@*
		 *  e *!viroteck@*
		 * ..etc.
		 */
		sealed private class ChannelMode
		{
			private char mode;
			private string value;
			private bool _requires_param;	/* does it require a param to unset? */
			
			public bool requires_param
			{
				get
				{
					return this._requires_param;
				}
			}
			
			public ChannelMode(char mode, string value, bool requires_param)
			{
				this.mode = mode;
				this.value = value;
				this._requires_param = requires_param;
			}
			
			public bool MatchesMe(char mode, string value)
			{
				if (mode == this.mode)
					if (this.requires_param && value == this.value)
						return true;
				
				return false;
			}
		}
		
		/*
		 * Holds type 'ChannelModes'
		 */
		ArrayList ExtModes = new ArrayList();

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public mcPage(mcServer Server, string Title, bool IsChannel) 
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.Server = Server;
			this.Text = Title;

			if (!IsChannel)
			{
				this.txtData.Dock = DockStyle.Fill;
				this.panel1.Top = 0;
				this.panel1.Height = this.Height - this.txtToSend.Height;
				this.lstUsers.Visible = false;
				this.txtTopic.Visible = false;
				this.Topic = null;
			}


			this.Dock = System.Windows.Forms.DockStyle.Fill;

			if (Obsidian.mainForm != null)
				Obsidian.mainForm.Controls.Add(this);

			this.SetIndent(StringWidth(TimeStamp()+"<> ", this.txtData.Font));
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
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>

		private void InitializeComponent()
		{
			this.cmdClosePage = new System.Windows.Forms.Button();
			this.txtToSend = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtData = new System.Windows.Forms.RichTextBox();
			this.lstUsers = new System.Windows.Forms.ListBox();
			this.ctmNicklist = new System.Windows.Forms.ContextMenu();
			this.mnuNicklistWhois = new System.Windows.Forms.MenuItem();
			this.txtTopic = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdClosePage
			// 
			this.cmdClosePage.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdClosePage.Location = new System.Drawing.Point(512, 0);
			this.cmdClosePage.Name = "cmdClosePage";
			this.cmdClosePage.Size = new System.Drawing.Size(48, 24);
			this.cmdClosePage.TabIndex = 3;
			this.cmdClosePage.Text = "X";
			this.cmdClosePage.UseCompatibleTextRendering = true;
			this.cmdClosePage.Click += new System.EventHandler(this.cmdClosePage_Click);
			// 
			// txtToSend
			// 
			this.txtToSend.AcceptsTab = true;
			this.txtToSend.BackColor = System.Drawing.Color.Black;
			this.txtToSend.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtToSend.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtToSend.ForeColor = System.Drawing.Color.White;
			this.txtToSend.Location = new System.Drawing.Point(0, 0);
			this.txtToSend.Multiline = true;
			this.txtToSend.Name = "txtToSend";
			this.txtToSend.Size = new System.Drawing.Size(512, 24);
			this.txtToSend.TabIndex = 5;
			this.txtToSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToSend_KeyPress);
			this.txtToSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToSend_KeyDown);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtData);
			this.panel1.Controls.Add(this.lstUsers);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 21);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(560, 243);
			this.panel1.TabIndex = 9;
			// 
			// txtData
			// 
			this.txtData.AutoSize = true;
			this.txtData.BackColor = System.Drawing.Color.Black;
			this.txtData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtData.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtData.ForeColor = System.Drawing.Color.White;
			this.txtData.Location = new System.Drawing.Point(0, 0);
			this.txtData.MaxLength = 2048;
			this.txtData.Name = "txtData";
			this.txtData.ReadOnly = true;
			this.txtData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.txtData.Size = new System.Drawing.Size(440, 243);
			this.txtData.TabIndex = 99;
			this.txtData.Text = "";
			this.txtData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtData_MouseUp);
			this.txtData.TextChanged += new System.EventHandler(this.txtData_TextChanged);
			// 
			// lstUsers
			// 
			this.lstUsers.BackColor = System.Drawing.Color.Black;
			this.lstUsers.ContextMenu = this.ctmNicklist;
			this.lstUsers.Dock = System.Windows.Forms.DockStyle.Right;
			this.lstUsers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lstUsers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstUsers.ForeColor = System.Drawing.Color.White;
			this.lstUsers.IntegralHeight = false;
			this.lstUsers.Location = new System.Drawing.Point(440, 0);
			this.lstUsers.Name = "lstUsers";
			this.lstUsers.Size = new System.Drawing.Size(120, 243);
			this.lstUsers.TabIndex = 100;
			this.lstUsers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstUsers_DrawItem);
			this.lstUsers.DoubleClick += new System.EventHandler(this.lstUsers_DoubleClick);
			// 
			// ctmNicklist
			// 
			this.ctmNicklist.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
									this.mnuNicklistWhois});
			// 
			// mnuNicklistWhois
			// 
			this.mnuNicklistWhois.Index = 0;
			this.mnuNicklistWhois.Text = "&Whois";
			this.mnuNicklistWhois.Click += new System.EventHandler(this.mnuNicklistWhois_Click);
			// 
			// txtTopic
			// 
			this.txtTopic.BackColor = System.Drawing.Color.Black;
			this.txtTopic.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtTopic.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTopic.ForeColor = System.Drawing.Color.White;
			this.txtTopic.Location = new System.Drawing.Point(0, 0);
			this.txtTopic.Name = "txtTopic";
			this.txtTopic.Size = new System.Drawing.Size(560, 21);
			this.txtTopic.TabIndex = 11;
			this.txtTopic.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTopic_KeyPress);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.txtToSend);
			this.panel2.Controls.Add(this.cmdClosePage);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 264);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(560, 24);
			this.panel2.TabIndex = 12;
			// 
			// mcPage
			// 
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.txtTopic);
			this.Name = "mcPage";
			this.Size = new System.Drawing.Size(560, 288);
			this.Tag = "";
			this.TextChanged += new System.EventHandler(this.PageTextChanged);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion


		/* Actual code below here */

		/*
		 * GetMode()
		 *  Returns a channel mode with char `mode', and value `value', or null.
		 */
		private ChannelMode GetMode(char mode, string value)
		{
			foreach (ChannelMode moo in this.ExtModes)
			{
				if (moo.MatchesMe(mode, value))
					return moo;
			}
			
			return null;
		}
		
		/*
		 * RemoveMode()
		 *  Frees memory taken by a ChannelMode, and removes it from the list
		 *  Should only be called from the MODE parser.
		 */
		public void RemoveMode(char mode, string value)
		{
				this.ExtModes.Remove(this.GetMode(mode, value));
		}
		
		/*
		 * AddMode()
		 *  Creates a new ChannelMode structure, and adds it to the list.
		 *  Should only be called from the MODE parser.
		 */
		public void AddMode(char mode, string value, bool requires_param)
		{
			this.ExtModes.Add(new ChannelMode(mode, value, requires_param));
		}
		
		/*
		 *  avoid using .Append externally, I'm still
		 *  considering making it private.
		 */
		private delegate void delegateAppendText(string text);
		public bool Append(string data) 
		{
			if(txtData == null)
				return false;

			delegateAppendText d = txtData.AppendText;
			if (txtData.InvokeRequired)
			{
				txtData.Invoke(d, data);
			}
			else
			{
				txtData.AppendText(data);
			}

			if(!Server.CurrentPage.Equals(this))
				this.ColourNode(Color.Red);
			return true;
		}
		
		public bool Save(string file) 
		{
			if(txtData == null || file == null || file.Length < 2)
				return false;

			txtData.SaveFile(file);
			return true;
		}

		private delegate void setSelColor(System.Drawing.Color color);
		private bool SetColor(System.Drawing.Color color) 
		{
			if (txtData.InvokeRequired)
			{
				setSelColor d = delegate(System.Drawing.Color clr)
				{
					txtData.SelectionColor = clr;
				};
				txtData.Invoke(d, color);
			}
			else
			{
				txtData.SelectionColor = color;
			}
			return true;
		}

		private delegate void resetSelColor();
		private bool ResetColor() 
		{
			if (txtData.InvokeRequired)
			{
				resetSelColor d = delegate()
				{
					txtData.SelectionColor = System.Drawing.Color.White;
				};
				txtData.Invoke(d, new object[0] { });
			}
			else
			{
				txtData.SelectionColor = System.Drawing.Color.White;
			}
			return true;
		}

		private bool SetIndent(int pixels) 
		{
			if(pixels < 0 || pixels > txtData.Width)
				return false;
			txtData.SelectionHangingIndent = pixels;
			return true;
		}
		
		private void PageTextChanged(object sender, System.EventArgs e)
		{
			MyNode.Text = this.Text;
		}

		//public so windowlist can reset us.
		private void ScrollDown()
		{
			if (this != Obsidian.mainForm.CurrentPage)
				return;
			
			if (txtData != null && txtData.Text.Length > 0) 
			{
				txtData.SelectionStart = txtData.Text.Length - 1;
				txtData.Focus();
				Obsidian.mainForm.CurrentPage.txtToSend.Focus();
			}
		}

		private void txtData_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			System.Windows.Forms.Clipboard.SetDataObject(txtData.SelectedText, true);
			this.txtToSend.Focus();
		}

		private void txtData_TextChanged(object sender, System.EventArgs e)
		{
			if(txtData.TextLength < 1)
				return;
			if(txtData.Text[txtData.TextLength-1] == '\n')
				ScrollDown();
		}

		private void cmdClosePage_Click(object sender, System.EventArgs e)
		{
			if (this.Server.ServerPage.Equals(this))
			{
				/* todo: this needs to be configurable */
				this.Server.Disconnect("Departing.");
				Obsidian.mainForm.DeleteServer(this.Server);
				this.Server.CloseAllPages();
				this.Server.ServerPage.ClosePage();
				this.Server = null;
				return;
			}
			if (this.IsChannel)
				this.Server.IRCSend("PART " + this.Text);
			this.ClosePage();
		}
		public void ClosePage()
		{
			MyNode.Remove();
			Server.DeletePage(this);
		}

		private string[] DoNickComplete(string word) 
		{
			System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
			
			foreach (ChanUser cu in lstUsers.Items) 
			{
				if (cu.Nick.ToLower().StartsWith(word.ToLower())) 
				{
					sc.Add(cu.Nick);
				}
			}
			
			foreach (TreeNode tn in Obsidian.mainForm.tvcWindows.Nodes)
			{
				if (tn.Text.ToLower().StartsWith(word.ToLower()))
				{
					sc.Add(tn.Text);
				}
				
				/* fix: go through channel window nodes, query nodes, etc. */
				foreach (TreeNode tn2 in tn.Nodes)
				{
					foreach (TreeNode tn3 in tn2.Nodes)
					{
						if (tn3.Text.ToLower().StartsWith(word.ToLower()))
						{
							sc.Add(tn3.Text);
						}
					}
				}
			}
			
			string[] a = new string[sc.Count];
			sc.CopyTo(a, 0);
			return a;
		}

		private string GetWord(string line, int cursor, out int start, out int length) 
		{
			start = cursor;
			length = 0;
			while (start >= 1 && !char.IsWhiteSpace(line[start - 1])) 
			{
				start--;
			}
			length = cursor - start;
			while (length < (line.Length - start) && !char.IsWhiteSpace(line[start + length])) 
			{
				length++;
			}
			return line.Substring(start, length);
		}

		private void txtToSend_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (((int)e.KeyChar) == 13)
			{
				/* Enter was pressed-- send it! */
				string mycmd = txtToSend.Text;
				txtToSend.Text = null;
				mcCommands.MainParser(this, mycmd);
				
				History.Add(mycmd);
				if (History.Count > 50)
					History.RemoveAt(0);
				
				HistoryIndex = History.Count;
				e.Handled = true;
				return;
			}
			else if (((int)e.KeyChar) == 9) 
			{
				// Tab was pressed. Try nickcompletion.
				int start, len;
				string word = GetWord(txtToSend.Text, txtToSend.SelectionStart, out start, out len);
				string[] list = DoNickComplete(word);
				string tmpstring = null;
				if (list.Length == 1) 
				{
					txtToSend.SelectionStart = start;
					txtToSend.SelectionLength = len;
					txtToSend.SelectedText = list[0];
				}
				else if (list.Length > 1) 
				{
					for (int i = 0; i < list.Length; i++) 
					{
						if (tmpstring == null) 
						{
							tmpstring = list[i];
						}
						else 
						{
							while (!list[i].ToLower().StartsWith(tmpstring.ToLower())) 
							{
								tmpstring = tmpstring.Remove(tmpstring.Length - 1, 1);
							}
						}
					}
					if (tmpstring.Length > word.Length) 
					{
						txtToSend.SelectionStart = start;
						txtToSend.SelectionLength = len;
						txtToSend.SelectedText = tmpstring;
					}
					MessageInfo(String.Join(" ", list));
				}
				e.Handled = true;
			}			
		}
		
		private void txtToSend_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				if (HistoryIndex <= 0)
				{
					System.Media.SystemSounds.Beep.Play();
					e.Handled = true;
					return;
				}
				HistoryIndex--;
				txtToSend.Text = (string)History[HistoryIndex];
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				if (HistoryIndex >= History.Count)
				{
					System.Media.SystemSounds.Beep.Play();
					e.Handled = true;
					return;
				}
				HistoryIndex++;
				if (HistoryIndex < History.Count)
				{
					txtToSend.Text = (string)History[HistoryIndex];
				}
				else
				{
					txtToSend.Text = "";
				}
				e.Handled = true;
			}
		}

		private void txtTopic_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ((int)e.KeyChar == 13)
			{
				/* Enter was pressed-- change the topic */
				if (this.IsChannel)
				{
					this.Server.IRCSend("TOPIC " + this.Text + " :" + txtTopic.Text);
				}
				else
				{
					this.Topic = null;
				}
				return;
			}
		}

		private int StringWidth(string msg, Font font) 
		{
			Form aForm = new Form();
			Graphics g = aForm.CreateGraphics();
			SizeF size = new SizeF();
			size = g.MeasureString(msg, font);
			g.Dispose();
			aForm.Dispose();
			return (int)size.Width;
		}

		private string TimeStamp() 
		{
			System.DateTime timestamp = new System.DateTime();
			string timestr;

			timestamp = System.DateTime.Now;
			timestr = "[";
			timestr +=  (timestamp.Hour > 9)? timestamp.Hour.ToString() : "0"+timestamp.Hour;
			timestr += ":";
			timestr += (timestamp.Minute > 9)? timestamp.Minute.ToString() : "0"+timestamp.Minute;
			timestr += ":";
			timestr += (timestamp.Second > 9)? timestamp.Second.ToString() : "0"+timestamp.Second;
			timestr += "] ";
			return timestr;
		}

		public void MessageDisplay(string msg) 
		{
			if(msg != null && msg.Length > 0)
				this.Append(TimeStamp()+msg+"\r\n");
		}

		public bool MessageUser(string nick, string msg) 
		{
			this.Append(TimeStamp());
			this.SetColor(System.Drawing.Color.Blue);
			this.Append("<"); 
			this.ResetColor();
			this.Append(nick); 
			this.SetColor(System.Drawing.Color.Blue);
			this.Append("> "); 
			this.ResetColor();
			this.Append(msg+"\r\n");
			this.SetIndent(0);

			return true;
		}

		public bool MessagePrivate(string nick, string msg) 
		{
			this.Append(TimeStamp());
			this.SetColor(System.Drawing.Color.Green);
			this.Append(">"); 
			this.ResetColor();
			this.Append(nick); 
			this.SetColor(System.Drawing.Color.Green);
			this.Append("< "); 
			this.ResetColor();
			this.Append(msg+"\r\n"); 
	
			return true;
		}

		public bool MessageNotice(string nick, string msg) 
		{
			System.Drawing.Color pink = System.Drawing.Color.FromArgb(238, 34, 238);

			this.Append(TimeStamp()); 
			this.SetColor(System.Drawing.Color.Blue);
			this.Append("-"); 
			this.SetColor(pink);
			this.Append(nick); 
			this.SetColor(System.Drawing.Color.Blue);
			this.Append("- "); 
			this.ResetColor();
			this.Append(msg+"\r\n"); 
	
			return true;
		}

		public bool MessageAction(string nick, string msg) 
		{
			this.Append(TimeStamp());
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append("*");
			this.ResetColor();
			this.Append(" "+nick+msg+"\r\n");

			return true;
		}
		
		public bool MessageMode(string nick, string info, string modes)
		{
			this.Append(TimeStamp());
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append("*");
			this.ResetColor();
			this.Append(" " + nick + " (" + info + ") sets mode " + modes + "\r\n");

			return true;
		}

		public bool MessageInfo(string msg) 
		{
			this.Append(TimeStamp());
			this.Append("-");
			this.SetColor(System.Drawing.Color.DarkCyan);
			this.Append("-");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append("-");
			this.ResetColor();
			this.Append(" " + msg + "\r\n");

			return true;
		}

		public bool MessageJoin(string nick, string info) 
		{
			this.Append(TimeStamp());
			this.Append("-");
			this.SetColor(System.Drawing.Color.DarkCyan);
			this.Append("-");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append("> ");
			this.SetColor(System.Drawing.Color.White);
			this.Append(nick);
			this.SetColor(System.Drawing.Color.DarkGray);
			this.Append(" (");
			this.SetColor(System.Drawing.Color.DarkCyan);
			this.Append(info);
			this.SetColor(System.Drawing.Color.DarkGray);
			this.Append(")");
			this.ResetColor();
			this.Append(" has joined "+this.Text+"\r\n"); 


			if (nick == this.Server.MyNickname)
				return true;

			this.AddUserToChannel(nick, info);
			return true;
		}

		public bool MessagePart(string nick, string info, string reason) 
		{
			this.Append(TimeStamp());
			this.Append("<");
			this.SetColor(System.Drawing.Color.DarkCyan);
			this.Append("-");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append("- ");
			this.SetColor(System.Drawing.Color.White);
			this.Append(nick);
			this.SetColor(System.Drawing.Color.DarkGray);
			this.Append(" (");
			this.SetColor(System.Drawing.Color.DarkCyan);
			this.Append(info);
			this.SetColor(System.Drawing.Color.DarkGray);
			this.Append(")");
			this.ResetColor();
			this.Append(" has left "+this.Text); 
			
			if(reason == null || reason.Length < 1) 
			{
				this.Append("\r\n");
			} 
			else 
			{
				this.SetColor(System.Drawing.Color.DarkGray);
				this.Append(" (");
				this.ResetColor();
				this.Append(reason);
				this.SetColor(System.Drawing.Color.DarkGray);
				this.Append(")\r\n");
				this.ResetColor();
			}

			this.RemoveUserFromChannel(nick);
			return true;
		}

		public bool MessageKick(string target, string opnick, string reason) 
		{
			this.Append(TimeStamp());
			this.Append("<");
			this.SetColor(System.Drawing.Color.DarkCyan);
			this.Append("-");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append("- ");
			this.SetColor(System.Drawing.Color.White);
			this.Append(target);
			this.ResetColor();
			this.Append(" has been kicked from "+this.Text+" by "+opnick);

			if(reason == null || reason.Length < 1)
			{
				this.Append("\r\n");
			}
			else
			{
				this.SetColor(System.Drawing.Color.DarkGray);
				this.Append(" (");
				this.ResetColor();
				this.Append(reason);
				this.SetColor(System.Drawing.Color.DarkGray);
				this.Append(")\r\n");
				this.ResetColor();
			}

			this.RemoveUserFromChannel(target);
			return true;
		}

		public bool MessageQuit(string qnick, string reason) 
		{
			this.Append(TimeStamp());
			this.Append("<");
			this.SetColor(Color.DarkCyan);
			this.Append("-");
			this.SetColor(Color.Cyan);
			this.Append("- ");
			this.SetColor(Color.White);
			this.Append(qnick);
			this.ResetColor();
			this.Append(" has Quit");

			if(reason == null || reason.Length < 1) 
			{
				this.Append("\r\n");
			} 
			else 
			{
				this.SetColor(Color.DarkGray);
				this.Append(" (");
				this.ResetColor();
				this.Append(reason);
				this.SetColor(Color.DarkGray);
				this.Append(")\r\n");
				this.ResetColor();
			}
			return true;
		}

		public bool MessageTopic(string topic) 
		{
			this.txtTopic.Text = topic;
			if(this.Equals(this.Server.CurrentPage))
				this.Server.CurrentPage.txtTopic.Text = this.txtTopic.Text;

			this.Append(TimeStamp());
			this.Append("-");
			this.SetColor(System.Drawing.Color.DarkCyan);
			this.Append("-");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append("- ");
			this.ResetColor();
			this.Append("Topic for ");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append(this.Text);
			this.ResetColor();
			this.Append(" is ");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append(topic);
			this.ResetColor();
			this.Append("\r\n");

			return true;
		}

		public bool MessageTopicTime(string user, string rawtime) 
		{
			System.DateTime time = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(double.Parse(rawtime)).ToLocalTime();
			this.Append(TimeStamp());
			this.Append("-");
			this.SetColor(System.Drawing.Color.DarkCyan);
			this.Append("-");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append("- ");
			this.ResetColor();
			this.Append("Topic for ");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append(this.Text);
			this.ResetColor();
			this.Append(" set by ");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append(user);
			this.ResetColor();
			this.Append(" at ");
			this.SetColor(System.Drawing.Color.Cyan);
			this.Append(time.ToString("ddd, MMM d, yyyy HH:mm:ss"));
			this.Append("\r\n");
			
			return true;
		}

		private void mnuNicklistWhois_Click(object sender, System.EventArgs e)
		{
			if (this.lstUsers.SelectedItems.Count < 1) return;
			this.Server.IRCSend("WHOIS " + ((ChanUser)(this.lstUsers.SelectedItem)).Nick);
		}

		private void ColourNode(Color ink) 
		{
			MyNode.ForeColor = ink;
		}

		public void DoFocus()
		{
			this.BringToFront();
			MyNode.ForeColor = System.Drawing.Color.White;
			this.Server.CurrentPage = this;
			Obsidian.mainForm.CurrentPage = this;
			this.txtToSend.Focus();
		}
		
		/*
		 * Clear()
		 *  Clears txtData (where stuff gets shown.)
		 */
		public void Clear()
		{
			this.txtData.Clear();
		}

		public class ChanUser : IComparable
		{
			public string Nick = "";
			public string Info = "";
			public string Prefixes = "";
			public Color DispColor;
			public mcPage p;
			public ChanUser(string nick, mcPage p) 
			{
				this.Nick = nick;
				this.DispColor = Color.White;
				this.p = p;
			}
			#region IComparable Members

			public int CompareTo(object obj)
			{
				if (!(obj is ChanUser)) throw new ArgumentException("Can only compare with other ChanUser objects.", "obj");
				ChanUser cu = ((ChanUser)(obj));
				// Both have no prefixes. Do nick compare.
				if (this.Prefixes.Length == 0 && cu.Prefixes.Length == 0) return Nick.CompareTo(cu.Nick);
				// They have a prefix. We don't. Put us > them so that we come after in an ascending sort.
				if (this.Prefixes.Length == 0 && cu.Prefixes.Length > 0) return 1;
				// We have a prefix. They don't. Us < them so we come before in an ascending sort.
				if (this.Prefixes.Length > 0 && cu.Prefixes.Length == 0) return -1;
				// Both have a prefix so we must compare the "highest" (first) prefix.
				char myhighprefix = Prefixes[0], hishighprefix = cu.Prefixes[0];
				// Highest prefixes are equal - compare nicks.
				if (myhighprefix == hishighprefix) 
				{
					return Nick.CompareTo(cu.Nick);
				}
				// If for some reason an invalid first prefix is involved, put the invalid prefixes after
				// all valid prefixes.
				// Here we're invalid, they aren't. We come after. We > them.
				if (p.Server.ISupport.PREFIX_Characters.IndexOf(myhighprefix) < 0 && p.Server.ISupport.PREFIX_Characters.IndexOf(hishighprefix) >= 0) 
				{
					return 1;
				}
				// They're invalid, we aren't. We come before. We < them.
				if (p.Server.ISupport.PREFIX_Characters.IndexOf(myhighprefix) >= 0 && p.Server.ISupport.PREFIX_Characters.IndexOf(hishighprefix) < 0) 
				{
					return -1;
				}
				// Both prefixes invalid, so do a flat ASCII compare.
				if (p.Server.ISupport.PREFIX_Characters.IndexOf(myhighprefix) < 0 && p.Server.ISupport.PREFIX_Characters.IndexOf(hishighprefix) < 0) 
				{
					return myhighprefix.CompareTo(hishighprefix);
				}
				int compare;
				// Compare the index of the prefixes within the prefix part of CHANMODES which will be
				// ordered in "highest level" prefix to "lowest level" (eg owner/op first, voice last).
				compare = p.Server.ISupport.PREFIX_Characters.IndexOf(myhighprefix).CompareTo(p.Server.ISupport.PREFIX_Characters.IndexOf(hishighprefix));
				// If his index is higher, we actually have the higher prefix. (PREFIX= symbols are ordered
				// high-to-low.) However, when we have the higher prefix, we come first, so we < them.
				if (compare != 0) return compare * 1;
					// Indexes are equal - shouldn't happen unless the prefixes themselves are equal and
					// that's handled above. However, I'm paranoid, and therefore am leaving this here.
				else return Nick.CompareTo(cu.Nick);
			}

			#endregion
		}

		public void AddUserToChannel(ChanUser cu) 
		{
			try 
			{
				for (int i = 0; i < this.lstUsers.Items.Count; i++) 
				{
					// For an ascending sort, we keep going until we find something we aren't >, 
					// then insert ourselves there.
					// We then just return. The finally block will trip to refresh the listbox before the
					// return kicks us all the way out.
					if (cu.CompareTo(lstUsers.Items[i]) < 1) 
					{
						// cu is less than or equal to lstUsers.Items[i]. That's where we insert.
						lstUsers.Items.Insert(i, cu);
						return; // Now get the heck out of this proc.
					}
				}
				this.lstUsers.Items.Add(cu);
			}
				// A fun hack to get this Refresh to happen come hell or high water.
				// Hint: finally blocks run when code leaves a try or catch block in ANY WAY OR FORM.
				// Whether by hitting the }, throwing up, breaking, or even return.
			finally 
			{
				this.lstUsers.Refresh();
			}
		}

		public void AddUserToChannel(string nick, string info)
		{
			if (this.GetUserOnChannelByNick(nick) != null)
				return;

			ChanUser cu = new ChanUser(nick, this);
			cu.Info = info;
			AddUserToChannel(cu);
		}

		public void RemoveUserFromChannel(ChanUser cu) 
		{
			if (cu != null) 
			{
				this.lstUsers.Items.Remove(cu);
				this.lstUsers.Refresh();
			}
		}

		public void RemoveUserFromChannel(string nick)
		{
			ChanUser cu = GetUserOnChannelByNick(nick);
			RemoveUserFromChannel(cu);
		}

		public void RemoveAllUsersFromChannel()
		{
			this.lstUsers.Items.Clear();
			this.lstUsers.Refresh();
		}
		
		public ChanUser GetUserOnChannelByNick(string nick)
		{
			foreach (ChanUser cu in this.lstUsers.Items)
			{
				if (cu.Nick == nick)
					return cu;
			}

			return null;
		}

		public void AddPrefix(string nick, char prefix) 
		{
			ChanUser cu = GetUserOnChannelByNick(nick);
			if (cu == null) return;
			RemoveUserFromChannel(cu);
			try 
			{
				char[] stmp = Server.ISupport.PREFIX_Characters.ToCharArray();
				for (int i = 0; i < stmp.Length; i++) 
				{
					if (stmp[i] != prefix && cu.Prefixes.IndexOf(stmp[i]) < 0)
						stmp[i] = ' ';
				}
				cu.Prefixes = (new string(stmp)).Replace(" ", "");
			}
			finally 
			{
				// No matter what put cu back where it came from if possible.
				AddUserToChannel(cu);
				this.lstUsers.Refresh();
			}
		}

		public void RemovePrefix(string nick, char prefix)
		{
			ChanUser cu = GetUserOnChannelByNick(nick);
			if (cu == null) return;
			RemoveUserFromChannel(cu);
			try 
			{
				cu.Prefixes = cu.Prefixes.Replace(prefix.ToString(), "");
			}
			finally 
			{
				AddUserToChannel(cu);
				this.lstUsers.Refresh();
			}
		}

		private void lstUsers_DrawItem(object sender, DrawItemEventArgs e)
		{
			ChanUser cu;
			if (e.Index >= 0) 
			{
				cu = ((ChanUser)(this.lstUsers.Items[e.Index]));
				e.DrawBackground();
				e.DrawFocusRectangle();
				if ((e.State & DrawItemState.Selected) != 0) 
				{
					e.Graphics.DrawString(cu.Prefixes + cu.Nick, lstUsers.Font, SystemBrushes.HighlightText, new RectangleF((float)e.Bounds.X, (float)e.Bounds.Y, (float)e.Bounds.Width, (float)e.Bounds.Height));
				}
				else 
				{
					e.Graphics.DrawString(cu.Prefixes + cu.Nick, lstUsers.Font, new SolidBrush(cu.DispColor), new RectangleF((float)e.Bounds.X, (float)e.Bounds.Y, (float)e.Bounds.Width, (float)e.Bounds.Height));
				}
			}
		}

		private void lstUsers_DoubleClick(object sender, System.EventArgs e)
		{
			mcCommands.MainParser(this, "/query " + ((ChanUser)lstUsers.SelectedItem).Nick);
		}
	}
}

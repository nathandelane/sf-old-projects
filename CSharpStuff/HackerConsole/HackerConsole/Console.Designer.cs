namespace HackerConsole
{
	partial class Console
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
			this._menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._contentPanel = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this._topPanel = new System.Windows.Forms.Panel();
			this._bottomPanel = new System.Windows.Forms.Panel();
			this._rightPanel = new System.Windows.Forms.Panel();
			this._consoleTabControl = new System.Windows.Forms.TabControl();
			this._consoleTab0 = new System.Windows.Forms.TabPage();
			this._toolsTabControl = new System.Windows.Forms.TabControl();
			this._analyzerTab0 = new System.Windows.Forms.TabPage();
			this._consoleTab0InnerPanel = new System.Windows.Forms.Panel();
			this._analyzerTab0InnerPanel = new System.Windows.Forms.Panel();
			this._menuStrip.SuspendLayout();
			this._contentPanel.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this._topPanel.SuspendLayout();
			this._bottomPanel.SuspendLayout();
			this._consoleTabControl.SuspendLayout();
			this._consoleTab0.SuspendLayout();
			this._toolsTabControl.SuspendLayout();
			this._analyzerTab0.SuspendLayout();
			this.SuspendLayout();
			// 
			// _menuStrip
			// 
			this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
			this._menuStrip.Location = new System.Drawing.Point(0, 0);
			this._menuStrip.Name = "_menuStrip";
			this._menuStrip.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
			this._menuStrip.Size = new System.Drawing.Size(1008, 25);
			this._menuStrip.TabIndex = 0;
			this._menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitConsole);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 19);
			this.settingsToolStripMenuItem.Text = "Settings";
			// 
			// preferencesToolStripMenuItem
			// 
			this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
			this.preferencesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
			this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.preferencesToolStripMenuItem.Text = "Preferences...";
			// 
			// _contentPanel
			// 
			this._contentPanel.AutoSize = true;
			this._contentPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this._contentPanel.Controls.Add(this.splitContainer1);
			this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._contentPanel.Location = new System.Drawing.Point(0, 25);
			this._contentPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._contentPanel.Name = "_contentPanel";
			this._contentPanel.Size = new System.Drawing.Size(1008, 707);
			this._contentPanel.TabIndex = 1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._rightPanel);
			this.splitContainer1.Size = new System.Drawing.Size(1008, 707);
			this.splitContainer1.SplitterDistance = 716;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this._topPanel);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this._bottomPanel);
			this.splitContainer2.Size = new System.Drawing.Size(716, 707);
			this.splitContainer2.SplitterDistance = 486;
			this.splitContainer2.SplitterWidth = 6;
			this.splitContainer2.TabIndex = 0;
			// 
			// _topPanel
			// 
			this._topPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._topPanel.Controls.Add(this._consoleTabControl);
			this._topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._topPanel.Location = new System.Drawing.Point(0, 0);
			this._topPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._topPanel.Name = "_topPanel";
			this._topPanel.Size = new System.Drawing.Size(716, 486);
			this._topPanel.TabIndex = 0;
			// 
			// _bottomPanel
			// 
			this._bottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._bottomPanel.Controls.Add(this._toolsTabControl);
			this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._bottomPanel.Location = new System.Drawing.Point(0, 0);
			this._bottomPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._bottomPanel.Name = "_bottomPanel";
			this._bottomPanel.Size = new System.Drawing.Size(716, 215);
			this._bottomPanel.TabIndex = 0;
			// 
			// _rightPanel
			// 
			this._rightPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._rightPanel.Location = new System.Drawing.Point(0, 0);
			this._rightPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._rightPanel.Name = "_rightPanel";
			this._rightPanel.Size = new System.Drawing.Size(286, 707);
			this._rightPanel.TabIndex = 0;
			// 
			// _consoleTabControl
			// 
			this._consoleTabControl.Controls.Add(this._consoleTab0);
			this._consoleTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._consoleTabControl.Location = new System.Drawing.Point(0, 0);
			this._consoleTabControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._consoleTabControl.Name = "_consoleTabControl";
			this._consoleTabControl.SelectedIndex = 0;
			this._consoleTabControl.Size = new System.Drawing.Size(714, 484);
			this._consoleTabControl.TabIndex = 0;
			// 
			// _consoleTab0
			// 
			this._consoleTab0.Controls.Add(this._consoleTab0InnerPanel);
			this._consoleTab0.Location = new System.Drawing.Point(4, 21);
			this._consoleTab0.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._consoleTab0.Name = "_consoleTab0";
			this._consoleTab0.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._consoleTab0.Size = new System.Drawing.Size(706, 459);
			this._consoleTab0.TabIndex = 0;
			this._consoleTab0.Text = "Console";
			this._consoleTab0.UseVisualStyleBackColor = true;
			// 
			// _toolsTabControl
			// 
			this._toolsTabControl.Controls.Add(this._analyzerTab0);
			this._toolsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._toolsTabControl.Location = new System.Drawing.Point(0, 0);
			this._toolsTabControl.Name = "_toolsTabControl";
			this._toolsTabControl.SelectedIndex = 0;
			this._toolsTabControl.Size = new System.Drawing.Size(714, 213);
			this._toolsTabControl.TabIndex = 0;
			// 
			// _analyzerTab0
			// 
			this._analyzerTab0.Controls.Add(this._analyzerTab0InnerPanel);
			this._analyzerTab0.Location = new System.Drawing.Point(4, 21);
			this._analyzerTab0.Name = "_analyzerTab0";
			this._analyzerTab0.Padding = new System.Windows.Forms.Padding(3);
			this._analyzerTab0.Size = new System.Drawing.Size(706, 188);
			this._analyzerTab0.TabIndex = 0;
			this._analyzerTab0.Text = "Analyzer";
			this._analyzerTab0.UseVisualStyleBackColor = true;
			// 
			// _consoleTab0InnerPanel
			// 
			this._consoleTab0InnerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._consoleTab0InnerPanel.Location = new System.Drawing.Point(4, 5);
			this._consoleTab0InnerPanel.Name = "_consoleTab0InnerPanel";
			this._consoleTab0InnerPanel.Size = new System.Drawing.Size(698, 449);
			this._consoleTab0InnerPanel.TabIndex = 0;
			// 
			// _analyzerTab0InnerPanel
			// 
			this._analyzerTab0InnerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._analyzerTab0InnerPanel.Location = new System.Drawing.Point(3, 3);
			this._analyzerTab0InnerPanel.Name = "_analyzerTab0InnerPanel";
			this._analyzerTab0InnerPanel.Size = new System.Drawing.Size(700, 182);
			this._analyzerTab0InnerPanel.TabIndex = 0;
			// 
			// Console
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 732);
			this.Controls.Add(this._contentPanel);
			this.Controls.Add(this._menuStrip);
			this.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MainMenuStrip = this._menuStrip;
			this.Name = "Console";
			this.Text = "Hackers Console";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyPress);
			this._menuStrip.ResumeLayout(false);
			this._menuStrip.PerformLayout();
			this._contentPanel.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this._topPanel.ResumeLayout(false);
			this._bottomPanel.ResumeLayout(false);
			this._consoleTabControl.ResumeLayout(false);
			this._consoleTab0.ResumeLayout(false);
			this._toolsTabControl.ResumeLayout(false);
			this._analyzerTab0.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip _menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
		private System.Windows.Forms.Panel _contentPanel;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Panel _topPanel;
		private System.Windows.Forms.Panel _bottomPanel;
		private System.Windows.Forms.Panel _rightPanel;
		private System.Windows.Forms.TabControl _consoleTabControl;
		private System.Windows.Forms.TabPage _consoleTab0;
		private System.Windows.Forms.TabControl _toolsTabControl;
		private System.Windows.Forms.TabPage _analyzerTab0;
		private System.Windows.Forms.Panel _consoleTab0InnerPanel;
		private System.Windows.Forms.Panel _analyzerTab0InnerPanel;
	}
}


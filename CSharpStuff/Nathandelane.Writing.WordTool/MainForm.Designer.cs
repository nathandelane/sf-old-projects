namespace Nathandelane.Writing.WordTool
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this._wordFileTextBox = new System.Windows.Forms.TextBox();
            this._openWordFileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._queryTextBox = new System.Windows.Forms.TextBox();
            this._resultsLabel = new System.Windows.Forms.Label();
            this._resultsListBox = new System.Windows.Forms.ListBox();
            this._runQueryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Word File:";
            // 
            // _wordFileTextBox
            // 
            this._wordFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._wordFileTextBox.Enabled = false;
            this._wordFileTextBox.Location = new System.Drawing.Point(80, 12);
            this._wordFileTextBox.Name = "_wordFileTextBox";
            this._wordFileTextBox.Size = new System.Drawing.Size(662, 20);
            this._wordFileTextBox.TabIndex = 1;
            // 
            // _openWordFileButton
            // 
            this._openWordFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._openWordFileButton.Location = new System.Drawing.Point(748, 10);
            this._openWordFileButton.Name = "_openWordFileButton";
            this._openWordFileButton.Size = new System.Drawing.Size(115, 23);
            this._openWordFileButton.TabIndex = 2;
            this._openWordFileButton.Text = "Open Word File...";
            this._openWordFileButton.UseVisualStyleBackColor = true;
            this._openWordFileButton.Click += new System.EventHandler(this.OpenWordFile);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Query:";
            // 
            // _queryTextBox
            // 
            this._queryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._queryTextBox.Location = new System.Drawing.Point(12, 58);
            this._queryTextBox.Multiline = true;
            this._queryTextBox.Name = "_queryTextBox";
            this._queryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._queryTextBox.Size = new System.Drawing.Size(850, 118);
            this._queryTextBox.TabIndex = 4;
            // 
            // _resultsLabel
            // 
            this._resultsLabel.AutoSize = true;
            this._resultsLabel.Location = new System.Drawing.Point(23, 196);
            this._resultsLabel.Name = "_resultsLabel";
            this._resultsLabel.Size = new System.Drawing.Size(51, 15);
            this._resultsLabel.TabIndex = 5;
            this._resultsLabel.Text = "Results:";
            // 
            // _resultsListBox
            // 
            this._resultsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._resultsListBox.FormattingEnabled = true;
            this._resultsListBox.Location = new System.Drawing.Point(12, 214);
            this._resultsListBox.Name = "_resultsListBox";
            this._resultsListBox.Size = new System.Drawing.Size(850, 160);
            this._resultsListBox.TabIndex = 6;
            // 
            // _runQueryButton
            // 
            this._runQueryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._runQueryButton.Location = new System.Drawing.Point(787, 185);
            this._runQueryButton.Name = "_runQueryButton";
            this._runQueryButton.Size = new System.Drawing.Size(75, 23);
            this._runQueryButton.TabIndex = 7;
            this._runQueryButton.Text = "Run Query";
            this._runQueryButton.UseVisualStyleBackColor = true;
            this._runQueryButton.Click += new System.EventHandler(this.RunQuery);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 384);
            this.Controls.Add(this._runQueryButton);
            this.Controls.Add(this._resultsListBox);
            this.Controls.Add(this._resultsLabel);
            this.Controls.Add(this._queryTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._openWordFileButton);
            this.Controls.Add(this._wordFileTextBox);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(890, 420);
            this.Name = "MainForm";
            this.Text = "Word Tool by Nathan Lane";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _wordFileTextBox;
        private System.Windows.Forms.Button _openWordFileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _queryTextBox;
        private System.Windows.Forms.Label _resultsLabel;
        private System.Windows.Forms.ListBox _resultsListBox;
        private System.Windows.Forms.Button _runQueryButton;
    }
}


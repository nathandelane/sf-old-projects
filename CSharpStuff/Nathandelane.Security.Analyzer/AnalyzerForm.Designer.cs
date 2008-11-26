namespace Nathandelane.Security.Analyzer
{
	partial class AnalyzerForm
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
			this.cryptPlainTextBox = new System.Windows.Forms.TextBox();
			this.resultsListBox = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.analyzeButton = new System.Windows.Forms.Button();
			this.clearButton = new System.Windows.Forms.Button();
			this.characterRadioButton = new System.Windows.Forms.RadioButton();
			this.wordRadioButton = new System.Windows.Forms.RadioButton();
			this.delimiterComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(164, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Crypt/Plain Text To Be Analyzed:";
			// 
			// cryptPlainTextBox
			// 
			this.cryptPlainTextBox.Location = new System.Drawing.Point(12, 25);
			this.cryptPlainTextBox.Multiline = true;
			this.cryptPlainTextBox.Name = "cryptPlainTextBox";
			this.cryptPlainTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.cryptPlainTextBox.Size = new System.Drawing.Size(888, 200);
			this.cryptPlainTextBox.TabIndex = 1;
			this.cryptPlainTextBox.TextChanged += new System.EventHandler(this.PopulateDelimiterComboBox);
			// 
			// resultsListBox
			// 
			this.resultsListBox.Font = new System.Drawing.Font("Lucida Console", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.resultsListBox.FormattingEnabled = true;
			this.resultsListBox.Location = new System.Drawing.Point(12, 280);
			this.resultsListBox.Name = "resultsListBox";
			this.resultsListBox.Size = new System.Drawing.Size(888, 121);
			this.resultsListBox.Sorted = true;
			this.resultsListBox.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 264);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Results:";
			// 
			// analyzeButton
			// 
			this.analyzeButton.Location = new System.Drawing.Point(825, 231);
			this.analyzeButton.Name = "analyzeButton";
			this.analyzeButton.Size = new System.Drawing.Size(75, 23);
			this.analyzeButton.TabIndex = 4;
			this.analyzeButton.Text = "Analyze";
			this.analyzeButton.UseVisualStyleBackColor = true;
			this.analyzeButton.Click += new System.EventHandler(this.AnalyzeText);
			// 
			// clearButton
			// 
			this.clearButton.Location = new System.Drawing.Point(744, 231);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(75, 23);
			this.clearButton.TabIndex = 5;
			this.clearButton.Text = "Clear";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.ClearForm);
			// 
			// characterRadioButton
			// 
			this.characterRadioButton.AutoSize = true;
			this.characterRadioButton.Checked = true;
			this.characterRadioButton.Location = new System.Drawing.Point(12, 234);
			this.characterRadioButton.Name = "characterRadioButton";
			this.characterRadioButton.Size = new System.Drawing.Size(71, 17);
			this.characterRadioButton.TabIndex = 6;
			this.characterRadioButton.TabStop = true;
			this.characterRadioButton.Text = "Character";
			this.characterRadioButton.UseVisualStyleBackColor = true;
			// 
			// wordRadioButton
			// 
			this.wordRadioButton.AutoSize = true;
			this.wordRadioButton.Location = new System.Drawing.Point(89, 234);
			this.wordRadioButton.Name = "wordRadioButton";
			this.wordRadioButton.Size = new System.Drawing.Size(51, 17);
			this.wordRadioButton.TabIndex = 7;
			this.wordRadioButton.Text = "Word";
			this.wordRadioButton.UseVisualStyleBackColor = true;
			this.wordRadioButton.CheckedChanged += new System.EventHandler(this.EnableOrDisableComboBox);
			// 
			// delimiterComboBox
			// 
			this.delimiterComboBox.Enabled = false;
			this.delimiterComboBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.delimiterComboBox.FormattingEnabled = true;
			this.delimiterComboBox.Location = new System.Drawing.Point(146, 233);
			this.delimiterComboBox.Name = "delimiterComboBox";
			this.delimiterComboBox.Size = new System.Drawing.Size(113, 19);
			this.delimiterComboBox.Sorted = true;
			this.delimiterComboBox.TabIndex = 8;
			// 
			// AnalyzerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(912, 416);
			this.Controls.Add(this.delimiterComboBox);
			this.Controls.Add(this.wordRadioButton);
			this.Controls.Add(this.characterRadioButton);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.analyzeButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.resultsListBox);
			this.Controls.Add(this.cryptPlainTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AnalyzerForm";
			this.Text = "Nathandelane\'s Analyzer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox cryptPlainTextBox;
		private System.Windows.Forms.ListBox resultsListBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button analyzeButton;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.RadioButton characterRadioButton;
		private System.Windows.Forms.RadioButton wordRadioButton;
		private System.Windows.Forms.ComboBox delimiterComboBox;
	}
}


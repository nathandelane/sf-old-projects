namespace Nathandelane.Security.Rpg
{
	partial class RpgForm
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
			this._passwordTextField = new System.Windows.Forms.TextBox();
			this._regenerateButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _passwordTextField
			// 
			this._passwordTextField.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._passwordTextField.Location = new System.Drawing.Point(12, 12);
			this._passwordTextField.Name = "_passwordTextField";
			this._passwordTextField.ReadOnly = true;
			this._passwordTextField.Size = new System.Drawing.Size(216, 22);
			this._passwordTextField.TabIndex = 0;
			this._passwordTextField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// _regenerateButton
			// 
			this._regenerateButton.Location = new System.Drawing.Point(84, 40);
			this._regenerateButton.Name = "_regenerateButton";
			this._regenerateButton.Size = new System.Drawing.Size(75, 23);
			this._regenerateButton.TabIndex = 1;
			this._regenerateButton.Text = "Regenerate";
			this._regenerateButton.UseVisualStyleBackColor = true;
			this._regenerateButton.Click += new System.EventHandler(this.RegeneratePassword);
			// 
			// RpgForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(240, 70);
			this.Controls.Add(this._regenerateButton);
			this.Controls.Add(this._passwordTextField);
			this.Name = "RpgForm";
			this.Text = "Rpg.Net";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _passwordTextField;
		private System.Windows.Forms.Button _regenerateButton;
	}
}


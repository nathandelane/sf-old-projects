using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.IO.Console
{
	public partial class NathandelaneConsoleForm : Form
	{
		private Settings _settings;
		private int _currentColumnIndex;

		public NathandelaneConsoleForm()
		{
			_settings = new Settings();
			_currentColumnIndex = 0;

			InitializeComponent();
			SetColors();
			ShowPrompt();
		}

		private void SetColors()
		{
			int foreRed, foreGrn, foreBlu;
			int backRed, backGrn, backBlu;

			try
			{
				foreRed = int.Parse(_settings["foregroundColor"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0]);
				foreGrn = int.Parse(_settings["foregroundColor"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[1]);
				foreBlu = int.Parse(_settings["foregroundColor"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[2]);
				backRed = int.Parse(_settings["backgroundColor"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0]);
				backGrn = int.Parse(_settings["backgroundColor"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[1]);
				backBlu = int.Parse(_settings["backgroundColor"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[2]);
			}
			catch (Exception)
			{
				foreRed = 192;
				foreGrn = 192;
				foreBlu = 0;
				backRed = 0;
				backGrn = 0;
				backBlu = 0;
			}

			commandTextBox.ForeColor = Color.FromArgb(foreRed, foreGrn, foreBlu);
			commandTextBox.BackColor = Color.FromArgb(backRed, backGrn, backBlu);
		}

		private void ShowPrompt()
		{
			string prompt = Prompt.Parse(_settings["prompt"]);

			if (commandTextBox.Text.Equals(String.Empty))
			{
				commandTextBox.AppendText(prompt);
			}
			else
			{
				commandTextBox.AppendText(String.Format("{0}{1}{2}", Environment.NewLine, Environment.NewLine, prompt));
			}
		}

		private void CaptureKeys(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				_currentColumnIndex = 0;
				ShowPrompt();
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Left)
			{
				if (_currentColumnIndex > 0)
				{
					_currentColumnIndex--;
				}
				else
				{
					e.SuppressKeyPress = true;
				}
			}
			else
			{
				_currentColumnIndex++;
			}
		}
	}
}

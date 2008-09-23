using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Nathandelane.Security.Rpg
{
	public partial class RpgForm : Form
	{
		private char[] _possibleChars;
		private int _passwordLength;

		public RpgForm(string[] arguments)
		{
			Regex isDigit = new Regex("[\\d]+");

			if (arguments.Length == 0)
			{
				AppSettings settings = new AppSettings();
				_passwordLength = int.Parse(settings["passwordLength"]);
				_possibleChars = settings["characterSet"].ToCharArray();
			}
			else if (arguments.Length == 1 && isDigit.IsMatch(arguments[0]))
			{
				_passwordLength = int.Parse(arguments[0]);
				AppSettings settings = new AppSettings();
				_possibleChars = settings["characterSet"].ToCharArray();
			}
			else
			{
				_passwordLength = int.Parse(arguments[0]);
				_possibleChars = arguments[1].ToCharArray();
			}

			InitializeComponent();

			GeneratePassword();
		}

		private void GeneratePassword()
		{
			string newPassword = String.Empty;
			char lastChar = '\0';
			int maxChar = _possibleChars.Length;
			Random rand = new Random((int)DateTime.Now.Ticks);

			for (int i = 0; i < _passwordLength; i++)
			{
				char nextChar = _possibleChars[rand.Next(maxChar)];

				if (nextChar != lastChar && !newPassword.Contains(new String(nextChar, 1)))
				{
					newPassword += new String(nextChar, 1);
					lastChar = nextChar;
				}
				else
				{
					i--;
				}
			}

			_passwordTextField.Text = newPassword;
		}

		private void RegeneratePassword(object sender, EventArgs e)
		{
			GeneratePassword();
		}
	}
}
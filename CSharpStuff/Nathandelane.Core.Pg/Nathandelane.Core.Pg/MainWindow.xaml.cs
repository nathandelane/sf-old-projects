using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nathandelane.Core.Pg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private void GeneratePassword(object sender, RoutedEventArgs e)
		{
			if (!String.IsNullOrEmpty(_charactersTextBox.Text) && ((bool)_reuseCharsTrue.IsChecked || (!(bool)_reuseCharsTrue.IsChecked && int.Parse(_numCharsTextBox.Text) <= _charactersTextBox.Text.Length)))
			{
				char[] availableCharacters = _charactersTextBox.Text.ToCharArray();
				int passwordLength = int.Parse(_numCharsTextBox.Text);
				bool allowRepeatedCharacters = (bool)_reuseCharsTrue.IsChecked ? true : false;
				Random randomizer = new Random((int)DateTime.Now.ToBinary());
				string newPassword = String.Empty;

				for (int counter = 0; counter < passwordLength; counter++)
				{
					int nextChar = randomizer.Next(availableCharacters.Length - 1);

					if (allowRepeatedCharacters)
					{
						newPassword = String.Format("{0}{1}", newPassword, availableCharacters[nextChar]);
					}
					else
					{
						if (!newPassword.Contains(availableCharacters[nextChar]))
						{
							newPassword = String.Format("{0}{1}", newPassword, availableCharacters[nextChar]);
						}
					}
				}

				_generatedPasswordTextBox.Text = newPassword;
			}
			else
			{
				_statusLabel.Content = String.Empty;

				if (String.IsNullOrEmpty(_charactersTextBox.Text))
				{
					_charactersTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));

					String.Concat(_statusLabel.Content, "Characters for password must be defined.");
				}

				if (!(bool)_reuseCharsTrue.IsChecked && int.Parse(_numCharsTextBox.Text) > _charactersTextBox.Text.Length)
				{
					_numCharsTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));

					String.Concat(_statusLabel.Content, "Password length invalid.");
				}
			}
		}

		private void CopyPasswordToClipboard(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(_generatedPasswordTextBox.Text);
		}
    }
}

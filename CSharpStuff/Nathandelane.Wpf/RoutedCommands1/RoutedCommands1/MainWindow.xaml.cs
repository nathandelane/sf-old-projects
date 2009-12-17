using System;
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

namespace RoutedCommands1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static RoutedUICommand CutText = new RoutedUICommand("Cut Text", "CutText", typeof(MainWindow));

		public string Value { get; private set; }

		public MainWindow()
		{
			InitializeComponent();
		}

		private void CutText_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CutText_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;

			Value = textBox.Text;

			textBox.Text = String.Empty;
		}
	}
}

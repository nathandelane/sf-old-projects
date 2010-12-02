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
using System.Web;

namespace SqlEscaper
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

		private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
		{
			string value = ((TextBox)e.Source).Text;

			if (!String.IsNullOrEmpty(value) && !String.IsNullOrEmpty(value.Trim()))
			{
				if (textBox2 != null)
				{
					textBox2.Text = HttpUtility.HtmlEncode(value);
				}
			}
		}

		private void IsFocused(object sender, RoutedEventArgs e)
		{
			((TextBox)e.Source).SelectAll();
		}
	}
}

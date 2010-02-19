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
using System.Threading;

namespace Nathandelane.Net.WebGet.WpfWebGet
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Constructor

		public MainWindow()
		{
			InitializeComponent();

			BitmapFrame windowIcon = BitmapFrame.Create(Application.GetResourceStream(new Uri("icons/ndellogo.ico", UriKind.RelativeOrAbsolute)).Stream);
			this.Icon = windowIcon;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Checks for the Enter key being pressed which signals to start the download.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FilterEnter(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				Agent newAgent = new Agent(_urlTextBox.Text, _saveAsTextBox.Text, true);

                _savedItemsListBox.Items.Add(newAgent.ToString());
				_saveAsTextBox.Text = String.Empty;
				_urlTextBox.Text = String.Empty;

				ThreadPool.QueueUserWorkItem(newAgent.Run, newAgent.ToString());
			}
		}

		/// <summary>
		/// Clears the URL and Save as text boxes if they contain text.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearUrlAndSaveAsBoxes(object sender, RoutedEventArgs e)
		{
			if (!String.IsNullOrEmpty(_urlTextBox.Text))
			{
				_urlTextBox.Text = String.Empty;
			}

			if (!String.IsNullOrEmpty(_saveAsTextBox.Text))
			{
				_saveAsTextBox.Text = String.Empty;
			}
		}

		#endregion
	}
}

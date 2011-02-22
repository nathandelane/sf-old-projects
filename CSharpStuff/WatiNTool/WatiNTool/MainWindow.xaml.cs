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
using WatiN.Core;

namespace WatiNTool
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Fields

		private static WindowCollection __windowCollection = new WindowCollection();
		private static IE __attachedBrowser;

		#endregion

		#region Constructor

		public MainWindow()
		{
			InitializeComponent();

			PopulateComboBox();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Regenerates the WindowCollection object and re-populates the combobox.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RefreshBrowserList(object sender, RoutedEventArgs e)
		{
			MainWindow.__windowCollection = new WindowCollection();

			PopulateComboBox();
		}

		/// <summary>
		/// Detaches from the IE browser currently attached to.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DetachFromBrowser(object sender, RoutedEventArgs e)
		{
			if (MainWindow.__attachedBrowser != null)
			{
				MainWindow.__attachedBrowser = null;

				RefreshBrowserList(sender, e);
			}
		}

		/// <summary>
		/// Attaches the WatiN Tool to the selected browser.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AttachToSelectedBrowser(object sender, SelectionChangedEventArgs e)
		{
			if (_windowsComboBox.SelectedIndex > 0)
			{
				MainWindow.__attachedBrowser = MainWindow.__windowCollection[(_windowsComboBox.SelectedIndex - 1)];

				_loggingTextBox.AppendText(String.Format("Attached to IE window {0} <{1}>{2}{2}", MainWindow.__attachedBrowser.Title, MainWindow.__attachedBrowser, Environment.NewLine));
			}
			else
			{
				DetachFromBrowser(sender, null);
			}
		}

		/// <summary>
		/// HACK: Cleans everything up in preparation of closing the application. This is a hack, 
		/// because for some reason the window is not closing all the way when I click on 
		/// the close button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CleanUpWatiN(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DetachFromBrowser(sender, null);

			Environment.Exit(0);
		}

		/// <summary>
		/// Executes the test code in the test text box by evaluating it.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExecuteTestCode(object sender, RoutedEventArgs e)
		{
			string code = _commandEntryTextBox.Text;

			WatiNInterpreter.Evaluate(MainWindow.__attachedBrowser, code);
		}

		/// <summary>
		/// Populates the combobox with the WindowCollection data.
		/// </summary>
		private void PopulateComboBox()
		{
			_windowsComboBox.Items.Add("Detached from All Browsers");

			foreach (IE nextIe in MainWindow.__windowCollection)
			{
				_windowsComboBox.Items.Add(nextIe.Title);
			}

			_windowsComboBox.SelectedIndex = 0;
		}
		
		#endregion
	}
}

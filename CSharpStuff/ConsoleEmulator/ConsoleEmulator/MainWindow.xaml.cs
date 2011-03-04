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

namespace ConsoleEmulator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Constructors
		
		public MainWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods
		
		/// <summary>
		/// Creates a new Command Console tab.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateNewCommandTab(object sender, RoutedEventArgs e)
		{
			Console newCmdConsole = ConsoleFactory.GetFactory().CreateCmdConsole();

			CreateNewTabForConsole(newCmdConsole);
		}

		/// <summary>
		/// Creates a new CygWin Console tab.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateNewCygWinTab(object sender, RoutedEventArgs e)
		{
			Console newCmdConsole = ConsoleFactory.GetFactory().CreateCygWinConsole();

			CreateNewTabForConsole(newCmdConsole);
		}

		/// <summary>
		/// Creates a new PowerShell Console tab.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateNewPowerShellTab(object sender, RoutedEventArgs e)
		{
			Console newCmdConsole = ConsoleFactory.GetFactory().CreatePowerShellConsole();

			CreateNewTabForConsole(newCmdConsole);
		}

		/// <summary>
		/// Creates a new tab for a given console.
		/// </summary>
		/// <param name="console"></param>
		private void CreateNewTabForConsole(Console console)
		{
			if (console != null)
			{
				TabItem newTabItem = new TabItem();
				newTabItem.Header = String.Format("{0}", console.Type);
				
				TextBox newTabTextBox = new TextBox();
				newTabTextBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
				newTabTextBox.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
				newTabTextBox.IsReadOnly = true;
				newTabTextBox.Focusable = true;
				
				newTabItem.Content = newTabTextBox;

				_tabControl.Items.Add(newTabItem);
			}
		}

		#endregion
	}
}

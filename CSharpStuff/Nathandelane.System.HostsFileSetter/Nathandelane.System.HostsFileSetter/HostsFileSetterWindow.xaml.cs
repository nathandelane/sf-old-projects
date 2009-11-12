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
using System.Configuration;
using System.IO;

namespace Nathandelane.HostsFileSetter
{
	/// <summary>
	/// Interaction logic for HostsFileSetterWindow.xaml
	/// </summary>
	public partial class HostsFileSetterWindow : Window
	{
		#region Fields

		private static readonly string HostFileLocation = "hostFileLocation";
		private static readonly string BackupOriginalHostsFile = "backupOriginalHostsFile";

		#endregion

		#region Constructors

		public HostsFileSetterWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Events

		private void LoadCurrentHostsFile(object sender, RoutedEventArgs e)
		{
			using (StreamReader reader = new StreamReader(ConfigurationManager.AppSettings[HostsFileSetterWindow.HostFileLocation]))
			{
				string data = reader.ReadToEnd();

				_hostsFileTextBox.Text = data;
			}
		}

		#endregion
	}
}

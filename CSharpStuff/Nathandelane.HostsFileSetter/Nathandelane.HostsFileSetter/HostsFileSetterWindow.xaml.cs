using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Nathandelane.HostsFileSetter
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class HostsFileSetterWindow : Window
	{
		#region Runtime Interop

		[DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
		private static extern UInt32 DnsFlushResolverCache();

		#endregion

		#region Fields

		private static readonly string __hostsFileLocation = "hostFileLocation";

		#endregion

		#region Constructor

		public HostsFileSetterWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void LoadInitHostsFile(object sender, RoutedEventArgs e)
		{
			LoadHostsFile();
		}

		private void LoadHostsFile()
		{
			using (StreamReader reader = new StreamReader(ConfigurationManager.AppSettings[HostsFileSetterWindow.__hostsFileLocation]))
			{
				_hostsFileTextBox.Text = reader.ReadToEnd();
			}
		}

		#endregion
	}
}

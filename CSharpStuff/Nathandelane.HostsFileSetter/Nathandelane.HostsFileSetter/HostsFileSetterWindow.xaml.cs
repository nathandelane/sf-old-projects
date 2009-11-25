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
using Nathandelane.HostsFileSetter.Models;

namespace Nathandelane.HostsFileSetter
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class HostsFileSetterWindow : Window
	{
		#region Constructor

		public HostsFileSetterWindow()
		{
			InitializeComponent();

			_serversListBox.ItemsSource = new HostsFileCollectionModel();
		}

		#endregion

		#region Methods

		#region Runtime Interop

		[DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
		private static extern UInt32 DnsFlushResolverCache();

		#endregion

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ReloadHostsFile();
		}

		private void FlushDnsCache(object sender, RoutedEventArgs e)
		{
			DnsFlushResolverCache();
		}

		private void ExitHostsFileSetter(object sender, RoutedEventArgs e)
		{
			this.Close();

			Environment.Exit(0);
		}

		private void ShowAboutDialog(object sender, RoutedEventArgs e)
		{
			AboutDialog aboutDialog = new AboutDialog();
			aboutDialog.Show();
		}

		private void UpdateHostsFile(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListBox)
			{
				AbstractHostsFileConfiguration selectedConfig = (AbstractHostsFileConfiguration)((ListBox)sender).SelectedItem;

				using (StreamWriter writer = new StreamWriter(new FileStream(ConfigurationManager.AppSettings["hostFileLocation"], FileMode.Truncate)))
				{
					writer.Write(selectedConfig.Comments);
					writer.Write(String.Format("{0}{0}", Environment.NewLine));

					foreach (DnsEntry dnsEntry in selectedConfig.Entries)
					{
						writer.WriteLine(String.Format("{0}\t\t\t{1}", dnsEntry.IpAddress, dnsEntry.Name));
					}
				}

				ReloadHostsFile();
				DnsFlushResolverCache();
			}
		}

		private void ReloadHostsFile()
		{
			HostsFileModel model = new HostsFileModel();

			_hostsFileTextBox.Text = model.FileContents;
		}

		#endregion
	}
}

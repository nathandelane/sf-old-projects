﻿using System;
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
		#region Runtime Interop

		[DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
		private static extern UInt32 DnsFlushResolverCache();

		#endregion

		#region Fields

		private static readonly string __hostsFileLocation = "hostFileLocation";
		private static HostsFileCollectionModel __servers;

		#endregion

		#region Constructor

		public HostsFileSetterWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			HostsFileSetterWindow.__servers = new HostsFileCollectionModel();

			_serversListBox.ItemsSource = HostsFileSetterWindow.__servers;
		}

		#endregion
	}
}

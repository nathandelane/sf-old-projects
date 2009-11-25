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
using System.Windows.Shapes;

namespace Nathandelane.HostsFileSetter
{
	/// <summary>
	/// Interaction logic for AboutDialog.xaml
	/// </summary>
	public partial class AboutDialog : Window
	{
		#region Constructors

		public AboutDialog()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void Hide(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
		}

		#endregion
	}
}

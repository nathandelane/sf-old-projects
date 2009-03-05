using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Nathandelane.Net.WebGet;

namespace Nathandelane.Net.WebGet.GraphicalWebGet
{
	public partial class MainForm : Form
	{
		private string _outputDirectory;
		private IDictionary<string, WebGetDelegate> _webGetCollection;

		public MainForm(string outputDirectory)
		{
			_outputDirectory = outputDirectory;
			_webGetCollection = new Dictionary<string, WebGetDelegate>();

			InitializeComponent();
		}

		private void AddResourceAndBeginDownload(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(urlTextBox.Text))
			{
				string resourceName = urlTextBox.Text;

				if (!_webGetCollection.ContainsKey(resourceName))
				{
					WebGetDelegate webGet = new WebGetDelegate(resourceName);
					webGet.WebGet.DownloadFileCompleted = new AsyncCompletedEventHandler(OnFileDownloadCompleted);

					_webGetCollection.Add(resourceName, new WebGetDelegate(resourceName));

					resourceListBox.Items.Add(webGet.WebGet.FileName);
				}
				else
				{
					MessageBox.Show("You are already downloading that resource.");
				}
			}
			else
			{
				MessageBox.Show("You must supply a resource name in the text field to continue.");
			}
		}

		private void OnFileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			MessageBox.Show("Completed");
		}
	}
}

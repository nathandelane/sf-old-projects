using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Nathandelane.Net.WebGet;

namespace Nathandelane.Net.WebGet.GraphicalWebGet
{
	public partial class Form1 : Form
	{
		
		private Dictionary<string, Agent> _agents;
		private string _outputDirectory;

		public Form1()
		{
			_agents = new Dictionary<string, Agent>();
			_outputDirectory = ConfigurationManager.AppSettings["outputDirectory"];

			InitializeComponent();
		}

		private void ClearUrlTextBox(object sender, EventArgs e)
		{
			_urlTextBox.Text = String.Empty;
		}

		private void AddResource(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(_urlTextBox.Text))
			{
				string name = _urlTextBox.Text.Substring(_urlTextBox.Text.LastIndexOf("/") + 1);

				if (!_resourceListBox.Items.Contains(name))
				{
					try
					{
						Agent nextAgent = new Agent(_urlTextBox.Text);
						nextAgent.FileName = String.Format("{0}\\{1}", _outputDirectory, nextAgent.FileName);
						nextAgent.Client.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadCompleted);

						_agents.Add(name, nextAgent);

						ThreadStart threadStart = new ThreadStart(_agents[name].Run);
						Thread thread = new Thread(threadStart);
						thread.Start();						

						_resourceListBox.Items.Add(name);
					}
					catch (Exception ex)
					{
						MessageBox.Show(String.Format("Exception caught! {0}", ex.Message));
					}
				}
				else
				{
					MessageBox.Show(String.Format("You are already downloading a resource named {0}", name));
				}

				ClearUrlTextBox(sender, e);
			}
			else
			{
				MessageBox.Show("You must enter a URL in the text box.");
			}
		}

		void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			string name = ((Agent)sender).FileName;
			int resourceIndex = _resourceListBox.Items.IndexOf(name);

			_resourceListBox.Items[resourceIndex] = String.Concat("Done...", name);
			_agents.Remove(name);
		}
	}
}

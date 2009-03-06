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

			Logger.LogMessage(String.Format("Created new GWGet form with empty agents collection and outputDirectory of {0}.", _outputDirectory));

			InitializeComponent();
		}

		private void ClearUrlTextBox(object sender, EventArgs e)
		{
			_urlTextBox.Text = String.Empty;
			_saveAsTextBox.Text = String.Empty;

			Logger.LogMessage("Cleared url and save as file name.");
		}

		private void AddResource(object sender, EventArgs e)
		{
			Logger.LogMessage("Began downloading file.");

			if (!String.IsNullOrEmpty(_urlTextBox.Text))
			{
				string name = _urlTextBox.Text.Substring(_urlTextBox.Text.LastIndexOf("/") + 1);

				Logger.LogMessage(String.Format("Got file name: {0}", name));

				if (!_resourceListBox.Items.Contains(name))
				{
					try
					{
						Agent nextAgent;

						if (String.IsNullOrEmpty(_saveAsTextBox.Text))
						{
							nextAgent = new Agent(_urlTextBox.Text);

							Logger.LogMessage("Using resource name as file name.");
						}
						else
						{
							nextAgent = new Agent(_urlTextBox.Text, _saveAsTextBox.Text);

							Logger.LogMessage("Using save as file name as file name.");
						}

						nextAgent.FileName = String.Format("{0}\\{1}", _outputDirectory, nextAgent.FileName);
						nextAgent.Client.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadCompleted);

						Logger.LogMessage(String.Format("Set file name and location to {0}.", nextAgent.FileName));

						_agents.Add(name, nextAgent);

						ThreadStart threadStart = new ThreadStart(_agents[name].Run);
						Thread thread = new Thread(threadStart);
						thread.Start();						

						_resourceListBox.Items.Add(name);
					}
					catch (Exception ex)
					{
						Logger.LogMessage(String.Format("Caught an exception! {0}", ex.Message));

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
			Logger.LogMessage("Download completed.");

			string name = ((Agent)sender).FileName;
			int resourceIndex = _resourceListBox.Items.IndexOf(name);

			_resourceListBox.Items[resourceIndex] = String.Concat("Done...", name);
			_agents.Remove(name);
		}
	}
}

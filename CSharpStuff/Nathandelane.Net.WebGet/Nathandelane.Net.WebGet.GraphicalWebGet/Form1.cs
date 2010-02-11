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
using System.IO;

namespace Nathandelane.Net.WebGet.GraphicalWebGet
{
	public partial class Form1 : Form
	{
		#region Fields

		private Dictionary<string, Agent> _agents;
		private string _outputDirectory;

		#endregion

		#region Constructors

		public Form1()
		{
			_agents = new Dictionary<string, Agent>();
			_outputDirectory = ConfigurationManager.AppSettings["outputDirectory"];

			InitializeComponent();
		}

		#endregion

		private void ClearUrlTextBox(object sender, EventArgs e)
		{
			_urlTextBox.Text = String.Empty;
			_urlTextBox.Focus();
		}

		private void StartWget(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(_urlTextBox.Text))
			{
				string name = _urlTextBox.Text.Substring(_urlTextBox.Text.LastIndexOf('/') + 1);

				if (!String.IsNullOrEmpty(_saveAsTextBox.Text))
				{
					name = _saveAsTextBox.Text;
				}

				if (!_resourceListBox.Items.Contains(name))
				{
					try
					{
                        string fileName = String.Format("{0}{1}{2}", _outputDirectory, '\\', name);
                        DialogResult result = DialogResult.None;

                        if (File.Exists(fileName))
                        {
                            result = MessageBox.Show(this, String.Format("There already exists a file by the name of {0}. Would you like to continue? If you would like to try and continue using this output filename, then please click Yes. Otherwise click on No and rename your output file.", fileName), "Warning! File Already Exists!", MessageBoxButtons.YesNo);
                        }

                        if((File.Exists(fileName) && result == DialogResult.Yes) || !File.Exists(fileName))
                        {
                            Agent nextAgent = new Agent(_urlTextBox.Text, name, true);
                            nextAgent.FileName = fileName;
                            nextAgent.Client.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadCompleted);

                            _agents.Add(name, nextAgent);

                            ThreadStart threadStart = new ThreadStart(_agents[name].Run);
                            Thread thread = new Thread(threadStart);
                            thread.Start();

                            _resourceListBox.Items.Add(String.Format("({0}) {1}", _urlTextBox.Text.Substring(_urlTextBox.Text.LastIndexOf('/') + 1), name));
                            _resourceListBox.SetSelected(_resourceListBox.Items.Count - 1, true);
                            _resourceListBox.SetSelected(_resourceListBox.Items.Count - 1, false);

                            ResetForm(sender, e);
                        }
					}
					catch (Exception ex)
					{
						MessageBox.Show(String.Format("You are already downloading a resource named {0}.", name));
					}
				}
				else
				{
					MessageBox.Show("You must enter a URL in the URL text box.");
				}
			}
		}

		private void ResetForm(object sender, EventArgs e)
		{
			_urlTextBox.Text = String.Empty;
			_saveAsTextBox.Text = String.Empty;
			_urlTextBox.Focus();
		}

		private void ClearList(object sender, EventArgs e)
		{
			_resourceListBox.Items.Clear();
			_urlTextBox.Focus();
		}

		private void FilterEnterKey(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				StartWget(sender, e);
				_urlTextBox.Focus();
			}
		}

		public void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			string name = ((Agent)sender).FileName;
			int resourceIndex = _resourceListBox.Items.IndexOf(name);

			_resourceListBox.Items[resourceIndex] = String.Concat("Done...", name);
			_agents.Remove(name);
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nathandelane.Net.WebGet;

namespace Nathandelane.Net.WebGet.GraphicalWebGet
{
	public partial class Form1 : Form
	{
		private Dictionary<string, Agent> _agents;

		public Form1()
		{
			_agents = new Dictionary<string, Agent>();

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

						_agents.Add(name, nextAgent);
						_agents[name].Run();

						_resourceListBox.Items.Add(name);
					}
					catch (Exception ex)
					{
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
	}
}

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
		}

		private void StartWget(object sender, EventArgs e)
		{

		}

		private void ResetForm(object sender, EventArgs e)
		{
			_urlTextBox.Text = String.Empty;
			_saveAsTextBox.Text = String.Empty;
		}

		private void ClearList(object sender, EventArgs e)
		{
			_resourceListBox.Items.Clear();
		}

		private void FilterEnterKey(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				StartWget(sender, e);
			}
		}
	}
}

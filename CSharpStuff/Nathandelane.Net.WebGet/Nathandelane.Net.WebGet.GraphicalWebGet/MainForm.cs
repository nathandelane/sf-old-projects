using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Nathandelane.Net.WebGet.GraphicalWebGet
{
	public partial class MainForm : Form
	{
		private string _outputDirectory;


		public MainForm(string outputDirectory)
		{
			_outputDirectory = outputDirectory;

			InitializeComponent();
		}

		private void AddResourceAndBeginDownload(object sender, EventArgs e)
		{

		}
	}
}

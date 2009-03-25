using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HackerConsole
{
	public partial class Console : Form
	{
		public Console()
		{
			InitializeComponent();
		}

		private void OnKeyPress(object sender, KeyEventArgs e)
		{
		}

		private void ExitConsole(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}
	}
}

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
			if (e.Alt && e.KeyCode == Keys.F4)
			{

			}
		}
	}
}

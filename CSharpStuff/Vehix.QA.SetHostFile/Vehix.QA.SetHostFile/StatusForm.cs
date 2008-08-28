using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vehix.QA.SetHostFile
{
	public partial class StatusForm : Form
	{
		public StatusForm(string serverName)
		{
			InitializeComponent();

			label.Text = serverName;
		}
	}
}
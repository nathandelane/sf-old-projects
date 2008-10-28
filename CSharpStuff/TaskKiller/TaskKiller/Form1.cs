using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace TaskKiller
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void KillTask(object sender, EventArgs e)
		{
			string taskName = taskTextField.Text;
			Regex regex = new Regex("\\w+\\.{1}\\w");
			
			if(!regex.IsMatch(taskName))
			{
				taskName += ".exe";
			}

			try
			{
				Process proc = new Process();
				proc.EnableRaisingEvents = true;
				proc.StartInfo.FileName = "taskkill";
				proc.StartInfo.Arguments = String.Format("/IM {0} /T", taskName);
				proc.StartInfo.CreateNoWindow = true;
				proc.StartInfo.UseShellExecute = false;
				proc.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Format("{0}", ex.Message));
			}
		}

		private void CaptureKeys(object sender, KeyPressEventArgs e)
		{
			if ((int)e.KeyChar == (int)Keys.Enter)
			{
				KillTask(sender, new EventArgs());
			}
		}
	}
}
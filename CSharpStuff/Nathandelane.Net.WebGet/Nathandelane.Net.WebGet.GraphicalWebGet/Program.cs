using System;
using System.Configuration;
using System.Windows.Forms;

namespace Nathandelane.Net.WebGet.GraphicalWebGet
{
	internal sealed class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			string outputDirectory = ConfigurationManager.AppSettings["outputDirectory"];

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(outputDirectory));
		}
		
	}
}

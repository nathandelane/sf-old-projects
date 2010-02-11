using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Nathandelane.Net.WebGet
{
	public class Agent
	{
		#region Fields

		private static string __saveFolder;

		private string _url;
		private string _fileName;
		private WebClient _client;
		private bool _hasGraphicalInterface;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the save folder for files being saved.
		/// </summary>
		public static string SaveFolder
		{
			get { return Agent.__saveFolder; }
			set { Agent.__saveFolder = value; }
		}

		#endregion

		#region Constructors

		public Agent(string url, string fileName, bool hasGraphicalInterface)
		{
			_url = url;
			_fileName = String.IsNullOrEmpty(fileName) ? _url.Substring(_url.LastIndexOf("/") + 1) : fileName;
			_client = new WebClient();
			_hasGraphicalInterface = hasGraphicalInterface;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Runs the agent in order to get the file specified.
		/// </summary>
		public void Run()
		{
			try
			{
				Uri uri = null;

				if (Uri.TryCreate(_url, UriKind.Absolute, out uri))
				{
					_client.DownloadFile(uri, _fileName);
				}
				else
				{
					throw new Exception(String.Format("Url was malformed or could not otherwise be parsed: {0}.", _url));
				}
			}
			catch (Exception e)
			{
				if (_hasGraphicalInterface)
				{
					MessageBox.Show(String.Format("Message: {0}\r\nStackTrace:\r\n{1}", e.Message, e.StackTrace));
				}
				else
				{
					Console.Write("Message: {0}\r\nStackTrace:\r\n{1}", e.Message, e.StackTrace);
				}
			}
		}

		/// <summary>
		/// Generates a string representation of the Agent.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("{Agent: {0} ({1})", _url, _fileName);
		}

		#endregion
	}
}

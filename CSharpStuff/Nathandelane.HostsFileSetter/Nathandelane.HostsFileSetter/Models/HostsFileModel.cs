using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.IO;

namespace Nathandelane.HostsFileSetter.Models
{
	public class HostsFileModel
	{
		#region Fields

		private string _fileContents;

		#endregion

		#region Properties

		public string FileContents
		{
			get { return _fileContents; }
		}

		#endregion

		#region Constructors

		public HostsFileModel()
		{
			string hostsFileLocation = ConfigurationManager.AppSettings["hostFileLocation"];

			using (StreamReader reader = new StreamReader(new FileStream(hostsFileLocation, FileMode.Open)))
			{
				_fileContents = reader.ReadToEnd();
			}
		}

		#endregion
	}
}

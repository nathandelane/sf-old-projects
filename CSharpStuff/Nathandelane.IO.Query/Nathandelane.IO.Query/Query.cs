using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Query
{
	public class Query
	{
		#region Fields

		private FileSystemInfo _resource;
		private FileSystemQuery _systemQuery;

		#endregion

		#region Constructors

		public Query(string directory, string query)
		{
			_resource = (Directory.Exists(directory)) ? (new DirectoryInfo(directory) as FileSystemInfo) : (new FileInfo(directory) as FileSystemInfo);
			_systemQuery = new FileSystemQuery(query);
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace Nathandelane.IO.CopyFiles
{
	public class CopyFiles
	{
		#region Fields

		private static ArchiveMethod _unpackMethod;

		#endregion

		#region Public Methods

		public static CopyResult Copy(string source, string destination, ArchiveMethod unpackMethod)
		{
			CopyResult result = CopyResult.Default;

			_unpackMethod = unpackMethod;

			if (source.Contains(',') && destination.Contains(','))
			{
				result = CopyMultiple(source.Split(new char[] { ',' }), destination.Split(new char[] { ',' }));
			}
			else if (source.Contains(','))
			{
				result = CopyFromMultiple(source.Split(new char[] { ',' }), destination);
			}
			else if (!source.Contains(',') && !destination.Contains(','))
			{
				result = CopySingle(source, destination);
			}
			else
			{
				result = CopyResult.Error;
			}

			return result;
		}

		#endregion

		#region Private Methods

		private static CopyResult CopySingle(string source, string destination)
		{
			CopyResult result = CopyResult.Default;
			FileSystemInfo sourceFsi = GetFileOrDirectory(source);
			FileSystemInfo destinationFsi = GetFileOrDirectory(destination);

			if (sourceFsi != null && destinationFsi != null)
			{
				
			}
			else if (destinationFsi == null)
			{
			}
			else if (sourceFsi == null)
			{
				result = CopyResult.Error;
				result.Description = "Source does not exist";
			}

			return result;
		}

		private static CopyResult CopyMultiple(string[] sources, string[] destinations)
		{
			CopyResult result = CopyResult.Default;

			return result;
		}

		private static CopyResult CopyFromMultiple(string[] sources, string destination)
		{
			CopyResult result = CopyResult.Default;

			return result;
		}

		private static FileSystemInfo GetFileOrDirectory(string descriptor)
		{
			FileSystemInfo fsInfo = null;

			using (Impersonator impersonator = new Impersonator(ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["domainName"], ConfigurationManager.AppSettings["password"]))
			{
				if (File.Exists(descriptor))
				{
					fsInfo = new FileInfo(descriptor);
				}
				else if (Directory.Exists(descriptor))
				{
					fsInfo = new DirectoryInfo(descriptor);
				}
			}

			return fsInfo;
		}

		private static bool TryCreateDestination(string destination)
		{
			bool result = false;

			return result;
		}

		#endregion
	}
}

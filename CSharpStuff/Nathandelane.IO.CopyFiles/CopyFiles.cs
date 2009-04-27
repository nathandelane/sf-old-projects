using System;
using System.IO;
using System.Linq;

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
				result = ProcessCopy(sourceFsi, destinationFsi);
			}
			else if (destinationFsi == null)
			{
				DestinationType type = DestinationType.File;

				if (sourceFsi is DirectoryInfo)
				{
					type = DestinationType.Directory;

					TryCreateDestination(destination, type);
				}

				destinationFsi = GetFileOrDirectory(destination);

				if (destinationFsi == null)
				{
					destinationFsi = new FileInfo(destination);
				}

				result = ProcessCopy(sourceFsi, destinationFsi);
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
			throw new NotImplementedException("CopyMultiple");
		}

		private static CopyResult CopyFromMultiple(string[] sources, string destination)
		{
			throw new NotImplementedException("CopyFromMultiple");
		}

		private static FileSystemInfo GetFileOrDirectory(string descriptor)
		{
			FileSystemInfo fsInfo = null;

			using (Impersonator impersonator = new Impersonator())
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

		private static bool TryCreateDestination(string destination, DestinationType type)
		{
			bool result = true;

			using (Impersonator impersonator = new Impersonator())
			{
				try
				{
					switch (type)
					{
						case DestinationType.File:
							File.Create(destination);
							break;
						case DestinationType.Directory:
							Directory.CreateDirectory(destination);
							break;
					}
				}
				catch (Exception)
				{
					result = false;
				}
			}

			return result;
		}

		private static CopyResult ProcessCopy(FileSystemInfo source, FileSystemInfo destination)
		{
			CopyResult result = CopyResult.Default;

			if (source is FileInfo && destination is FileInfo)
			{
				using (Impersonator impersonator = new Impersonator())
				{
					File.Copy(source.FullName, destination.FullName, true);

					if (GetFileOrDirectory(destination.FullName) != null)
					{
						result = CopyResult.Success;
						result.Description = String.Format("{0} was successfully copied", destination);
					}
					else
					{
						result = CopyResult.Failure;
						result.Description = String.Format("{0} could not be copied", destination);
					}
				}
			}
			else if (source is DirectoryInfo && destination is DirectoryInfo)
			{
				using (Impersonator impersonator = new Impersonator())
				{
					FileInfo[] files = ((DirectoryInfo)source).GetFiles();

					foreach (FileInfo file in files)
					{
						File.Copy(file.FullName, String.Format("{0}{1}{2}", destination.FullName, Path.PathSeparator, file.Name));
					}
				}
			}

			return result;
		}

		#endregion
	}
}

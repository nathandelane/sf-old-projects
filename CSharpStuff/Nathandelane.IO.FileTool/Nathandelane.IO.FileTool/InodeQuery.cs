using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.FileTool
{
	public enum QueryType
	{
		GetAttributes,
		SetAttributes,
		GetCreationTime,
		SetCreationTime,
		GetLastAccessTime,
		SetLastAccessTime,
		GetLastWriteTime,
		SetLastWriteTime
	}

	public class InodeQuery
	{
		#region Fields

		private QueryType _type;
		private object _setValue;

		#endregion

		#region Properties

		public QueryType Type
		{
			get { return _type; }
		}

		public object SetValue
		{
			get { return _setValue; }
		}

		#endregion

		#region Constructor

		public InodeQuery(string command)
		{
			Parse(command);
		}

		#endregion

		#region Private Methods

		private void Parse(string command)
		{
			if (command.Contains("="))
			{
				string value = command.Split(new char[] { '=' })[1];
				if (command.StartsWith("-a") || command.StartsWith("--attributes"))
				{
					_type = QueryType.SetAttributes;
					_setValue = ParseAttributes(value);
				}
				else if (command.StartsWith("-c") || command.StartsWith("--creationtime"))
				{
					_type = QueryType.SetCreationTime;
					_setValue = DateTime.Parse(value);
				}
				else if (command.StartsWith("-t") || command.StartsWith("--accesstime"))
				{
					_type = QueryType.SetLastAccessTime;
					_setValue = DateTime.Parse(value);
				}
				else if (command.StartsWith("-w") || command.StartsWith("--writetime"))
				{
					_type = QueryType.SetLastWriteTime;
					_setValue = DateTime.Parse(value);
				}
			}
			else
			{
				if (command.Equals("-a") || command.Equals("--attributes"))
				{
					_type = QueryType.GetAttributes;
				}
				else if (command.Equals("-c") || command.Equals("--creationtime"))
				{
					_type = QueryType.GetCreationTime;
				}
				else if (command.Equals("-t") || command.Equals("--accesstime"))
				{
					_type = QueryType.GetLastAccessTime;
				}
				else if (command.Equals("-w") || command.Equals("--writetime"))
				{
					_type = QueryType.GetLastWriteTime;
				}
			}
		}

		private FileAttributes ParseAttributes(string attribs)
		{
			FileAttributes fileAttributes = FileAttributes.Normal;

			if (attribs.Contains("+"))
			{
				if (!attribs.ToLower().Contains("normal"))
				{
					string[] attributes = attribs.Split(new char[] { '+' });
					foreach (string nextAttribute in attributes)
					{
						switch (nextAttribute.ToLower())
						{
							case "archive":
								fileAttributes = AddAttribute(fileAttributes, FileAttributes.Archive);
								break;
							case "readonly":
								fileAttributes = AddAttribute(fileAttributes, FileAttributes.ReadOnly);
								break;
							case "hidden":
								fileAttributes = AddAttribute(fileAttributes, FileAttributes.Hidden);
								break;
							case "system":
								fileAttributes = AddAttribute(fileAttributes, FileAttributes.System);
								break;
						}
					}
				}
			}
			else if(!attribs.ToLower().Contains("n"))
			{
				char[] attributes = attribs.ToCharArray();
				foreach (char nextAttribute in attributes)
				{
					switch (nextAttribute)
					{
						case 'a':
						case 'A':
							fileAttributes = AddAttribute(fileAttributes, FileAttributes.Archive);
							break;
						case 'r':
						case 'R':
							fileAttributes = AddAttribute(fileAttributes, FileAttributes.ReadOnly);
							break;
						case 'h':
						case 'H':
							fileAttributes = AddAttribute(fileAttributes, FileAttributes.Hidden);
							break;
						case 's':
						case 'S':
							fileAttributes = AddAttribute(fileAttributes, FileAttributes.System);
							break;
					}
				}
			}
			

			return fileAttributes;
		}

		private FileAttributes AddAttribute(FileAttributes currentAttributes, FileAttributes additionalAttribute)
		{
			if (currentAttributes == FileAttributes.Normal)
			{
				currentAttributes = additionalAttribute;
			}
			else
			{
				currentAttributes = currentAttributes | additionalAttribute;
			}

			return currentAttributes;
		}

		#endregion
	}
}

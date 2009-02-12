using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Concatenate
{
	public class Concatenate : IDisposable
	{
		#region Fields

		private long _lineNumber;
		private string[] _fileNames;
		private string _options;
		private bool _isDisposed;

		#endregion

		#region Properties

		public bool IsDisposed
		{
			get { return _isDisposed; }
		}

		#endregion

		#region Constructors

		public Concatenate(string fileName)
		{
			_isDisposed = false;
			_fileNames = new string[] { fileName };
			_options = String.Empty;
		}

		public Concatenate(string[] fileNames)
		{
			_isDisposed = false;
			_fileNames = fileNames;
			_options = String.Empty;
		}

		public Concatenate(string fileName, string options)
		{
			_isDisposed = false;
			_fileNames = new string[] { fileName };
			_options = options;
		}

		public Concatenate(string[] fileNames, string options)
		{
			_isDisposed = false;
			_fileNames = fileNames;
			_options = options;
		}

		#endregion

		#region Public Methods

		public void Run()
		{
			foreach (string fileName in _fileNames)
			{
				_lineNumber = 1;
				using (StreamReader reader = new StreamReader(fileName))
				{
					string line = String.Empty;
					try
					{
						do
						{
							line = reader.ReadLine();

							if (line == null)
							{
								break;
							}
							else
							{
								Console.WriteLine("{0}{1}{2}", ShowLineNumberOption(line), Normalize(line), ShowEndsOption());
							}
						}
						while (true);
					}
					catch (Exception)
					{
					}
				}
			}
		}

		#endregion

		#region Private Methods

		private string Normalize(string line)
		{
			string value = line;

			if (_options.Contains("T"))
			{
				value = value.Replace("\t", "^I");
			}

			return value;
		}

		private string ShowLineNumberOption(string line)
		{
			string value = String.Empty;

			if (_options.Contains("b"))
			{
				if (!String.IsNullOrEmpty(line.Trim().Replace("\r", String.Empty).Replace("\n", String.Empty)))
				{
					value = _lineNumber.ToString();
					_lineNumber++;
				}
			}
			else if (_options.Contains("n"))
			{
				value = _lineNumber.ToString();
				_lineNumber++;
			}

			return value;
		}

		private string ShowEndsOption()
		{
			return ((_options.Contains("E")) ? "$" : String.Empty);
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (!_isDisposed)
			{
				_isDisposed = true;
			}
		}

		#endregion
	}
}

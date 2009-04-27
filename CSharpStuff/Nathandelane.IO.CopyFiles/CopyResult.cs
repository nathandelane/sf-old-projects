using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.CopyFiles
{
	public class CopyResult
	{
		#region Fields

		private string _name;
		private string _description;

		#endregion

		#region Properties

		public string Name
		{
			get { return _name; }
		}

		public string Description
		{
			get
			{
				string desc = String.Empty;

				if (String.IsNullOrEmpty(_description))
				{
					desc = "None";
				}
				else
				{
					desc = _description;
				}

				return desc;
			}

			set { _description = value; }
		}

		#endregion

		#region Constructors

		private CopyResult(string name)
		{
			_name = name;
		}

		#endregion

		#region ToString Method

		public override string ToString()
		{
			return String.Format("{0}; {1}", Name, Description);
		}

		#endregion

		#region Static Members

		public static readonly CopyResult Default = new CopyResult("Default");
		public static readonly CopyResult Success = new CopyResult("Success");
		public static readonly CopyResult Failure = new CopyResult("Failure");
		public static readonly CopyResult Error = new CopyResult("Error");

		#endregion
	}
}

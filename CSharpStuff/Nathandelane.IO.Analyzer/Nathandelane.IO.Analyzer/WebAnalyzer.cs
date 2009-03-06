using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Analyzer
{
	public class WebAnalyzer : IAnalyzer
	{
		#region Fields

		private WebAnalyzerType _type;
		private string _location;
		private int _timeout;

		#endregion

		#region Properties

		public WebAnalyzerType Type
		{
			get { return _type; }
			set { _type = value; }
		}

		public string Location
		{
			get { return _location; }
		}

		public int Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}

		#endregion

		#region Constructors

		public WebAnalyzer(WebAnalyzerType type, string location)
		{
			_type = type;
			_location = location;
			_timeout = 30;
		}

		#endregion

		#region Public Methods

		public virtual void Run()
		{
			throw new NotImplementedException("Run()");
		}

		#endregion
	}
}

﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;

namespace Nathandelane.IO.Analyzer
{
	public abstract class WebAnalyzer
	{
		#region Fields

		private WebAnalyzerType _type;
		private Uri _location;

		#endregion

		#region Properties

		public WebAnalyzerType Type
		{
			get { return _type; }
		}

		public Uri Location
		{
			get { return _location; }
		}

		#endregion

		#region Constructors

		public WebAnalyzer(WebAnalyzerType type, Uri location)
		{
			_type = type;
			_location = location;
		}

		#endregion
	}
}

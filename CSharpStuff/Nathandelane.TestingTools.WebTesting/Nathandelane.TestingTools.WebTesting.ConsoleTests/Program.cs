using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.TestingTools.WebTesting;
using Nathandelane.TestingTools.WebTesting.Rules;
using System.Diagnostics;

namespace Nathandelane.TestingTools.WebTesting.ConsoleTests
{
	class Program
	{
		static void Main(string[] args)
		{
			WebTestRequest request = new WebTestRequest("http://www.vehix.com/");
			ExtractHiddenFields extractionRule1 = new ExtractHiddenFields();

			request.ExtractValues += new WebTestRequest.ExtractionEventHandler(extractionRule1.Extract);

			request.Execute();

			Debug.Assert(request.Context.Count > 0);
		}
	}
}

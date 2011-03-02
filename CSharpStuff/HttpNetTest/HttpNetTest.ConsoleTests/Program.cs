using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpNetTest;
using HttpNetTest.Rules;
using System.Diagnostics;

namespace HttpNetTest.ConsoleTests
{
	class Program
	{
		static void Main(string[] args)
		{
			NetTestRequest request = new NetTestRequest("http://www.vehix.com/");
			ExtractHiddenFields extractionRule1 = new ExtractHiddenFields();

			request.ExtractValues += new NetTestRequest.ExtractionEventHandler(extractionRule1.Extract);

			request.Execute();

			Debug.Assert(request.Context.Count > 0);
		}
	}
}

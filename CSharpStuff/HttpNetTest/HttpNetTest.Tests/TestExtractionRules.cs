using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HttpNetTest.Rules;

namespace HttpNetTest.Tests
{
	[TestFixture]
	public class TestExtractionRules
	{
		#region Methods

		[Test]
		public void TestExtractHiddenFields()
		{
			NetTestRequest request = new NetTestRequest("http://www.vehix.com/");
			ExtractHiddenFields extractionRule1 = new ExtractHiddenFields();

			request.ExtractValues += new NetTestRequest.ExtractionEventHandler(extractionRule1.Extract);

			request.Execute();

			Assert.IsNotNull(request.Context["sclient"]);
		}

		#endregion
	}
}

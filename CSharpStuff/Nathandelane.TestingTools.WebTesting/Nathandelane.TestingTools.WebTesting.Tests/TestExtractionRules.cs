﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nathandelane.TestingTools.WebTesting.Rules;

namespace Nathandelane.TestingTools.WebTesting.Tests
{
	[TestFixture]
	public class TestExtractionRules
	{
		#region Methods

		[Test]
		public void TestExtractHiddenFields()
		{
			WebTestRequest request = new WebTestRequest("http://www.vehix.com/");
			ExtractHiddenFields extractionRule1 = new ExtractHiddenFields();

			request.ExtractValues += new WebTestRequest.ExtractionEventHandler(extractionRule1.Extract);

			request.Execute();

			Assert.IsNotNull(request.Context["sclient"]);
		}

		#endregion
	}
}
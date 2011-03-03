using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nathandelane.TestingTools.WebTesting;

namespace Nathandelane.TestingTools.WebTesting.Tests
{
	[TestFixture]
	public class TestNetTestRequest
	{
		[Test]
		public void TestConstruction()
		{
			WebTestRequest request = new WebTestRequest("http://www.google.com/");

			Assert.IsNotNull(request);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestConstructionWithBadUrl()
		{
			WebTestRequest request = new WebTestRequest("www.google.com");
		}

		[Test]
		public void TestConstructionWithUri()
		{
			Uri uri = new Uri("http://www.google.com/");
			WebTestRequest request = new WebTestRequest(uri);

			Assert.IsNotNull(request);
		}
	}
}

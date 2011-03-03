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
			NetTestRequest request = new NetTestRequest("http://www.google.com/");

			Assert.IsNotNull(request);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestConstructionWithBadUrl()
		{
			NetTestRequest request = new NetTestRequest("www.google.com");
		}

		[Test]
		public void TestConstructionWithUri()
		{
			Uri uri = new Uri("http://www.google.com/");
			NetTestRequest request = new NetTestRequest(uri);

			Assert.IsNotNull(request);
		}
	}
}

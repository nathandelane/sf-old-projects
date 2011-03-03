using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.TestingTools.WebTesting;
using Nathandelane.TestingTools.WebTesting.Rules;

namespace ExampleTestProject
{
	public class OreillyHomepage : WebTest
	{
		public override IEnumerator<WebTestRequest> GetRequestEnumerator()
		{
			WebTestRequest request1 = new WebTestRequest("http://oreilly.com/");
			yield return request1;
		}
	}
}

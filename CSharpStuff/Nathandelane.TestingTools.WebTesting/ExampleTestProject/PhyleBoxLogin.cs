using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.TestingTools.WebTesting;
using Nathandelane.TestingTools.WebTesting.Rules;
using System.Xml.Linq;

namespace ExampleTestProject
{
	public class PhyleBoxLogin : WebTest
	{
		public override IEnumerator<WebTestRequest> GetRequestEnumerator()
		{
			WebTestRequest request1 = new WebTestRequest("http://phyer.net/phyle-box/login.php");
			request1.ContentType = "application/x-www-form-urlencoded";
			request1.Method = "POST";
			request1.HttpRequestBody = "userName=nathanlane&amp;password=i78y6zbgfhla";

			yield return request1;

			WebTestRequest request2 = new WebTestRequest("http://phyer.net/phyle-box/index.php");
			yield return request2;
		}
	}
}

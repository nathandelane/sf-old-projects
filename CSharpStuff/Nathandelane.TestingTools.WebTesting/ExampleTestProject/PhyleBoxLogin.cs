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
			ValidateResponseUrl rule1 = new ValidateResponseUrl();
			rule1.ExpectedResponseUrl = "http://phyer.net/phyle-box/index.php";

			WebTestRequest request1 = new WebTestRequest("http://phyer.net/phyle-box/login.php");
			request1.ContentType = "application/x-www-form-urlencoded";
			request1.Method = "POST";
			request1.HttpRequestBody = "userName=nathanlane&amp;password=i78y6zbgfhla";
			request1.ValidateResponse += new WebTestRequest.ValidationEventHandler(rule1.Validate);
			yield return request1;

			WebTestRequest request2 = new WebTestRequest("http://phyer.net/phyle-box/index.php");
			request2.ValidateResponse += new WebTestRequest.ValidationEventHandler(rule1.Validate);
			yield return request2;
		}
	}
}

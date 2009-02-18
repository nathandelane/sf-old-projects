using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nathandelane.Math.Processor.Parser;
using Nathandelane.Math.Processor.Tokens;

namespace Nathandelane.Math.Processor.Parser.UnitTests
{
	/// <summary>
	/// Summary description for TokenParserUnitTests
	/// </summary>
	[TestClass]
	public class TokenParserUnitTests
	{
		public TokenParserUnitTests()
		{
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void TestParseMethod()
		{
			string exampleExpression = "1+2";
			TokenParser parser = TokenParser.Parse(exampleExpression);

			var tokens = from t in parser.Expression
						 select t;

			Assert.AreEqual(3, tokens.Count<IToken>());
		}

		[TestMethod]
		public void TestParseMethodUsingNegatives1()
		{
			string exampleExpression = "1+-2";
			TokenParser parser = TokenParser.Parse(exampleExpression);

			var tokens = from t in parser.Expression
						 select t;

			Assert.AreEqual(3, tokens.Count<IToken>());
		}

		[TestMethod]
		public void TestParseMethodUsingNegatives2()
		{
			string exampleExpression = "-1+-2";
			TokenParser parser = TokenParser.Parse(exampleExpression);

			var tokens = from t in parser.Expression
						 select t;

			Assert.AreEqual(3, tokens.Count<IToken>());
		}

		[TestMethod]
		public void TestParseMethodUsingNegatives3()
		{
			string exampleExpression = "-1+2";
			TokenParser parser = TokenParser.Parse(exampleExpression);

			var tokens = from t in parser.Expression
						 select t;

			Assert.AreEqual(3, tokens.Count<IToken>());
		}

		[TestMethod]
		public void TestParseMethodUsingNegatives4()
		{
			string exampleExpression = "-1--2";
			TokenParser parser = TokenParser.Parse(exampleExpression);

			var tokens = from t in parser.Expression
						 select t;

			Assert.AreEqual(3, tokens.Count<IToken>());
		}
	}
}

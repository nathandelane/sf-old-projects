using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace Nathandelane.WatiN.VehixTesting
{
	[TestClass]
	public abstract class WatiNTestBase
	{
		#region Fields

		private TestContext _testContextInstance;

		#endregion

		#region Properties

		public TestContext TestContext
		{
			get { return _testContextInstance; }
			set { _testContextInstance = value; }
		}

		#endregion

		#region Constructors

		public WatiNTestBase()
		{
		}

		#endregion

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

		#region Test Methods

		[TestMethod]
		public abstract void TestMain()
		{
		}

		#endregion
	}
}

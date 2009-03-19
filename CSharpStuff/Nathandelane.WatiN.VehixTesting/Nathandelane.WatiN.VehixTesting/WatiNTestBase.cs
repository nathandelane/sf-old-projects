using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;
using System.IO;
using System.Reflection;

namespace Nathandelane.WatiN.VehixTesting
{
	[TestClass]
	public abstract class WatiNTestBase
	{
		#region Fields

		private static readonly string __configurationUri = "Nathandelane.WatiN.VehixTesting.WatiNTestConfiguration.xml";

		private TestContext _testContextInstance;
		private TestType _type;
		private TestLength _length;
		private XDocument _settings;

		#endregion

		#region Properties

		public TestContext TestContext
		{
			get { return _testContextInstance; }
			set { _testContextInstance = value; }
		}

		public TestType Type
		{
			get { return _type; }
		}

		public TestLength Length
		{
			get { return _length; }
		}

		public string this[string key]
		{
			get
			{
				string value = String.Empty;

				if (_settings != null)
				{
					string[] components = key.Split(new char[] { '.' });
					XElement nextElement = _settings.Root;

					for (int componentIndex = 0; componentIndex < (components.Length - 1); componentIndex++)
					{
						nextElement = nextElement.Element(XName.Get(components[componentIndex]));
					}

					value = nextElement.Attribute(XName.Get(components[components.Length - 1])).Value;
				}

				return value;
			}
		}

		#endregion

		#region Constructors

		public WatiNTestBase()
		{
			LoadConfiguration();
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

		#region Private Methods

		private void LoadConfiguration()
		{
			using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(WatiNTestBase.__configurationUri)))
			{
				_settings = XDocument.Parse(reader.ReadToEnd());
			}
		}

		#endregion
	}
}

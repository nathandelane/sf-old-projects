using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.TestingTools.WebTesting
{
	public abstract class WebTest
	{
		#region Fields

		private WebTestContext _context;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the context for this test.
		/// </summary>
		public WebTestContext Context
		{
			get { return _context; }
			set { _context = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of WebTest.
		/// </summary>
		public WebTest()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// When overridden in a derived class, returns an IEnumerator<T> interface that supports a simple iteration over a generic collection of WebTestRequest.
		/// </summary>
		/// <returns></returns>
		public abstract IEnumerator<WebTestRequest> GetRequestEnumerator();

		#endregion
	}
}

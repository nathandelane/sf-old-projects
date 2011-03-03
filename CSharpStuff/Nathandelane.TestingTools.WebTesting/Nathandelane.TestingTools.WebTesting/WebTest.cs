/**
 * Web Testing Framework to automate HTTP-based web testing.
 * Copyright (C) 2011 Nathan Lane
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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
		private WebTestOutcome _outcome;
		private Guid _guid;

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

		/// <summary>
		/// Gets or sets the outcome of this test.
		/// </summary>
		public WebTestOutcome Outcome
		{
			get { return _outcome; }
			set { _outcome = value; }
		}

		/// <summary>
		/// Gets or sets the GUID for this test.
		/// </summary>
		public Guid Guid
		{
			get { return _guid; }
			set { _guid = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of WebTest.
		/// </summary>
		public WebTest()
		{
			_context = WebTestContext.GetContext();
			_outcome = WebTestOutcome.NotExecuted;
			_guid = Guid.NewGuid();
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

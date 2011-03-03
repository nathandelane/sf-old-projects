﻿/**
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

namespace Nathandelane.TestingTools.WebTesting.Events
{
	public class ValidationEventArgs : EventArgs
	{
		#region Fields

		private WebTestItem _webTestItem;

		#endregion

		#region Properties

		public WebTestItem WebTestItem
		{
			get { return _webTestItem; }
		}

		#endregion

		#region Constructors

		public ValidationEventArgs(WebTestItem webTestItem)
		{
			_webTestItem = webTestItem;
		}

		#endregion
	}
}

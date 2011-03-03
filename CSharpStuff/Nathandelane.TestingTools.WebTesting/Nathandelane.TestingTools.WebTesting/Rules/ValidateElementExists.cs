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
using System.Xml.Linq;

namespace Nathandelane.TestingTools.WebTesting.Rules
{
	public class ValidateElementExists : ValidationRule
	{
		#region Fields

		private XElement _element;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the expected element.
		/// </summary>
		public XElement Element
		{
			get { return _element; }
			set { _element = value; }
		}

		#endregion

		#region Methods

		public override void Validate(object sender, Events.ValidationEventArgs e)
		{
			_outcome = WebTestOutcome.Passed;

			if (_element == null)
			{
				throw new ArgumentNullException("Element may not be null.");
			}

			try
			{
				base.Validate(sender, e);

				IEnumerable<XElement> elements = base.Document.Document.Descendants();
				IEnumerable<XElement> formElements = from el in elements where el.Name.LocalName.Equals(_element.Name.LocalName) select el;

				foreach (XElement nextElement in formElements)
				{
					foreach (XAttribute nextAttribute in _element.Attributes())
					{
						if (nextElement.Attribute(nextAttribute.Name) == null || !nextElement.Attribute(nextAttribute.Name).Value.Equals(nextAttribute.Value))
						{
							_outcome = WebTestOutcome.Failed;
						}
					}
				}

				if (_outcome != WebTestOutcome.Passed)
				{
					_outcome = WebTestOutcome.Failed;
				}
			}
			catch (Exception ex)
			{
				_outcome = WebTestOutcome.Error;
				_message = ex.Message;
			}
		}

		#endregion
	}
}

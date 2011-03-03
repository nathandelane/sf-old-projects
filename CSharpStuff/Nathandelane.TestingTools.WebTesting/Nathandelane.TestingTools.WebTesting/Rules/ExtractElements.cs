﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Nathandelane.TestingTools.WebTesting.Rules
{
	public class ExtractElements : ExtractionRule
	{
		#region Fields

		private string _elementName;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the name of the elements to extract.
		/// </summary>
		public string ElementName
		{
			get { return _elementName; }
			set { _elementName = value; }
		}

		#endregion

		#region Methods

		public override void Extract(object sender, Events.ExtractionEventArgs e)
		{
			if (String.IsNullOrEmpty(_elementName))
			{
				throw new ArgumentNullException("Element name may not be null.");
			}

			base.Extract(sender, e);

			IEnumerable<XElement> elements = base.Document.Document.Descendants();
			IEnumerable<XElement> selectedElements = from el in elements where el.Name.LocalName.Equals(_elementName) select el;
			int elementIndex = 0;

			foreach (XElement nextElement in selectedElements)
			{
				base._context[String.Format("{0}{1}", _elementName, elementIndex)] = nextElement;

				elementIndex++;
			}
		}

		#endregion
	}
}
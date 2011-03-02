using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Collections;
using System.Xml.Linq;

namespace HttpNetTest.Rules
{
	public class ExtractHiddenFields : ExtractionRule
	{
		#region Methods

		public override void Extract(object sender, Events.ExtractionEventArgs e)
		{
			base.Extract(sender, e);

			IEnumerable<XElement> elements = base.Document.Document.Descendants();
			IEnumerable<XElement> hiddenElements = from el in elements where el.Name.LocalName.Equals("input") && el.Attribute(XName.Get("type")).Value.Equals("hidden") select el;

			foreach (XElement nextElement in hiddenElements)
			{
				string name = String.Empty;
				string value = String.Empty;

				if (nextElement.HasAttributes && nextElement.Attribute(XName.Get("name")) != null)
				{
					name = nextElement.Attribute(XName.Get("name")).Value;

					if (nextElement.Attribute(XName.Get("value")) != null)
					{
						value = nextElement.Attribute(XName.Get("value")).Value;
					}

					base._context[name] = value;
				}
			}
		}

		#endregion
	}
}

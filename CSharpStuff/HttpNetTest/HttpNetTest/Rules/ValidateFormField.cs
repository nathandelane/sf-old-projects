using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpNetTest;
using System.Xml.Linq;

namespace HttpNetTest.Rules
{
	public class ValidateFormField : ValidationRule
	{
		#region Fields

		private string _name;
		private string _expectedValue;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the form field's name.
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Gets or sets the form field's expected value.
		/// </summary>
		public string ExpectedValue
		{
			get { return _expectedValue; }
			set { _expectedValue = value; }
		}

		#endregion

		#region Methods

		public override void Validate(object sender, Events.ValidationEventArgs e)
		{
			if (String.IsNullOrEmpty(_name))
			{
				throw new ArgumentNullException("Name may not be null.");
			}

			try
			{
				base.Validate(sender, e);

				IEnumerable<XElement> elements = base.Document.Document.Descendants();
				IEnumerable<XElement> formElements = from el in elements where el.Name.LocalName.Equals("input") && el.Attribute(XName.Get("name")).Value.Equals(_name) select el;

				foreach (XElement nextElement in formElements)
				{
					if (nextElement.Attribute(XName.Get("value")) != null && nextElement.Attribute(XName.Get("value")).Value.Equals(_expectedValue))
					{
						_outcome = WebTestOutcome.Passed;
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

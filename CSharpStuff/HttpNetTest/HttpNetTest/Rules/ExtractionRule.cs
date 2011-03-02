using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpNetTest.Events;
using HtmlAgilityPack;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace HttpNetTest.Rules
{
	public abstract class ExtractionRule
	{
		#region Fields

		protected NetTestContext _context;

		private XDocument _document;
		private string _ruleDescription;
		private string _ruleName;
		private bool _required;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the description of this ExtractionRule.
		/// </summary>
		public string RuleDescription
		{
			get { return _ruleDescription; }
			set { _ruleName = value; }
		}

		/// <summary>
		/// Gets or sets the name of this ExtractionRule.
		/// </summary>
		public string RuleName
		{
			get { return _ruleName; }
			set { _ruleName = value; }
		}

		/// <summary>
		/// Gets the Response body as an XDocument object.
		/// </summary>
		public XDocument Document
		{
			get { return _document; }
		}

		/// <summary>
		/// Gets or sets whether this rule is required.
		/// </summary>
		public bool Required
		{
			get { return _required; }
			set { _required = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of ExtractionRule.
		/// </summary>
		public ExtractionRule()
		{
			_context = NetTestContext.GetContext();
			_required = false;
		}

		/// <summary>
		/// Creates an instance of ExtractionRule.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public ExtractionRule(string name, string description)
		{
			_context = NetTestContext.GetContext();
			_ruleName = name;
			_ruleDescription = description;
			_required = false;
		}

		/// <summary>
		/// Returns a string representation of this ExtractionRule based on the name and description.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("{0}: {1}", _ruleName, _ruleDescription);
		}

		/// <summary>
		/// When overridden in a derived class, this extracts information from both the request and response into the NetTestContext for the running test.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public virtual void Extract(Object sender, ExtractionEventArgs e)
		{
			HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
			document.LoadHtml(((NetTestRequest)e.WebTestItem).HttpResponseBody);
			document.OptionOutputAsXml = true;

			using (StringWriter writer = new StringWriter())
			{
				document.Save(writer);

				_document = XDocument.Parse(writer.GetStringBuilder().ToString());
			}
		}

		#endregion
	}
}

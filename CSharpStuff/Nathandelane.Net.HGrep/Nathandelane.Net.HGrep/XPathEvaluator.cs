using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Nathandelane.Net.HGrep
{
	public class XPathEvaluator
	{
		#region Fields

		private HtmlDocument _document;

		#endregion

		#region Constructors

		public XPathEvaluator(string document)
		{
			_document = new HtmlDocument();
			_document.LoadHtml(document);
		}

		public XPathEvaluator(HtmlDocument document)
		{
			_document = document;
		}

		#endregion

		#region Methods

		public HtmlNodeCollection Select(string xPath)
		{
			return _document.DocumentNode.SelectNodes(xPath);
		}

		#endregion
	}
}

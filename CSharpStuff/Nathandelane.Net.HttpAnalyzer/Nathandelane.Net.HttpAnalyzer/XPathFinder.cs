using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Xml;
using HtmlAgilityPack;

namespace Nathandelane.Net.HttpAnalyzer
{
	public class XPathFinder
	{
		private string _xpath;
		private string[] _attributes;
		private string _response;
		private HtmlNodeCollection _collection;

		public XPathFinder(string response, string xpath, string[] attributes)
		{
			_xpath = xpath;
			_attributes = attributes;
			_response = response;

			SelectNodes();
		}

		private void SelectNodes()
		{
			HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
			document.LoadHtml(_response);

			_collection = document.DocumentNode.SelectNodes(_xpath);
		}

		public override string ToString()
		{
			string response = String.Empty;

			if (_attributes.Length > 0)
			{
				int i = 0;
				foreach (HtmlNode node in _collection)
				{
					string nodeAttr = String.Empty;
					foreach (string attr in _attributes)
					{
						if (attr.Equals("innertext"))
						{
							nodeAttr = String.Format("{0},{1}={2}", nodeAttr, attr, node.InnerText);
						}
						else if (attr.Equals("innerhtml"))
						{
							nodeAttr = String.Format("{0},{1}={2}", nodeAttr, attr, node.InnerHtml);
						}
						else
						{
							nodeAttr = String.Format("{0},{1}={2}", nodeAttr, attr, node.Attributes[attr].Value);
						}
					}

					response = String.Format("{0}; {2}:{1}", response, nodeAttr, i);
					i++;
				}
			}
			else
			{
				int i = 0;
				foreach (HtmlNode node in _collection)
				{
					string nodeAttr = String.Empty;
					foreach (HtmlAttribute attr in node.Attributes)
					{
						nodeAttr = String.Format("{0},{1}={2}", nodeAttr, attr.Name, attr.Value);
					}

					response = String.Format("{0}; {2}:{1}", response, nodeAttr, i);
					i++;
				}
			}

			return response;
		}
	}
}

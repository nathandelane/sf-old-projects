using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace Nathandelane.Net.Spider
{
	internal class StartUpQueue
	{
		#region Fields

		private XDocument _settings;

		#endregion

		#region Properties

		public SpiderUrl this[int index]
		{
			get
			{
				XElement element = _settings.Root.Elements().ElementAt(index);
				SpiderUrl url = new SpiderUrl(element.Attribute(XName.Get("url")).Value, element.Attribute(XName.Get("referrer")).Value);
				return url;
			}
		}

		public int Count
		{
			get { return _settings.Root.Elements().Count(); }
		}

		#endregion

		#region Constructors

		public StartUpQueue()
		{
			LoadStartUpQueue();
		}

		#endregion

		#region Public Methods

		public void Save(Queue<SpiderUrl> spiderUrlQueue)
		{
			XmlDocument xmlDoc = new XmlDocument();

			XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0","utf-8",null);
			XmlElement rootNode = xmlDoc.CreateElement("queue");
			xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
			xmlDoc.AppendChild(rootNode);

			while (spiderUrlQueue.Count > 0)
			{
				SpiderUrl url = spiderUrlQueue.Dequeue();

				XmlElement newElement = xmlDoc.CreateElement("spiderUrl");
				newElement.SetAttribute("url", url.Url);
				newElement.SetAttribute("referrer", url.ReferringUrl);

				rootNode.AppendChild(newElement);
			}

			xmlDoc.Save("StartUpQueue.xml");
		}

		#endregion

		#region Private Methods

		private void LoadStartUpQueue()
		{
			using (StreamReader reader = new StreamReader(new FileStream("StartUpQueue.xml", FileMode.Open)))
			{
				_settings = XDocument.Parse(reader.ReadToEnd());
			}
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.IO;

namespace Nathandelane.ReplaceAll
{
	public class Configuration
	{
		#region Fields

		private static readonly string __configurationPath = String.Format("{0}{1}ReplaceAllConfig.xml", Environment.CurrentDirectory, Path.DirectorySeparatorChar);

		private List<Replacement> _replacements;

		#endregion

		#region Properties

		public List<Replacement> Replacements
		{
			get { return _replacements; }
		}

		#endregion

		#region Constructors

		public Configuration()
		{
			_replacements = new List<Replacement>();
		}

		#endregion

		#region Private Methods

		private void LoadConfiguration()
		{
			XDocument doc = XDocument.Load(Configuration.__configurationPath);
			XElement[] replacements = doc.Root.Descendants().ToArray<XElement>();

			foreach (XElement element in replacements)
			{
				Replacement replacement = new Replacement();
				replacement.Name = element.Attribute(XName.Get("name")).Value;
				replacement.OldValue = element.Attribute(XName.Get("oldValue")).Value;
				replacement.NewValue = element.Attribute(XName.Get("newValue")).Value;

				XElement[] files = element.Descendants().ToArray<XElement>();

				foreach (XElement nextFile in files)
				{
					replacement.FilePaths.Add(nextFile.Attribute(XName.Get("path")).Value);
				}

				_replacements.Add(replacement);
			}
		}

		#endregion
	}
}

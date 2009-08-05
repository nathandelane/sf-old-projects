using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Net.HGrep
{
	public class RegexpEvaluator
	{
		#region Fields

		private string _document;

		#endregion

		#region Constructors

		public RegexpEvaluator(string document)
		{
			_document = document;
		}

		#endregion

		#region Methods

		public IList<string> Select(string regularExpression)
		{
			IList<string> results = new List<string>();
			Regex regex = new Regex(regularExpression);
			string[] documentLines = _document.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string line in documentLines)
			{
				if (regex.IsMatch(line))
				{
					results.Add(line);
				}
			}

			return results;
		}

		#endregion
	}
}

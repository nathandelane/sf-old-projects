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

		public MatchCollection Select(string regularExpression)
		{
			MatchCollection matches = null;
			Regex regex = new Regex(regularExpression);

			if (regex.IsMatch(_document))
			{
				matches = regex.Matches(regularExpression);
			}

			return matches;
		}

		#endregion
	}
}

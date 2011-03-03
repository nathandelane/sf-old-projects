using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.TestingTools.WebTesting.Events
{
	public class ExtractionEventArgs : EventArgs
	{
		#region Fields

		private NetTestItem _webTestItem;

		#endregion

		#region Properties

		public NetTestItem WebTestItem
		{
			get { return _webTestItem; }
		}

		#endregion

		#region Constructors

		public ExtractionEventArgs(NetTestItem webTestItem)
		{
			_webTestItem = webTestItem;
		}

		#endregion
	}
}

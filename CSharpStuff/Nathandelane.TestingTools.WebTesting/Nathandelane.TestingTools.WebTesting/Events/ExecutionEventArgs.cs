using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.TestingTools.WebTesting.Events
{
	public class ExecutionEventArgs : EventArgs
	{
		#region Fields

		private WebTestItem _webTestItem;

		#endregion

		#region Properties

		public WebTestItem WebTestItem
		{
			get { return _webTestItem; }
		}

		#endregion

		#region Constructors

		public ExecutionEventArgs(WebTestItem webTestItem)
		{
			_webTestItem = webTestItem;
		}

		#endregion
	}
}

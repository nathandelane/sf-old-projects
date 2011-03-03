using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpNetTest.Events
{
	public class ValidationEventArgs : EventArgs
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

		public ValidationEventArgs(NetTestItem webTestItem)
		{
			_webTestItem = webTestItem;
		}

		#endregion
	}
}

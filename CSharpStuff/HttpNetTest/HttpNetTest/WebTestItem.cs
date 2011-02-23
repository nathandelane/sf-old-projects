using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpNetTest
{
	public abstract class WebTestItem : ICloneable
	{
		#region Fields

		private static int _itemId = -1;

		#endregion

		#region Properties

		/// <summary>
		/// Zero-based sequence number of the item in the Web performance test.
		/// </summary>
		public int ItemId
		{
			get { return WebTestItem._itemId; }
		}

		#endregion

		#region Constructor

		public WebTestItem()
		{
			WebTestItem._itemId++;
		}

		#endregion

		#region Methods

		public object Clone()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpNetTest
{
	public abstract class NetTestItem : ICloneable
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
			get { return NetTestItem._itemId; }
		}

		#endregion

		#region Constructor

		public NetTestItem()
		{
			NetTestItem._itemId++;
		}

		#endregion

		#region Methods

		/// <summary>
		/// When it is overridden in a derived class, creates a copy of this object. 
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determines whether the specified Object is equal to the current Object. (Inherited from Object.)
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. (Inherited from Object.)
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Returns a String that represents the current Object. (Inherited from Object.)
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>
		/// Executes the NetTestItem.
		/// </summary>
		public virtual void Execute()
		{
			throw new NotImplementedException("Execute is not implemented.");
		}

		#endregion
	}
}

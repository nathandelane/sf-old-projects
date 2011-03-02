using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpNetTest
{
	public class NetTestContext
	{
		#region Fields

		private static NetTestContext __context;

		private IDictionary<string, string> _contextItems;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a context item.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string this[string key]
		{
			get
			{
				string result = String.Empty;

				if (_contextItems.ContainsKey(key))
				{
					result = _contextItems[key];
				}

				return result;
			}
			set { _contextItems[key] = value; }
		}

		/// <summary>
		/// Gets the number of items stored in the context.
		/// </summary>
		public int Count
		{
			get { return _contextItems.Count; }
		}

		#endregion

		#region Constructors

		private NetTestContext()
		{
			_contextItems = new Dictionary<string, string>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a reference to the instance of the context.
		/// </summary>
		/// <returns></returns>
		public static NetTestContext GetContext()
		{
			if (NetTestContext.__context == null)
			{
				NetTestContext.__context = new NetTestContext();
			}

			return NetTestContext.__context;
		}

		/// <summary>
		/// Resets the current context instance.
		/// </summary>
		public void Reset()
		{
			NetTestContext.__context = new NetTestContext();
		}

		/// <summary>
		/// Checks whether the context contains an item.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsItem(string key)
		{
			return _contextItems.ContainsKey(key);
		}

		#endregion
	}
}

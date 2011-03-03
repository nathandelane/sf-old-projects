using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.TestingTools.WebTesting
{
	public class WebTestContext
	{
		#region Fields

		private static WebTestContext __context;

		private IDictionary<string, object> _contextItems;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a context item.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public object this[string key]
		{
			get
			{
				object result = String.Empty;

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

		private WebTestContext()
		{
			_contextItems = new Dictionary<string, object>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a reference to the instance of the context.
		/// </summary>
		/// <returns></returns>
		public static WebTestContext GetContext()
		{
			if (WebTestContext.__context == null)
			{
				WebTestContext.__context = new WebTestContext();
			}

			return WebTestContext.__context;
		}

		/// <summary>
		/// Resets the current context instance.
		/// </summary>
		public void Reset()
		{
			WebTestContext.__context = new WebTestContext();
		}

		/// <summary>
		/// Checks whether the context contains an item.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(string key)
		{
			return _contextItems.ContainsKey(key);
		}

		#endregion
	}
}

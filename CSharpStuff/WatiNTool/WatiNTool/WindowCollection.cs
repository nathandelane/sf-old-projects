using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;

namespace WatiNTool
{
	/// <summary>
	/// Contains a collection of window names, making it simpler to attach to a browser.
	/// </summary>
	public class WindowCollection
	{
		#region Fields

		private IECollection _windows;

		#endregion

		#region Properties

		/// <summary>
		/// Gets an IE window by index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public IE this[int index]
		{
			get { return _windows[index]; }
		}

		/// <summary>
		/// Gets the number of windows in the WindowsCollection.
		/// </summary>
		public int Count
		{
			get { return _windows.Count; }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Creates an instance of WindowCollection and collects all of the visible windows.
		/// </summary>
		public WindowCollection()
		{
			_windows = IE.InternetExplorers();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the enumerator for this collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<IE> GetEnumerator()
		{
			return _windows.GetEnumerator();
		}

		#endregion
	}
}

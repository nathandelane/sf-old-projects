using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConsoleEmulator
{
	public class ConsoleCollection
	{
		#region Fields

		private IList<Console> _consoles;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a console by its index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Console this[int index]
		{
			get
			{
				Console result = null;

				if (_consoles.Count > index)
				{
					result = _consoles[index];
				}

				return result;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an instance of ConsoleCollection.
		/// </summary>
		public ConsoleCollection()
		{
			_consoles = new List<Console>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a console to the console collection
		/// </summary>
		/// <param name="name"></param>
		/// <param name="console"></param>
		public void Add(Console console)
		{
			_consoles.Add(console);
		}

		#endregion
	}
}

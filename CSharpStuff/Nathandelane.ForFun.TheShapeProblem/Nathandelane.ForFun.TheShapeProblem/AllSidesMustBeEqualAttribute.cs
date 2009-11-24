using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class AllSidesMustBeEqualAttribute : Attribute
	{
		#region Fields

		private bool _sidesMustBeEqual;

		#endregion

		#region Properties

		/// <summary>
		/// Gets whether all sides must be equal.
		/// </summary>
		public bool SidesMustBeEqual
		{
			get { return _sidesMustBeEqual; }
		}

		#endregion

		#region Constructors

		public AllSidesMustBeEqualAttribute(bool sidesMustBeEqual)
		{
			_sidesMustBeEqual = sidesMustBeEqual;
		}

		#endregion
	}
}

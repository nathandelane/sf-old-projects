using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class AllAnglesMustBeEqualAttribute : Attribute
	{
		#region Fields

		private bool _anglesMustBeEqual;

		#endregion

		#region Properties

		/// <summary>
		/// Gets whether all angles must be equal.
		/// </summary>
		public bool AnglesMustBeEqual
		{
			get { return _anglesMustBeEqual; }
		}

		#endregion

		#region Constructors

		public AllAnglesMustBeEqualAttribute(bool anglesMustBeEqual)
		{
			_anglesMustBeEqual = anglesMustBeEqual;
		}

		#endregion
	}
}

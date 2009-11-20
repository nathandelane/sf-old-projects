using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class AnglesMustEqualAttribute : Attribute
	{
		#region Fields

		private int _degrees;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the number of degrees the angles must be equal to.
		/// </summary>
		public int Degrees
		{
			get { return _degrees; }
		}

		#endregion

		#region Constructors

		public AnglesMustEqualAttribute(int degrees)
		{
			_degrees = degrees;
		}

		#endregion
	}
}

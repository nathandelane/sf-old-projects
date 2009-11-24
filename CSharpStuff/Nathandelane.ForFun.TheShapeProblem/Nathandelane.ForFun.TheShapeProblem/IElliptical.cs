using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public interface IElliptical
	{
		/// <summary>
		/// Gets the SemiMinor Radius of the IElliptical.
		/// </summary>
		double SemiMinorRadius { get; }

		/// <summary>
		///  Gets the SemiMajor Radiius of the IElliptical.
		/// </summary>
		double SemiMajorRadius { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public interface IElliptical : IGeometric
	{
		/// <summary>
		/// Gets the Horizontal Radius of the IElliptical.
		/// </summary>
		double HorizontalRadius { get; }

		/// <summary>
		///  Gets the Vertical Radiius of the IElliptical.
		/// </summary>
		double VerticalRadius { get; }
	}
}

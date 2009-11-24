using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public interface IPolyoganol
	{
		/// <summary>
		/// Gets the segment lengths.
		/// </summary>
		IList<double> SegmentLengths { get; }

		// <summary>
		/// Gets the angles.
		/// </summary>
		IList<double> Angles { get; }
	}
}

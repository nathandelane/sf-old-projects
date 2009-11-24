using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public interface IGeometric
	{
		/// <summary>
		/// Gets the points.
		/// </summary>
		List<Point> Points { get; }

		/// <summary>
		/// Gets the segment lengths.
		/// </summary>
		List<double> SegmentLengths { get; }

		/// <summary>
		/// Gets the angles.
		/// </summary>
		List<double> Angles { get; }
		
		/// <summary>
		/// Gets a specific point.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		Point this[int index] { get; }

		/// <summary>
		/// Adds a point to the IPolyoganol.
		/// </summary>
		/// <param name="point"></param>
		void AddPoint(Point point);
	}
}

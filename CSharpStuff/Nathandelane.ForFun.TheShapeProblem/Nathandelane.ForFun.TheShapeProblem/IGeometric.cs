using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public interface IGeometric
	{
		/// <summary>
		/// Gets the points.
		/// </summary>
		IList<Point> Points { get; }

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

		/// <summary>
		/// Calculates a segment's length
		/// </summary>
		/// <param name="origin">Point First point of the line segment.</param>
		/// <param name="endPoint">Point End point of the line segment.</param>
		/// <returns>double length</returns>
		double CalculateSegmentLength(Point origin, Point endPoint);

		/// <summary>
		/// Calculates the angle between two line segments.
		/// </summary>
		/// <param name="origin">Point Point shared by both vectors.</param>
		/// <param name="firstSide">Point End point of first side.</param>
		/// <param name="secondSide">Point End point of second side.</param>
		/// <returns>double angle</returns>
		double CalculateAngle(Point origin, Point firstSide, Point secondSide);
	}
}

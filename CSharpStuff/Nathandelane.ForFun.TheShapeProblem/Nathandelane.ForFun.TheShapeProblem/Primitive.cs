using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public abstract class Primitive : IGeometric
	{
		#region Fields

		private List<Point> _points;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the points.
		/// </summary>
		public IList<Point> Points
		{
			get { return _points; }
		}

		/// <summary>
		/// Gets a specific point of the Primitive.
		/// </summary>
		/// <param name="index"></param>
		/// <returns>Point</returns>
		public Point this[int index]
		{
			get { return _points[index]; }
		}

		#endregion

		#region Constructors

		public Primitive(Point origin)
			: this(new Point[] { origin })
		{
		}

		public Primitive(IEnumerable<Point> points)
		{
			_points = new List<Point>(points);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a point to the primitive. Also calculates the length between the last point and the newest point, as well as any 
		/// angles between two line segments.
		/// </summary>
		/// <param name="point">Point Point defining a corner of the primitive.</param>
		public virtual void AddPoint(Point point)
		{
			_points.Add(point);
		}

		/// <summary>
		/// Calculates a segment's length.
		/// </summary>
		/// <param name="origin">Point First point of the line segment.</param>
		/// <param name="endPoint">Point End point of the line segment.</param>
		/// <returns>double length</returns>
		public double CalculateSegmentLength(Point origin, Point endPoint)
		{
			double segmentLength = Math.Sqrt(Math.Pow((endPoint.X - origin.X), 2.0) + Math.Pow((endPoint.Y - origin.Y), 2.0));

			return segmentLength;
		}

		/// <summary>
		/// Calculates the angle between two line segments.
		/// </summary>
		/// <param name="origin">Point Point shared by both vectors.</param>
		/// <param name="firstSide">Point End point of first side.</param>
		/// <param name="secondSide">Point End point of second side.</param>
		/// <returns>double angle</returns>
		public double CalculateAngle(Point origin, Point firstSide, Point secondSide)
		{
			Point newFirstSide = new Point((firstSide.X - origin.X), (firstSide.Y - origin.Y));
			Point newSecondSide = new Point((secondSide.X - origin.X), (secondSide.Y - origin.Y));
			double dotProduct = (newFirstSide.X * newSecondSide.X) + (newFirstSide.Y * newSecondSide.Y);

			return Math.Acos(dotProduct / (CalculateSegmentLength(origin, firstSide) * CalculateSegmentLength(origin, secondSide)));
		}

		#endregion
	}
}

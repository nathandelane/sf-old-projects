using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public abstract class Primitive : IGeometric
	{
		#region Fields

		private List<Point> _points;
		private List<double> _segmentLengths;
		private List<double> _angles;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the points.
		/// </summary>
		public List<Point> Points
		{
			get { return _points; }
		}

		/// <summary>
		/// Gets the segment lengths.
		/// </summary>
		public List<double> SegmentLengths
		{
			get { return _segmentLengths; }
		}

		/// <summary>
		/// Gets the angles.
		/// </summary>
		public List<double> Angles
		{
			get { return _angles; }
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

		public Primitive(Point startPoint)
		{
			_points = new List<Point>();
			_points.Add(startPoint);
			_segmentLengths = new List<double>();
			_angles = new List<double>();
		}

		public Primitive(IEnumerable<Point> points)
		{
			_points = new List<Point>(points);
			_segmentLengths = new List<double>();
			_angles = new List<double>();

			if (_points.Count > 1)
			{
				Point lastPoint = _points[0];
				for (int pointsIndex = 1; pointsIndex < _points.Count; pointsIndex++)
				{
					_segmentLengths.Add(CalculateSegmentLength(lastPoint, _points[pointsIndex]));

					lastPoint = _points[pointsIndex];
				}
				_segmentLengths.Add(CalculateSegmentLength(lastPoint, _points[0]));
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the enumerator for the points collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<Point> GetEnumerator()
		{
			return _points.GetEnumerator();
		}

		/// <summary>
		/// Adds a point to the primitive.
		/// </summary>
		/// <param name="point">Point Point defining a corner of the primitive.</param>
		public void AddCorner(Point point)
		{
			_points.Add(point);

			if (_points.Count > 1)
			{
				int newestPointIndex = _points.Count - 1;

				_segmentLengths.Add(CalculateSegmentLength(_points[newestPointIndex - 1], _points[newestPointIndex]));
			}
		}

		/// <summary>
		/// Calculates a segment's length
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

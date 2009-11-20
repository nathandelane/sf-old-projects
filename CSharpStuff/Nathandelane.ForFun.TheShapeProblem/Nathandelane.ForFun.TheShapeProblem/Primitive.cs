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
		/// Gets a specific point of the Primitive.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
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
		}

		public Primitive(IEnumerable<Point> points)
		{
			_points = new List<Point>(points);
			_segmentLengths = new List<double>();

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
		/// Adds a point to the primitive.
		/// </summary>
		/// <param name="point"></param>
		public void AddCorner(Point point)
		{
			_points.Add(point);

			if (_points.Count > 1)
			{

			}
		}

		/// <summary>
		/// Calculates a segment's length
		/// </summary>
		/// <param name="lastPoint"></param>
		/// <param name="currentPoint"></param>
		/// <returns></returns>
		private double CalculateSegmentLength(Point lastPoint, Point currentPoint)
		{
			double segmentLength = 0.0;

			return segmentLength;
		}

		#endregion
	}
}

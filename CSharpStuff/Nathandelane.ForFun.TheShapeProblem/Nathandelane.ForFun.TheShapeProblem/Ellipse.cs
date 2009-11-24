using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	/// <summary>
	/// Creates an Ellipse, which may be vertically and horizontally aligned, or based on its radial points, transformed radially.
	/// </summary>
	public class Ellipse : Primitive
	{
		#region Fields

		private double _radius1;
		private double _radius2;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the semiminor radius of the Ellipse.
		/// </summary>
		public double SemiMinorRadius
		{
			get
			{
				Point origin = Points[0];
				Point radialPoint1 = Points[1];
				Point radialPoint2 = Points[2];
				double semiMinorRadius = 0.0;

				if (_radius1 < _radius2 || _radius1 == _radius2)
				{
					semiMinorRadius = _radius1;
				}
				else
				{
					semiMinorRadius = _radius2;
				}

				return semiMinorRadius;
			}
		}

		/// <summary>
		/// Gets the semimajor radius of the Ellipse.
		/// </summary>
		public double SemiMajorRadius
		{
			get
			{
				Point origin = Points[0];
				Point radialPoint1 = Points[1];
				Point radialPoint2 = Points[2];
				double semiMinorRadius = 0.0;

				if (_radius1 > _radius2 || _radius1 == _radius2)
				{
					semiMinorRadius = _radius1;
				}
				else
				{
					semiMinorRadius = _radius2;
				}

				return semiMinorRadius;
			}
		}

		#endregion

		#region Constructors

		public Ellipse(Point origin, Point radialPoint1, Point radialPoint2)
			: base(new Point[] { origin, radialPoint1, radialPoint2 })
		{
			_radius1 = CalculateSegmentLength(origin, radialPoint1);
			_radius2 = CalculateSegmentLength(origin, radialPoint2);
		}

		#endregion
	}
}
